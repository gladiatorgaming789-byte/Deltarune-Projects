#!/usr/bin/env python3
"""Build Secret Boss Challenge v0.4.2 as mergeable Deltamod .g3mpatch files.

The v0.4.0 gameplay source is extracted from the archived release. Chapter 5
then receives update_ch5_v042.csx before G3MTool creates the semantic patch.
Chapters 1 and 2 remain gameplay-identical to v0.4.1.
"""
from pathlib import Path
import argparse
import shutil
import subprocess
import tempfile
import zipfile

ROOT = Path(__file__).resolve().parent
PROJECT = ROOT.parent
SOURCE_ZIP = PROJECT / 'Secret_Boss_Challenge_v0.4.0_Deltamod.zip'
CHAPTERS = (1, 2, 5)


def run(cmd):
    print('+', *map(str, cmd))
    subprocess.run([str(x) for x in cmd], check=True, stdin=subprocess.DEVNULL)


def main():
    p = argparse.ArgumentParser()
    p.add_argument('--game-root', type=Path, required=True,
                   help='Folder containing chapter1_windows, chapter2_windows and chapter5_windows')
    p.add_argument('--utmt', type=Path, required=True, help='UndertaleModCli executable')
    p.add_argument('--g3mtool', type=Path, required=True, help='Deltamod-compatible G3MTool executable')
    p.add_argument('--output', type=Path, default=ROOT / 'Secret_Boss_Challenge_v0.4.2_Deltamod.zip')
    args = p.parse_args()

    if not SOURCE_ZIP.exists():
        raise FileNotFoundError(f'Archived gameplay source not found: {SOURCE_ZIP}')
    update_ch5 = ROOT / 'update_ch5_v042.csx'
    if not update_ch5.exists():
        raise FileNotFoundError(update_ch5)

    with tempfile.TemporaryDirectory(prefix='sbc_build_') as temp_name:
        temp = Path(temp_name)
        source = temp / 'source'
        stage = temp / 'package'
        patches = stage / 'patches'
        source.mkdir(); patches.mkdir(parents=True)

        with zipfile.ZipFile(SOURCE_ZIP) as archive:
            for chapter in CHAPTERS:
                member = f'patches/SecretBossChallenge_ch{chapter}.csx'
                (source / f'SecretBossChallenge_ch{chapter}.csx').write_bytes(archive.read(member))

        for name in ('meta.json', 'modding.xml', 'README.txt'):
            shutil.copy2(ROOT / name, stage / name)
        shutil.copy2(PROJECT / 'LICENSE', stage / 'LICENSE')

        for chapter in CHAPTERS:
            clean = args.game_root / f'chapter{chapter}_windows' / 'data.win'
            modified = temp / f'ch{chapter}_modified.win'
            base_csx = source / f'SecretBossChallenge_ch{chapter}.csx'
            patch = patches / f'SecretBossChallenge_ch{chapter}.g3mpatch'
            if not clean.exists():
                raise FileNotFoundError(clean)

            scripts = [base_csx]
            if chapter == 5:
                scripts.append(update_ch5)
            run([args.utmt, 'load', clean, '--output', modified, '--scripts', *scripts])
            run([args.g3mtool, 'patch', 'create', clean, modified, patch])
            run([args.g3mtool, 'patch', 'validate', patch, '--data', clean])

        args.output.parent.mkdir(parents=True, exist_ok=True)
        with zipfile.ZipFile(args.output, 'w', zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
            for file in sorted(stage.rglob('*')):
                if file.is_file():
                    archive.write(file, file.relative_to(stage).as_posix())
        with zipfile.ZipFile(args.output) as archive:
            bad = archive.testzip()
            if bad:
                raise RuntimeError(f'ZIP integrity failure: {bad}')
        print('Wrote', args.output)


if __name__ == '__main__':
    main()

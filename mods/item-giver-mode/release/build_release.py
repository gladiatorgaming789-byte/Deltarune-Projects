#!/usr/bin/env python3
"""Build Item Giver Mode v0.2.0 as mergeable Deltamod .g3mpatch files."""
from pathlib import Path
import argparse
import shutil
import subprocess
import tempfile
import zipfile

ROOT = Path(__file__).resolve().parent
CHAPTERS = range(1, 6)


def run(cmd):
    print('+', *map(str, cmd))
    subprocess.run([str(x) for x in cmd], check=True, stdin=subprocess.DEVNULL)


def make_csx(chapter: int, output: Path):
    template = (ROOT / 'ItemGiver_template.csx').read_text(encoding='utf-8')
    gml = (ROOT / 'ItemGiver_Draw75.gml').read_text(encoding='utf-8')
    # Escape quotes for a C# verbatim string used by QueueAppend.
    gml = gml.replace('"', '""')
    text = template.replace('__CHAPTER__', str(chapter)).replace('__GML_BODY__', gml)
    output.write_text(text, encoding='utf-8', newline='\n')


def main():
    p = argparse.ArgumentParser()
    p.add_argument('--game-root', type=Path, required=True,
                   help='Folder containing chapter1_windows ... chapter5_windows')
    p.add_argument('--utmt', type=Path, required=True, help='UndertaleModCli executable')
    p.add_argument('--g3mtool', type=Path, required=True, help='Deltamod-compatible G3MTool executable')
    p.add_argument('--output', type=Path, default=ROOT / 'Item_Giver_Mode_v0.2.0_Deltamod.zip')
    args = p.parse_args()

    with tempfile.TemporaryDirectory(prefix='item_giver_build_') as temp_name:
        temp = Path(temp_name)
        stage = temp / 'package'
        patches = stage / 'patches'
        patches.mkdir(parents=True)
        for name in ('meta.json', 'modding.xml', 'README.txt', 'LICENSE'):
            shutil.copy2(ROOT / name, stage / name)

        for chapter in CHAPTERS:
            clean = args.game_root / f'chapter{chapter}_windows' / 'data.win'
            modified = temp / f'ch{chapter}_modified.win'
            csx = temp / f'ItemGiver_ch{chapter}.csx'
            patch = patches / f'ItemGiver_ch{chapter}.g3mpatch'
            if not clean.exists():
                raise FileNotFoundError(clean)
            make_csx(chapter, csx)
            run([args.utmt, 'load', clean, '--output', modified, '--scripts', csx])
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

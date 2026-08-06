#!/usr/bin/env python3
"""Rebuild Item Giver + Secret Boss Challenge v0.1.0 from the archived standalone sources."""
from pathlib import Path
from zipfile import ZipFile, ZIP_DEFLATED

ROOT = Path(__file__).resolve().parent
REPO = ROOT.parents[2]
PATCH_DIR = ROOT / "patches"
ITEM_TEMPLATE = REPO / "mods" / "item-giver-mode" / "release" / "ItemGiver_template.csx"
SBC_ZIP = REPO / "mods" / "secret-boss-challenge" / "Secret_Boss_Challenge_v0.4.0_Deltamod.zip"


def split_usings(source: str):
    lines = source.splitlines()
    usings = []
    i = 0
    while i < len(lines) and lines[i].startswith("using ") and lines[i].rstrip().endswith(";"):
        usings.append(lines[i].rstrip())
        i += 1
    if i < len(lines) and not lines[i].strip():
        i += 1
    body = "\n".join(lines[i:]).strip() + "\n"
    return usings, body


def item_script(template: str, chapter: int) -> str:
    text = template
    if chapter != 1:
        text = text.replace("chapter 1", f"chapter {chapter}")
        text = text.replace("chapitre 1", f"chapitre {chapter}")
        text = text.replace("Chapter 1", f"Chapter {chapter}")
    return text


def combine(item: str, sbc: str) -> str:
    item_usings, item_body = split_usings(item)
    sbc_usings, sbc_body = split_usings(sbc)
    usings = []
    for line in sbc_usings + item_usings:
        if line not in usings:
            usings.append(line)
    return (
        "\n".join(usings)
        + "\n\n// Secret Boss Challenge v0.4.0\n{\n"
        + sbc_body
        + "}\n\n// Item Giver Mode v0.1.4\n{\n"
        + item_body
        + "}\n"
    )


def main():
    if not ITEM_TEMPLATE.is_file():
        raise SystemExit(f"Missing Item Giver source: {ITEM_TEMPLATE}")
    if not SBC_ZIP.is_file():
        raise SystemExit(f"Missing Secret Boss Challenge source archive: {SBC_ZIP}")

    template = ITEM_TEMPLATE.read_text(encoding="utf-8")
    PATCH_DIR.mkdir(exist_ok=True)

    with ZipFile(SBC_ZIP) as zf:
        for chapter in range(1, 6):
            item = item_script(template, chapter)
            if chapter in (1, 2, 5):
                sbc = zf.read(f"patches/SecretBossChallenge_ch{chapter}.csx").decode("utf-8")
                merged = combine(item, sbc)
            else:
                merged = item
            path = PATCH_DIR / f"ItemGiver_SecretBossChallenge_ch{chapter}.csx"
            path.write_text(merged, encoding="utf-8", newline="\n")
            print(f"Wrote {path.relative_to(ROOT)}")

    zip_path = ROOT.parent / "Item_Giver_Secret_Boss_Challenge_Merged_v0.1.0_Deltamod.zip"
    with ZipFile(zip_path, "w", ZIP_DEFLATED, compresslevel=9) as zf:
        for name in ("meta.json", "modding.xml", "README.txt", "LICENSES.md"):
            zf.write(ROOT / name, name)
        for path in sorted(PATCH_DIR.glob("*.csx")):
            zf.write(path, f"patches/{path.name}")
    print(f"Wrote {zip_path}")


if __name__ == "__main__":
    main()

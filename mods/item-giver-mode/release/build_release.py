#!/usr/bin/env python3
"""Build Item Giver Mode v0.1.4's five Deltamod CSX scripts from the tested Chapter 1 template."""
from pathlib import Path

ROOT = Path(__file__).resolve().parent
TEMPLATE = (ROOT / "ItemGiver_template.csx").read_text(encoding="utf-8")
PATCH_DIR = ROOT / "patches"
PATCH_DIR.mkdir(exist_ok=True)

for chapter in range(1, 6):
    text = TEMPLATE
    if chapter != 1:
        text = text.replace("chapter 1", f"chapter {chapter}")
        text = text.replace("chapitre 1", f"chapitre {chapter}")
        text = text.replace("Chapter 1", f"Chapter {chapter}")
    output = PATCH_DIR / f"ItemGiver_ch{chapter}.csx"
    output.write_text(text, encoding="utf-8", newline="\n")
    print(f"Wrote {output.relative_to(ROOT)}")

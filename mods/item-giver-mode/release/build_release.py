#!/usr/bin/env python3
"""Build Item Giver Mode v0.1.2's five Deltamod CSX scripts from the release template."""
from pathlib import Path

ROOT = Path(__file__).resolve().parent
TEMPLATE = (ROOT / "ItemGiver_template.csx").read_text(encoding="utf-8")
PATCH_DIR = ROOT / "patches"
PATCH_DIR.mkdir(exist_ok=True)

for chapter in range(1, 6):
    text = TEMPLATE
    # v0.1.1 hotkey change.
    text = text.replace("vk_f8", "vk_f7").replace("F8", "F7")
    # v0.1.2 startup fix: bypass DELTARUNE's instance_create -> object_get_depth wrapper.
    text = text.replace(
        "instance_create(0, 0, obj_item_giver);",
        "instance_create_depth(0, 0, -999999, obj_item_giver);",
    )
    # Normalize an older template typo if present.
    text = text.replace("if (norom == 1)", "if (noroom == 1)")

    if chapter != 1:
        text = text.replace("chapter 1", f"chapter {chapter}")
        text = text.replace("chapitre 1", f"chapitre {chapter}")
        text = text.replace("Chapter 1", f"Chapter {chapter}")

    output = PATCH_DIR / f"ItemGiver_ch{chapter}.csx"
    output.write_text(text, encoding="utf-8", newline="\n")
    print(f"Wrote {output.relative_to(ROOT)}")

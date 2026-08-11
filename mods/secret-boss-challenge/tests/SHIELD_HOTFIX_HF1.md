# Secret Boss Challenge v0.4.2 — Shield Hotfix hf1

## Reported runtime failure

Casting Shield in v0.4.2 can fail from `obj_heroparent` Step with:

```text
local variable bbox_top(24) not set before reading it.
at gml_Script_scr_spell
```

The earlier v0.4.2 validation was compile/decompile based and explicitly left real battle testing outstanding. This runtime report supersedes the earlier static assumption that the whole-party Shield cast implementation was safe.

## Root-cause direction

v0.4.2 added Shield-specific local variables directly to `scr_spell`, including a loop-local party index and a local animation instance inside a `with` block. The observed `bbox_top(24)` failure is consistent with the merged/recompiled `scr_spell` local table becoming unsafe at runtime.

The hotfix therefore does not attempt another local-heavy edit to `scr_spell`.

## Hotfix architecture

Vanilla Chapter 5 has no `scr_spell` case 14; spell ID 14 is SBC's Shield. The hotfix changes the single call site in:

`gml_Object_obj_heroparent_Step_0`

from an unconditional `scr_spell(global.charspecial[myself], myself)` call to:

- spell 14 -> `scr_gg_sbc_shield_apply()`
- every other spell -> original `scr_spell(...)`

The new helper contains Shield's party iteration and feedback locals in its own local table. The broken v0.4.2 Shield branch in `scr_spell` is never executed when Shield is cast.

## Preserved v0.4.2 behavior

The hotfix only replaces the cast execution path. Secret Boss Challenge v0.4.2 remains responsible for:

- 75% TP Shield cost;
- whole-party/no-target spell metadata;
- approximately 75% damage reduction;
- Shield expiry after the enemy phase;
- Pink Scarf +25% graze area;
- Pink Scarf +10% graze TP gain.

The helper preserves the v0.4.2 cast feedback for each active party member and sets `global.spelldelay = 20`.

## Final G3M footprint

After removing unrelated UTMT Sound serialization noise:

- changed CodeEntries: 1
  - `gml_Object_obj_heroparent_Step_0`
- new CodeEntries: 1
  - `gml_Script_scr_gg_sbc_shield_apply`
- new Scripts: 1
  - `scr_gg_sbc_shield_apply`
- deleted resources: 0
- Sound resources: 0

G3M patch SHA-256:

`0e3779aeb1f67594d0810fdd8be61b1113f7ad2d9721cc0cd749d79608d381ee`

## Validation

Tools:

- UndertaleModTool CLI 0.9.1.2
- G3MTool 1.2.1
- clean DELTARUNE v23 Chapter 5 SHA-256 `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

Passed:

1. Hotfix source compiled against clean Chapter 5.
2. Raw G3M export was audited; 85 unrelated Sound serialization changes were identified and removed.
3. Cleaned patch validates with G3MTool 1.2.1: 1 changed, 2 new, 0 deleted.
4. Cleaned patch applies successfully to clean Chapter 5.
5. G3M-applied output reopens in UndertaleModTool.
6. Round-trip decompilation confirms spell 14 calls `scr_gg_sbc_shield_apply()` and all other spells still call `scr_spell`.
7. Round-trip decompilation confirms the helper contains the whole-party Shield activation/feedback logic.
8. Item Giver Mode v0.3.1 + this hotfix were merged with G3MTool 1.2.1 in both orders with 0 conflicts.

## Packaging status

This is intentionally a **companion hotfix** for SBC v0.4.2, not a claimed full v0.4.3 rebuild. The archived v0.4.0 Chapter 5 CSX currently stored in GitHub has a malformed ZIP member / corrupted source tail, and the previously generated v0.4.2 binary package is not available in this workspace. A full replacement release cannot be reproduced honestly from those inputs.

Keep Secret Boss Challenge v0.4.2 enabled and enable the hotfix alongside it.

## Remaining runtime test

The decisive remaining check is a real Windows battle cast of Shield with SBC v0.4.2 + hf1 enabled. After the cast succeeds, repeated direct hits, party-wide attacks, varied party compositions, and Shield expiry should still be exercised.

# Item Giver Mode v0.2.0 — Test Report

## Goal

Make Item Giver a separate Deltamod mod that can coexist with Secret Boss Challenge and as many other mods as Deltamod's merge system permits.

## Root cause of the previous compatibility problem

Inspection of Deltamod 2.0.4's installed `GamePatching.js` showed this pipeline:

1. override/copy patches
2. xdelta + `g3mpatch` merge stage
3. CSX stage

For every CSX targeting a chapter `data.win`, Deltamod 2.0.4 loads the same `.bak` source and writes that script's result to the live target. Multiple standalone CSX mods therefore do not reliably stack; a later CSX can replace earlier changes. Sequential UndertaleModTool testing was not an exact simulation of this pipeline.

## Compatibility rewrite

v0.2.0 is distributed as native `.g3mpatch` files generated with **Deltamod 2.0.4's bundled G3MTool 1.2.1**.

The Item Giver implementation was also reduced to one changed resource per chapter:

- Resource type: `CodeEntries`
- Changed resources: **1**
- New resources: **0**
- Deleted resources: **0**
- Entry: `gml_Object_obj_time_Draw_75`

No custom GameMaker object, new event, or helper Script resource is required.

## Clean source SHA-256

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 3: `c1a0925343694ec9b9adcbf2f916a720b02fd1b999286cfe8fe6a52f3320f714`
- Chapter 4: `ed64789586238b52375e994e1c1cf13694dd2d0dab57d13e639b9c892e37d8f2`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Patch SHA-256

- Chapter 1: `b9d944d2d8ea70b3cc622a8b409f98a979ff26c50c3c651aa1897bf109659e82`
- Chapter 2: `9a1bccb064ec276626e6e3ba74603bbafd4e13a2767f5c981cdf5bef2e0cb3b6`
- Chapter 3: `652412922dad71dec4730be9e63143cd89aea39c4d384143b831ffdf4479028e`
- Chapter 4: `11c37b1a56b659578ec3416b9c2ca183ea7347b6911fae3964f9cb91a8cbca6a`
- Chapter 5: `91bcae445bc2c40f8591ff4b8ef30702ccc36ede6d211712088eefd8f4611c3d`

Each packaged patch passed `g3mtool patch validate ... --data <clean chapter>` and reported one changed resource.

## Standalone apply path

The exact G3MTool bundled with Deltamod 2.0.4 was used with the same single-patch operation Deltamod uses:

`g3mtool patch apply <clean> <ItemGiver.g3mpatch> <output>`

All Chapters 1–5 applied successfully. Each output was reopened with UndertaleModCli and `gml_Object_obj_time_Draw_75` was decompiled. Every chapter retained:

- `ig_itemgiver_version`
- `vk_f7`
- `ITEM GIVER`

Semantic-apply output SHA-256:

- Chapter 1: `e053dff848719d2bb003de19558e7825c658c018794d80490a81c3b862c8367a`
- Chapter 2: `d42996e44bdf92e8de80fe5b98972b4443373a56d0bcd7b5d8d55efb9dde962e`
- Chapter 3: `98d6d390e3a5ca98defc0e7455acb6c870582d653b46d906e94d4cbe591604cd`
- Chapter 4: `5cfcf951f07bfa32888ccf9822b3def593e6768aebcbba009fe0d81378c4208b`
- Chapter 5: `82ce961a5aec31a40fb2743f97f5033bbbaaad18f56a942f2c567d27ab2c4f86`

## Separate-mod merge with Secret Boss Challenge v0.4.1

The exact Deltamod 2.0.4 G3MTool merge path was tested for shared Chapters 1, 2, and 5 in both orders:

1. Item Giver → Secret Boss Challenge
2. Secret Boss Challenge → Item Giver

Results:

- Chapter 1: **0 conflicts** in both orders
- Chapter 2: **0 conflicts** in both orders
- Chapter 5: **0 conflicts** in both orders

The G3MTool logs reported one auto-merged helper/asset-order item in each run and no code conflict. Round-trip decompilation retained the Item Giver F7 handler plus Secret Boss Challenge markers in Chapters 1/2, and retained F7, Pink Scarf, and Shield together in Chapter 5.

The two order outputs are not asserted to be byte-identical; both were independently reopened and feature-checked.

## Final Deltamod ZIP

- Package ID: `github.itemgivermode.gladiatorgaming`
- Version: `0.2.0`
- `modding.xml`: five `type="g3mpatch"` routes
- ZIP integrity: passed
- Extracted `.g3mpatch` files: byte-identical to the patches used in validation/merge tests
- ZIP SHA-256: `c42703ffa546a74bd0c02c4112e849edcaeb8330001a42f83684efb2b20772f5`

## Important limitation

Deltamod 2.0.4's later CSX stage can still replace a G3M-merged `data.win`. A separate third-party CSX package targeting the same chapter can therefore remain incompatible even if it edits unrelated code. This cannot be solved solely by reducing Item Giver's G3M resource footprint.

## Manual verification

The previous v0.1.x F7 menu and GUI were confirmed by the user in-game. v0.2.0 moves input handling into the existing Draw GUI End entry to eliminate extra resources. Compilation, semantic application, decompilation, and G3M merge tests pass, but a fresh manual F7 keypress on v0.2.0 still needs confirmation in the user's normal Windows/Deltamod setup.

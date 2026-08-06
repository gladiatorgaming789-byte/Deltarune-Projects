# Item Giver + Secret Boss Challenge Merge v0.1.0 — Test Report

## Source components

- Item Giver Mode v0.1.4
- Secret Boss Challenge v0.4.0
- UndertaleModTool CLI 0.9.1.2
- DELTARUNE Windows full release, launcher version `v23`

## Reason for this merge

The standalone mods were previously tested by applying their CSX patches sequentially. Deltamod's multi-mod merge path uses G3MTool, whose patch merge derives each input independently from the same original before resource merge. A sequential UTMT test therefore does not exactly reproduce Deltamod's cross-mod merge behavior.

The merged package executes both source patches inside a single chapter CSX for Chapters 1, 2, and 5. Chapters 3 and 4 contain Item Giver only.

## Clean source hashes

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 3: `c1a0925343694ec9b9adcbf2f916a720b02fd1b999286cfe8fe6a52f3320f714`
- Chapter 4: `ed64789586238b52375e994e1c1cf13694dd2d0dab57d13e639b9c892e37d8f2`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Merged output hashes

- Chapter 1: `8bbaba2ac85b8dcbe66bb155dcc6d967e710d9d07915197686b2302a8a5ea183`
- Chapter 2: `6451b48e143c6e26bc5620f7d27279a141b9c258edd718ab14d55f5f4755dd2b`
- Chapter 3: `b6c1297c9622d2e18ce9ee335730c7c8cc568f6aaa4991608f36d6799dc64b3b`
- Chapter 4: `6566ff0357bd01ccc9ce790430fc739694a632b3c95cc8fe1825d4c93bd82167`
- Chapter 5: `b4d2e0cb5a146b3cfbe01f4dd5c51c8e284010333831a5cbd920bb9c7b58c163`

Chapter 5's merged-script output is byte-identical to the known-good manual order: clean → Secret Boss Challenge v0.4.0 → Item Giver v0.1.4.

## Idempotency

Each merged chapter script was applied a second time to its first output. Chapters 1–5 were byte-identical after the second application.

## Chapter 5 feature verification

Round-trip decompilation confirmed that the same output contains:

- Item Giver F7 handler in `obj_time` Step.
- Item Giver late GUI renderer in `obj_time` Draw GUI End.
- Pink Scarf definition.
- Pink's Staff definition.
- Secret Boss Challenge Shield implementation and Pink Scarf graze passive in the merged source build.

## Debug Mode v4.01

The compatibility build compiled with Debug Mode in both patch orders across the tested chapters. Chapter 5 round-trip checks in both orders retained:

- Item Giver F7 handler.
- Item Giver GUI.
- Debug Mode F10 handler.
- Pink Scarf.
- Pink's Staff.

## Final package

- ZIP SHA-256: `374b4e5dff51bf4d1995c43b33e2ac1b0e9274afef20e3f361e5dfe904cb7972`
- ZIP size: `32879` bytes.
- ZIP integrity check passed.
- Each CSX script extracted from the final ZIP reproduced its tested merged chapter output byte-for-byte.

## Limitations

A complete interactive Windows playthrough of every Item Giver category and every Secret Boss Challenge battle/reward path was not automated. The merged source was validated by compilation, reopen/decompile inspection, idempotency, output comparison, exact-package reproduction, and Debug Mode patch-order testing.

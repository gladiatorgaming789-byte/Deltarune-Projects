# Secret Boss Challenge v0.4.1 — Test Report

## Scope

v0.4.1 changes the **Deltamod distribution format**, not Secret Boss Challenge gameplay. The gameplay source is the tested v0.4.0 implementation; v0.4.1 materializes that implementation and converts the changed resources to native `.g3mpatch` files.

## Why the format changed

Deltamod 2.0.4 was inspected directly. Its patch pipeline merges xdelta/`g3mpatch` inputs first, then runs CSX patches afterward. Each CSX targeting a chapter is loaded from the chapter `.bak`, so multiple standalone CSX packages can replace one another instead of stacking.

Native `g3mpatch` is therefore the appropriate format for multi-mod resource merging.

## Tools

- UndertaleModTool CLI 0.9.1.2 / Deltamod-bundled UTMT
- **G3MTool 1.2.1 bundled with Deltamod 2.0.4**
- Supplied DELTARUNE Windows full release, launcher target `v23`

## Clean source SHA-256

- Chapter 1: `82c2bb61b8d78cd287120f6301588fecba34ec5a890bac711b7a8774c760ec70`
- Chapter 2: `047c5ab003e3e017a709c02757e119c81e0327760169512110fd276b19241e68`
- Chapter 5: `370dfd141d2955d5a1960122919b16e4092b52ffbb85fda541bc4680c6b3b85c`

## Gameplay source outputs before G3M conversion

The unchanged v0.4.0 CSX source produced:

- Chapter 1: `c282ec0e0aa11ad2cb3906de364bd6806112e677a2d2c00e2e6642f908411a78`
- Chapter 2: `56a136dd92a081a78a2eec115ffaa993d82912fa271140571d33c7aeccb86334`
- Chapter 5: `9e546af99d3cca4a9953c26ce87798c3b444a98356a826a77f7ff749b84a5174`

## G3M patches

Patch SHA-256:

- Chapter 1: `49527c84993260378bc1b45357bb1f010ef95c1d8d315486083c21beef13e722`
- Chapter 2: `f68ec65bf763cf6f1f7c65b9c2d464ede8ca288e0bb00f60e000a41930bd8aca`
- Chapter 5: `516dbcbc8459a2e757a1b512dc882f5c817a8235fd75e370290d5b22387fa7e7`

Each patch passed `g3mtool patch validate --data <clean>`.

Changed resource counts:

- Chapter 1: 9 CodeEntries
- Chapter 2: 8 CodeEntries
- Chapter 5: 16 CodeEntries
- No new or deleted resources in those patches

## Standalone G3M apply

Using Deltamod 2.0.4's bundled G3MTool single-patch path, all three chapters applied successfully.

Semantic-apply output SHA-256:

- Chapter 1: `11a357a5504dc9a4843a29120394df8ecdb59f63dbd5c1315ae8bf275f173999`
- Chapter 2: `dc8db75a8e7e772515a2e1fd03f5e82cc340cb07d12549d1d288e5b9c2aa043b`
- Chapter 5: `8c977653e60eb78c2ab45365df56c38f5ca34f2301616b8840f814708eea9311`

Round-trip inspection retained the Boss Challenge CONFIG markers in Chapters 1 and 2 and Pink Scarf/Shield code in Chapter 5.

## Separate-mod merge with Item Giver Mode v0.2.0

Deltamod's actual G3M merge command style was tested in both orders for Chapters 1, 2, and 5:

1. Secret Boss Challenge → Item Giver
2. Item Giver → Secret Boss Challenge

G3MTool results:

- Chapter 1: `0 conflicts` in both orders
- Chapter 2: `0 conflicts` in both orders
- Chapter 5: `0 conflicts` in both orders

Round-trip checks retained:

- Boss Challenge markers in Chapters 1 and 2
- Item Giver F7 code in all shared chapters
- Pink Scarf in Chapter 5
- Shield in Chapter 5

No combined compatibility package is needed for these versions.

## Preserved v0.4.0 gameplay validation

Because the gameplay source is unchanged, the v0.4.0 checks remain applicable to the source material used to create v0.4.1, including:

- Boss Challenge reward gates
- Meaner Bombs + Boss Challenge Pink's Staff requirement
- Pink Scarf / Pink's Staff definitions and character restrictions
- 65% TP Shield spell registration and Ralsei equipment gate
- approximately 75% Shield damage reduction through the next enemy phase
- battle reset / end-turn expiry
- party-aware approximately +10% graze size and TP gain
- full-inventory pending recovery
- Chapter 5 CONFIG and conditional flower-shop progression

The v0.4.0 Chapter 5 source also previously completed a 30-second Wine startup smoke test without an immediate data-load crash.

## Final Deltamod ZIP

- Package ID: `github.secretbosschallenge.gladiatorgaming`
- Version: `0.4.1`
- Three `type="g3mpatch"` routes: Chapters 1, 2, and 5
- ZIP integrity: passed
- Packaged patch files: byte-identical to the patches used in validation and two-mod merge testing
- ZIP SHA-256: `cdaf7546bd78013dcb26264f91e71499454c7cb4600a016fe76aea252438709e`

## Limitations

A full interactive playthrough of every secret-boss route, Pink reward state, Shield target/hit combination, and inventory state was not repeated for the packaging-only v0.4.1 update. Also, third-party CSX packages targeting the same chapter can still replace G3M output because of Deltamod 2.0.4's CSX stage behavior.

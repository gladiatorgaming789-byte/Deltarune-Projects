# Release archive

## Current release

- **Project:** DELTARUNE Compatibility Merge
- **Version:** 1.0.7
- **Workspace archive:** `DELTARUNE_Compatibility_Merge_Deltamod_v1.0.7.zip`
- **SHA-256:** `f6d0fa4ed8328b283f47fc95ebb5ff0c2c17527bede19a56c2c5bacf1f3e3b61`
- **Format:** Deltamod package
- **Target:** DELTARUNE 1.49 Windows launcher and Chapters 1–5 using the exact source hashes recorded in `package/meta.json`

The completed release is preserved in this repository as a **project folder**. The connected GitHub writer cannot transfer the 14 MB binary ZIP from the current workspace, so the repository stores the complete release metadata, Deltamod patch map, output hashes, binary manifest, changelog, credits, and validation results instead of pretending an empty or partial ZIP is installable.

The binary payload is intentionally represented by [`BINARY-MANIFEST.md`](BINARY-MANIFEST.md). Do not substitute placeholder patch files: every xdelta and override must match the listed SHA-256 before packaging.

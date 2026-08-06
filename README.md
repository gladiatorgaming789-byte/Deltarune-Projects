# Deltarune Projects

This branch collects completed DELTARUNE mod projects.

## Workflow

- Development, editing, compilation, and testing happen in the current workspace rather than in temporary GitHub branches.
- Finished projects are published to the `all-projects` branch only after validation is complete.
- Every standalone mod lives in its own directory under `mods/<mod-name>/`.
- Each mod directory should contain its Deltamod-compatible release package, source patch scripts, documentation, and test report.
- Updates to an existing mod stay inside that mod's existing folder.
- Merged compatibility builds use their own clearly named folder under `mods/`.
- Do not create temporary, feature, agent, or release branches for mod development.
- Do not commit original DELTARUNE executables, `data.win` files, music, or other copyrighted game assets.

All published mods and merged builds must remain compatible with Deltamod.

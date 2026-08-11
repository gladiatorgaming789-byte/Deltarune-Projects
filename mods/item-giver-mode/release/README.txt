Item Giver Mode v0.3.1
======================

Fix release for the broken v0.3.0 Controls integration.

Controls
--------
Default Item Giver key: I

Open DELTARUNE's normal Controls screen. ITEM GIVER is a real selectable row
below Finish. Confirm ITEM GIVER, then press an unused keyboard key. Escape
cancels rebinding. Reset to default restores I.

The selected key is stored per save slot in keyconfig_<slot>.ini under:

  [ITEM_GIVER]
  KEYBOARD=<key code>

Item Giver menu
---------------
Configured Item Giver key : Open / close
Left / Right               : Change category
Up / Down                  : Move selection
Page Up / Page Down        : Jump ten entries
Home / End                 : First / last entry
Z / Enter                  : Give selected entry
R                          : Refresh definitions
X / Escape                 : Close

v0.3.1 architecture
-------------------
v0.3.0 tried to emulate an extra Controls row from obj_time Draw GUI End. That
pseudo-row did not behave like DELTARUNE's actual Controls menu and the large
injected body also made obj_time's local-variable metadata fragile when merged.

v0.3.1 instead patches the real obj_darkcontroller Controls Step/Draw code and
moves Item Giver's runtime into the uniquely named scr_gg_itemgiver_runtime
script. obj_time Draw GUI End contains only one helper call.

The five patches are Deltamod-native g3mpatch files. UTMT sound serialization
noise was removed before release so the package does not claim unrelated audio
resources as Item Giver changes.

Compatibility
-------------
Secret Boss Challenge remains a separate mod. The v0.3.1 design removes the
local-heavy obj_time implementation implicated in the reported bbox_top crash.
The exact current v0.3.1 + Secret Boss Challenge v0.4.2 binary merge could not
be rerun in this workspace because the SBC v0.4.2 patch binary was not locally
available, so a combined Deltamod run is still an important final user test.

Package ID: github.itemgivermode.gladiatorgaming
Target: DELTARUNE Windows full release v23, Chapters 1-5
License: MIT

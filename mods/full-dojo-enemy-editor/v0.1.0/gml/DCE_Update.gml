/// DCE_Update()
/// Per-frame editor state. UI integration calls this from obj_dojomanage.

DCE_Init();

if (!global.dce_editor_open) {
    return;
}

// The final UI implementation is intentionally kept separate from the data API.
// This function owns navigation/selection state so the editor can later be
// rendered without changing the custom enemy data representation.

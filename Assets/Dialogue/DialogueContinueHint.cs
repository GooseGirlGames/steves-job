public class DialogueContinueHint : ContinueHint {
    public override bool IsShown() {

        // Temporary, in the future we might place the `E` hint next to the actionBoxes?
        // This is just to avoid confusion for now
        if (!DialogueManager.Instance.CanBeAdvancedByKeyPress()) {
            bool loreBoxHintVisible = DialogueManager.Instance.IsDialogueActive()
                    && !DialogueManager.Instance.CanBeAdvancedByKeyPress()
                    && InventoryCanvasSlots.Instance.IsLoreVisible();
            return !loreBoxHintVisible;
        }

        return DialogueManager.Instance.CanBeAdvancedByKeyPress();
    }
}
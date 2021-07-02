public class LoreBoxSelectHint : ContinueHint {
    public override bool IsShown() {
        return DialogueManager.Instance.IsDialogueActive()
                &&!DialogueManager.Instance.CanBeAdvancedByKeyPress()
                && InventoryCanvasSlots.Instance.IsLoreVisible();
    }
}
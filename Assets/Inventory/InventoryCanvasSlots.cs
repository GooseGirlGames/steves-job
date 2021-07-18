using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryCanvasSlots : MonoBehaviour {
    private List<string> SCENES_WITH_INVENTORY_DISABLED = new List<string> {
        "MenuScene",
        "BloodFalling",
        "JumpMiniGame",
        "MusicMiniGame"
        "IntroScene",
        "OutroScene",
    };

    public List<InventorySlot> slots;
    public Canvas canvas;
    public bool visible = false;

    public GameObject dialogueOptionBoxes;
    public LoreboxUI itemLoreBox;
    public Animator itemLoreBoxAnimator;
    

    public static InventoryCanvasSlots Instance = null;
    private Button firstItemBoxButton = null;


    public Animator toolBeltAnimator;
    private const string INVENTORY_LOCK_TAG = "Inventory";
    private bool loreVisible = false;
    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
        Clear();
        canvas.enabled = false;
    }
    private void SetActionBoxesActive(bool active) {
        // not that big of a problem anymore, huh?
        dialogueOptionBoxes.transform.localScale =
                active ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
        var actionButtons = dialogueOptionBoxes.GetComponentsInChildren<Button>();
        foreach (Button button in actionButtons) {
            button.interactable = active;
        }
    }
    public void ShowItemLoreBox(Item item) {
        if (!Inventory.Instance.HasItem(item)) {
            HideItemLoreBox();
        }
        itemLoreBox.gameObject.SetActive(true);
        itemLoreBoxAnimator.SetInteger("World", (int) item.originWorld);
        SetActionBoxesActive(false);
        itemLoreBox.DisplayLore(item);
        SetLoreBoxRenderers(true);
        loreVisible = true;
    }

    public void HideItemLoreBox() {
        SetLoreBoxRenderers(false);
        SetActionBoxesActive(true);
        itemLoreBox.gameObject.SetActive(false);
        loreVisible = false;
    }

    private void SetLoreBoxRenderers(bool enabled) {
        foreach (Renderer r in itemLoreBox.GetComponentsInChildren<Renderer>()) {
            r.enabled = enabled;
        }
    }

     public void CheckForSelectedItem() {
        foreach (InventorySlot slot in slots) {
            if(slot.button.IsActive() && slot.button.Selected) {
                slot.SetHighlightBorder(true);
                foreach (InventorySlot otherSlot in slots) {
                    if (otherSlot != slot) {
                        otherSlot.SetHighlightBorder(false);
                    }
                }
                ShowItemLoreBox(slot.item);
                return;
            }
        }

        foreach (InventorySlot slot in slots) {
            slot.SetHighlightBorder(false);
        }
        HideItemLoreBox();
    }


    private bool CanBeOpenend() {
        if (SCENES_WITH_INVENTORY_DISABLED.Contains(SceneManager.GetActiveScene().name)) {
            return false;
        }

        if (DialogueManager.Instance.IsDialogueActive()) {
            return DialogueManager.Instance.CurrentSentenceHasItemOption();
        }
        return true;  // no dialogue active
    }

    private void Update() {
        if (PauseMenu.IsPausedOrJustUnpaused()) return;

        // Let `ESC` close inventory after it opened due to "Select Item".
        if (IsShowing() && Input.GetKeyDown(KeyCode.Escape)) {
            Hide();
        }

        if (Input.GetButtonDown("Inventory") && CanBeOpenend()) {
            if (visible) {
                Hide();
            } else {
                Show();
                if (firstItemBoxButton != null) {
                    StartCoroutine(UIUtility.SelectButtonLater(firstItemBoxButton));
                }
            }
        }

        if (visible) {
            CheckForSelectedItem();
        }

    }

    public void Show() {
            DialogueManager.Instance.ClearHint();

            stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
            steve.Lock(INVENTORY_LOCK_TAG);

            foreach (InventorySlot slot in slots) {
                slot.button.enabled = true;
                slot.button.Selected = false;
                slot.button.interactable = true;
            }

            DisplayItems(Inventory.Instance.items);
            visible = true;
            canvas.enabled = true;
    }

    public void Hide() {
            DisplayItems(Inventory.Instance.items);
            visible = false;
            canvas.enabled = false;
            HideItemLoreBox();

            foreach (InventorySlot slot in slots) {
                slot.button.Selected = false;
                slot.button.interactable = false;
                slot.button.enabled = false;
            }

            stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
            steve.Unlock(INVENTORY_LOCK_TAG);

            if (DialogueManager.Instance.IsDialogueActive()) {
                SetActionBoxesActive(true);
                DialogueManager.Instance.SelectItemActionBox();
            }
    }

    private void Clear() {
        foreach (InventorySlot slot in slots) {
            slot.image.enabled = false;
            slot.image.sprite = null;
            slot.button.gameObject.SetActive(false);
        }
        firstItemBoxButton = null;
    }

    public void DisplayItems(List<Item> items) {
        Clear();
        List<Item> visibleItems = new List<Item>();
        foreach (Item item in items) {
            if (item.visible) {
                visibleItems.Add(item);
            }
        }
        toolBeltAnimator.SetInteger("NumItems", visibleItems.Count);
        int n = Mathf.Min(slots.Count, visibleItems.Count);
        for (int i = 0; i < n; ++i) {
            slots[i].image.enabled = true;
            slots[i].image.sprite = visibleItems[i].icon;
            slots[i].item = visibleItems[i];
            slots[i].button.gameObject.SetActive(true);
            if (i == 0) {
                firstItemBoxButton = slots[i].button;
            }
        }
    }

    public bool IsLoreVisible() {
        return loreVisible;
    }

    public void SetSlotButtonsInteractable(bool interactable) {
        foreach (InventorySlot slot in slots) {
            slot.button.interactable = interactable;
        }
    }

    public bool IsShowing() {
        return canvas.enabled;
    }

    public void SelectFirstItemBoxButton() {
        if (firstItemBoxButton == null) {
            Debug.LogWarning("First item box button of inventory must not be null");
        } else {
            StartCoroutine(UIUtility.SelectButtonLater(firstItemBoxButton));
        }
    }
}

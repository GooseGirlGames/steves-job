using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryCanvasSlots : MonoBehaviour
{
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
    public void SetActionBoxVisibility(bool visible) {
        // not that big of a problem anymore, huh?
        dialogueOptionBoxes.transform.localScale =
                visible ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
    }
    public void ShowItemLoreBox(Item item) {
        if (!Inventory.Instance.HasItem(item)) {
            HideItemLoreBox();
        }
        itemLoreBoxAnimator.SetInteger("World", (int) item.originWorld);
        SetActionBoxVisibility(false);
        itemLoreBox.DisplayLore(item);
        itemLoreBox.gameObject.SetActive(true);
        loreVisible = true;
    }

    public void HideItemLoreBox() {
        SetActionBoxVisibility(true);
        itemLoreBox.gameObject.SetActive(false);
        loreVisible = false;
    }

     public void CheckForSelectedItem() {
        foreach (InventorySlot slot in slots) {
            if(slot.button.IsActive() && slot.button.Selected) {
                ShowItemLoreBox(slot.item);
                return;
            }
        }
        HideItemLoreBox();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) && !DialogueManager.Instance.IsDialogueActive()) {
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

        /*if (loreVisible) {
            if (Input.GetKeyDown(KeyCode.D)) {
                foreach (InventorySlot slot in slots) {
                    slot.button.Selected = false;
                }
                HideItemLoreBox();
            }
        }*/
    }

    public void Show() {
            stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
            steve.Lock(INVENTORY_LOCK_TAG);
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
            }
            stevecontroller steve = GameObject.FindObjectOfType<stevecontroller>();
            steve.Unlock(INVENTORY_LOCK_TAG);
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
}

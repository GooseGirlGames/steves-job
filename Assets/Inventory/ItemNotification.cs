using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemNotification : MonoBehaviour {
    public Canvas canvas;
    public TextMeshProUGUI text;
    public Image image;
    public Image bgImage;
    private enum NotifyType {
        recieved,
        lost,
        restored
    }
    private struct ItemNotify {
        public Item item;
        public NotifyType type;
    }
    private Coroutine currentNotification;
    private Queue<ItemNotify> notificationQueue = new Queue<ItemNotify>();
    public float secondsShown;
    public float delayInBetween;
    public static ItemNotification Instance = null;
    private void Awake() {
        if (Instance == null)
            Instance = this;
        canvas.enabled = false;
    }
    private IEnumerator Notify(ItemNotify n) {
        string prefix = "";
        Color color = Color.black;
        if (n.type == NotifyType.recieved) {
            prefix = "Recieved:\n";
            color = new Color(0.5f, 1, 0.5f);
        } else if (n.type == NotifyType.lost) {
            prefix = "Lost:\n";
            color = new Color(1, 0.4f, 0.4f);
        } else if (n.type == NotifyType.restored) {
            prefix = "";
            color = new Color(1, 0.89f, 0.16f);
        }

        text.text = Uwu.OptionalUwufy(prefix);
        text.text += Uwu.OptionalUwufy(n.item.name);
        image.sprite = n.item.icon;
        bgImage.color = color;
        canvas.enabled = true;
        yield return new WaitForSeconds(secondsShown * (n.type == NotifyType.restored ? 2 : 1));
        if (delayInBetween > 0.01f || notificationQueue.Count == 0) {
            // stop flicker
            canvas.enabled = false;
        }   
        yield return new WaitForSeconds(delayInBetween);
        currentNotification = null;
    }
    private void Update() {
        if (notificationQueue.Count == 0 || currentNotification != null) {
            return;
        }
        currentNotification = StartCoroutine(Notify(notificationQueue.Dequeue()));
    }
    public void NotifyItem(Item item, bool recieved) {
        if (!item.visible) {
            if (item.isRestoreItem) {
                RestoreNotify(item);
            }
            return;
        }
        var n = new ItemNotify();  // are structs in C# really this stupid?
        n.item = item;
        n.type = recieved ? NotifyType.recieved : NotifyType.lost;
        notificationQueue.Enqueue(n);
    }
    private void RestoreNotify(Item item) {
        var n = new ItemNotify();
        n.type = NotifyType.restored;
        n.item = new Item();  // building a little fake item
        n.item.icon = item.icon;
        n.item.name = Inventory.Instance.HasItem(item.restoreItemCounterpart) ? "Fully" : "Partly";
        n.item.name += " restored ";
        n.item.name += item.name;
        notificationQueue.Enqueue(n);
    }
}

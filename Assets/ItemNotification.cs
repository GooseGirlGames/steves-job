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
    private struct ItemNotify {
        public Item item;
        public bool recieved;
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
        text.text = Uwu.OptionalUwufy(n.recieved ? "Recieved:\n" : "Lost:\n");
        text.text += Uwu.OptionalUwufy(n.item.name);
        image.sprite = n.item.icon;
        bgImage.color = n.recieved ? new Color(0.5f, 1, 0.5f) : new Color(1, 0.4f, 0.4f);
        canvas.enabled = true;
        yield return new WaitForSeconds(secondsShown);
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
            return;
        }
        var n = new ItemNotify();  // are structs in C# really this stupid?
        n.item = item;
        n.recieved = recieved;
        notificationQueue.Enqueue(n);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition {
    private List<Item> m_mustHave = new List<Item>();
    private List<Item> m_mustNotHave = new List<Item>();

    public bool IsMet() {
        foreach (Item item in m_mustHave)
            if (!Inventory.Instance.HasItem(item)) return false;
        foreach (Item item in m_mustNotHave)
            if (Inventory.Instance.HasItem(item)) return false;
        return true;
    }

    public Condition MustHave(Item item) {
        m_mustHave.Add(item);
        return this;
    }
    public Condition MustNotHave(Item item) {
        m_mustNotHave.Add(item);
        return this;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtDialouge : DialogueTrigger
{   
    public Dialogue dirt;
    public Item rgb;

    public void cleaning(){
        Destroy(this.gameObject);
    }

    public void wantsRGB(){
        Inventory.Instance.AddItem(rgb);
    }

    public override Dialogue GetActiveDialogue() {
        return dirt;

    }

    void Update()
    {
        
    }
}

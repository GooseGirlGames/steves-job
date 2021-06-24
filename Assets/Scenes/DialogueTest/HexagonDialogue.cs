using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDialogue : Dialogue
{
    public HexagonDialogue() {
        Debug.Log("Creating Hexagondialogue");
        
        Say("Hewwo")
            .Do(GiveItem(ItemManager.bucked));

        Say("Uwu")
            .If(DoesNotHaveItem(ItemManager.shirt))
            .Do(() => { Debug.Log("toll"); });

        Say("Na ja")
            .Do(new TriggerDialogueAction<HexagonDialogue>());
            // IDEAS FOR OPTION INTERFACE:
            // Triggers other Dialogue:
            //.Choose(new ItemOption<OtherHexagonDialogue>(ItemManager.bucked))
            // Triggers arbitrary code:
            //.Choose(new TextOption("Cooler Text", () => {
            //        // some code goes here
            //}))

    }
}

public class OtherHexagonDialogue : Dialogue {
    public OtherHexagonDialogue() {
        Say("hm");
    }
}
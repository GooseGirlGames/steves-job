using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDialogue : Dialogue
{
    public HexagonDialogue() {
        Debug.Log("Creating Hexagondialogue");
        Say("Hewwo");
        Say("Uwu")
            .If(new DoesNotHaveItem(ItemManager.shirt))
            .Do(() => { Debug.Log("toll"); });
        Say("Na ja")
            .Do(new TriggerDialogueAction<HexagonDialogue>());
    }
}

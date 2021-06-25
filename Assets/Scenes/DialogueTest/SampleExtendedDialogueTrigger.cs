using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleExtendedDialogueTrigger : DialogueTrigger
{
    public Dialogue bye;
    private bool saidHello = false;
    public Item bucket;
    public Item shirt;
    public override Dialogue GetActiveDialogue() {
        return new HexagonDialogue();
    }
}


public class HexagonDialogue : Dialogue
{
    public HexagonDialogue() {
        Debug.Log("Creating Hexagondialogue");

        // This allows access to the trigger, which can be useful for linking game objects.
        // Currently, this is used for items, which in the future will be done through ItemManager.
        // Still, accessing the trigger might come in handy anyway.
        SampleExtendedDialogueTrigger trigger =
                (SampleExtendedDialogueTrigger) SampleExtendedDialogueTrigger.Instance;
        
        Say("Hewwo")
            .Do(GiveItem(trigger.bucket));

        Say("I won't say this")
            .If(() => false);

        Say("I will say this tho")
            .If(() => true);

        Say("Has shirt")
            .If(HasItem(trigger.shirt));

        Say("Uwu (no shirt)")
            .If(DoesNotHaveItem(trigger.shirt))
            .Do(() => { Debug.Log("toll"); });

        Say("Na ja, hier ist ein Shirt")
            .Do(new TriggerDialogueAction<OtherHexagonDialogue>())
            .Do(GiveItem(trigger.shirt))
            .If(DoesNotHaveItem(trigger.shirt));
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
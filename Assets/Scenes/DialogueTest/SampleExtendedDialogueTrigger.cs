using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleExtendedDialogueTrigger : DialogueTrigger
{
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
        ShirtChooseDialogue.trigger = trigger;
        
        Say("?")
            .Choice(new TextOption("I know!"))
            .Choice(new TextOption("I know!"))
            .Choice(new TextOption("I know!"))
            .Choice(new TextOption("I know!"));

        Say("Hewwo")
            .Do(GiveItem(trigger.bucket));
            //.DoAfter(RemoveItem(trigger.bucket));

        Say("I won't say this")
            .If(() => false);

        Say("I will say this tho")
            .If(() => true);

        Say("Has shirt")
            .If(HasItem(trigger.shirt))
            .Choice(new TextOption("I know!"))
            .Choice(new TextOption("dugh.."));


        Say("Uwu (no shirt)")
            .If(DoesNotHaveItem(trigger.shirt))
            .Do(() => { Debug.Log("toll"); });
            
        EmptySentence().DoAfter(new TriggerDialogueAction<ShirtChooseDialogue>());

        Say("Bye!!!!");
    }
}

public class NoUseForThisDialogue : Dialogue {
    public NoUseForThisDialogue() {
        Say("Was soll ich denn damit?!")
            .DoAfter(new TriggerDialogueAction<ShirtChooseDialogue>());
    }
}

public class ShirtChooseDialogue : Dialogue {

    public static SampleExtendedDialogueTrigger trigger;
    public ShirtChooseDialogue() {

            Say("Na ja, hier ist ein Shirt")
            .Choice(
                new TextOption("Shirt klauen")
                .IfChosen(GiveItem(trigger.shirt))
                .AddCondition(DoesNotHaveItem(trigger.shirt))
                .IfChosen(new TriggerDialogueAction<CoolerTextDialogue>(exitCurrent: true))
            )
            .Choice(
                new ItemOption(trigger.shirt)
                .IfChosen(RemoveItem(trigger.shirt))
                .IfChosen(new TriggerDialogueAction<ShirtGivenDialogue>())
            )
            .Choice(
                new TextOption("Bye!!!")
            )
            .Choice(
                new OtherItemOption().IfChosen(new TriggerDialogueAction<NoUseForThisDialogue>())
            );
    }
}

public class OtherHexagonDialogue : Dialogue {
    public OtherHexagonDialogue() {
        Say("Danke fürs Shirt!");
    }
}

public class CoolerTextDialogue : Dialogue {
    public CoolerTextDialogue() {
        Say("Cooler text indeed!");
    }
}

public class ShirtGivenDialogue : Dialogue {
    public ShirtGivenDialogue() {
        Say("Danke für das Shirt!");
    }
}

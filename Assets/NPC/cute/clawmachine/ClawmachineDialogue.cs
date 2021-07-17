using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawmachineDialogue : DialogueTrigger {
    public const string CLAWMACHINE_NAME = "AQUACLAW";
    public static ClawmachineDialogue t;
    public Item item_for_sale;
    public Item startcoin;
    public Item cutecoin;
    public Item horrorcoin;
    public Item machine_used;
    public List<Animator> animators;
    private void SetAnimatorTrigger(string trigger) {
        foreach (var a in animators) a.SetTrigger(trigger);
    }
    public Sprite avatar_before;
    public Sprite avatar_after;

    private void Awake() {
        this.name = CLAWMACHINE_NAME;
        GetActiveDialogue();  // Set animation triggers
    }

    public override Dialogue GetActiveDialogue() {
        ClawmachineDialogue.t = this;
        avatar = avatar_before;

        if (Inventory.Instance.HasItem(machine_used)) {
            t.SetAnimatorTrigger("Empty");
            avatar = avatar_after;
            return new Bye();
        }

        return new Welcome();
    }

    public class BuyItem : Dialogue {
        public BuyItem() {
            ClawmachineDialogue t = ClawmachineDialogue.t;

            EmptySentence().Do(new TriggerDialogueAction<Welcome>());

            Say(
                "Want to give the amazing, guaranteed to not be cursed "
                + CLAWMACHINE_NAME + " a try? It's just 25 cents!"
                )
            .Choice(
                new ItemOption(t.startcoin)
                .IfChosen(RemoveItem(t.startcoin))
                .IfChosen(new DialogueAction(() => t.SetAnimatorTrigger("Dispense")))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t.machine_used))
                .IfChosen(new DialogueAction(() => t.GetComponent<AudioSource>().Play()))
            )
            .Choice(
                new ItemOption(t.horrorcoin)
                .IfChosen(RemoveItem(t.horrorcoin))
                .IfChosen(new DialogueAction(() => t.SetAnimatorTrigger("Dispense")))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t.machine_used))
                .IfChosen(new DialogueAction(() => t.GetComponent<AudioSource>().Play()))
            )
            .Choice(
                new ItemOption(t.cutecoin)
                .IfChosen(RemoveItem(t.cutecoin))
                .IfChosen(new DialogueAction(() => t.SetAnimatorTrigger("Dispense")))
                .IfChosen(GiveItem(t.item_for_sale))
                .IfChosen(GiveItem(t.machine_used))
                .IfChosen(new DialogueAction(() => t.GetComponent<AudioSource>().Play()))
            )
            .Choice(
                new TextOption("No")
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<NotACoin>())
            )
            .DoAfter(new TriggerDialogueAction<Bye>());
        }
    }

    public class Welcome : Dialogue {
        public Welcome() {
            Say("Welcome to the " + CLAWMACHINE_NAME + "!")
            .DoAfter(new TriggerDialogueAction<BuyItem>());
        }
    }

    public class Bye : Dialogue {
        public Bye() {
            ClawmachineDialogue t = ClawmachineDialogue.t;
            t.avatar = t.avatar_after;

            Say("Thank you for using " + CLAWMACHINE_NAME + "!");

            Say("Have fun with your brand new " + t.item_for_sale.name + "!")
            .If(HasItem(t.item_for_sale));
        }
    }

    public class NotACoin : Dialogue {
        public NotACoin() {
            ClawmachineDialogue t = ClawmachineDialogue.t;

            Say(
                "Sadly, I cannot accept "
                + DialogueManager.Instance.currentItem.name
                + " as currency."
            )
            .DoAfter(new TriggerDialogueAction<BuyItem>());
        }
    }

}

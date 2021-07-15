using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDialogue : DialogueTrigger {
    public Item magpie_item;
    public Item goose;
    public Item goosecute;
    public Item goosebloody;
    public Item goosebloodycute;
    public Item _magpie_released;
    public Item _restored_hangman;
    public SpriteRenderer key_renderer;
    public Magpie magpie;
    public static KeyDialogue t;
    public override Dialogue GetActiveDialogue() {
        UpdateState();
        t = this;

        if (!Inventory.Instance.HasItem(_restored_hangman)
                && !Inventory.Instance.HasItem(_magpie_released)) {
            return new ReleaseDialogue();
        } else {
            return null;
        }
    }
    private void Awake() {
        UpdateState();
    }
    private void UpdateState() {
        key_renderer.enabled = !Inventory.Instance.HasItem(_magpie_released);
    }
    private void ReleaseMagpie() {
        magpie.Spawn();
    }

    public class ReleaseDialogue : Dialogue {
        public ReleaseDialogue() {
            Say("Nope, can't reach those keys.");
            Say("If only I could fly...")
            .Choice(new TextOption("Nope"))
            .Choice(
                new ItemOption(t.magpie_item)
                .IfChosen(RemoveItem(t.magpie_item))
                .IfChosen(GiveItem(t._magpie_released))
                .IfChosen(new DialogueAction(t.ReleaseMagpie))
            )
            .Choice(
                new ItemOption(t.goose)
                .IfChosen(new TriggerDialogueAction<Goose>())
            )
            .Choice(
                new ItemOption(t.goosecute)
                .IfChosen(new TriggerDialogueAction<Goose>())
            )
            .Choice(
                new ItemOption(t.goosebloody)
                .IfChosen(new TriggerDialogueAction<Goose>())
            )
            .Choice(
                new ItemOption(t.goosebloodycute)
                .IfChosen(new TriggerDialogueAction<Goose>())
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<CantFly>())
            );
        }
    }
    public class CantFly : Dialogue {
        public CantFly() {
            string item = DialogueManager.Instance.currentItem.name;
            Say("A " + item + " can't fly!");
        }
    }

    public class Goose : Dialogue {
        public Goose() {
            Say("Why would a *goose* fetch me those shiny keys?");
        }
    }
}

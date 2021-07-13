using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDialogue : DialogueTrigger {
    public Item magpie;
    public Item goose;
    public Item goosecute;
    public Item goosebloody;
    public Item goosebloodycute;
    public Item _magpie_released;
    public SpriteRenderer key_renderer;
    public static KeyDialogue t;
    public override Dialogue GetActiveDialogue() {
        UpdateState();
        t = this;
        if (!Inventory.Instance.HasItem(_magpie_released)) {
            return new ReleaseDialogue();
        } else {
            return new WasReleasedDialogue();
        }
    }
    private void Awake() {
        UpdateState();
    }
    private void UpdateState() {
        key_renderer.enabled = !Inventory.Instance.HasItem(_magpie_released);
    }

    public class ReleaseDialogue : Dialogue {
        public ReleaseDialogue() {
            Say("Nope, can't reach those keys.");
            Say("If only I could fly...")
            .Choice(
                new ItemOption(t.magpie)
                .IfChosen(RemoveItem(t.magpie))
                .IfChosen(GiveItem(t._magpie_released))
                .IfChosen(new TriggerDialogueAction<WasReleasedDialogue>())
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
    public class WasReleasedDialogue : Dialogue {
        public WasReleasedDialogue() {
            Say("Oof, it's just stolen the keys and flown away...").Do(t.UpdateState);
        }
    }

    public class Goose : Dialogue {
        public Goose() {
            Say("Why would a *goose* fetch my those shiny keys?");
        }
    }
}

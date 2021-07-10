using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorPeasants : DialogueTrigger
{
    public Item _spineless_and_lifeless;

    public static HorrorPeasants h;

    void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue() {
        HorrorPeasants.h = this;

        if (! Inventory.Instance.HasItem(_spineless_and_lifeless)) {
            return new HorrorPeasantsHi();
        }
        return new HorrorPeasantsOther();
    }

    public class HorrorPeasantsHi : Dialogue {
        public HorrorPeasantsHi() {
            Say("Helllow ");
            Say("The guy running this shop has some really good stuff");
            Say("Ouch!");
            Say("Our friend over there seems to have some problems though");
            Say("Maybe you can look after him, we are busy at the moment");
            Say("Aaaargh");
        }
    }

    public class HorrorPeasantsOther : Dialogue {
        public HorrorPeasantsOther() {
            Say("Hey thaaanks");
            Say("Why don't you come join us some time");
            Say("Owww");
        }
    }
}

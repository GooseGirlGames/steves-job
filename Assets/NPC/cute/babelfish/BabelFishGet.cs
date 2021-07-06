using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabelFishGet : DialogueTrigger
{
    public Item babelfish;
    public override Dialogue GetActiveDialogue() {
        return new BabelFishGetDia(babelfish);
    }
}

public class BabelFishGetDia : Dialogue {
    public BabelFishGetDia(Item babelfish) {
        Say(Uwu.Uwufy("Hello, my dear friend."))
        .Choice(
            new TextOption("Uhm, okay...?")
            .IfChosen(GiveItem(babelfish))
        );
    }
}
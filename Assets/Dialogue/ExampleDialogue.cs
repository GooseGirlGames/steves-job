using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogue : MonoBehaviour {
    public Item bucket;
    public static DialogueElement root =
        new Sentence("Rude")
        .Append(new Sentence("Dude"))
        .Append(new Sentence("Yo, Du hast nen Eimer!")).Condition(new Condition().MustHave(bucket))
        .Append(new Sentence("Kein Eimer, tjap!")).Condition(new Condition().MustNotHave(bucket))
        .Append(new Options("Wie geht's?")
            .AddOption(
                (Option) new TextOption("Blubb")
                .Append(new Sentence("A"))
                .Append(new Action(() => Debug.Log("Blubb")))
            )
            .AddOption(
                (Option) new ItemOption(bucket)
                .Append(new Action(() => Inventory.Instance.RemoveItem(bucket)))
                .Append(new Sentence("Danke fuer den Eimer!"))
            )
            .AddOption(
                (Option) new TextOption("Wie bitte")
                .Append(ExampleDialogue.root)
            )
        );

    public void Awake() {
        while (root.HasNext()) {
            DialogueElement elem = root.Next();

            if (!elem.ConditionsMet()) {
                continue;
            }

            if (elem is Sentence) {
                Sentence s = (Sentence) elem;
                Debug.Log(s.Text);
            }
        }
    }
}
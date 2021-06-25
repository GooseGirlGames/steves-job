using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/**
 * A Dialogue is a collection of sentences that are to be displayed in order.
 *
 * Position of dialogue box:
 * Create an empty GameObject as a child of the character sprite and place it a
 * bit above its head.  Set this GameObject as `diaboxPosition`.  The bottom of
 * the dialogue box will touch the `diaboxPosition` object.
 */
[Serializable]
public abstract class Dialogue
{
    // no conditions!  GetActiveDialogue handles this
    public List<Sentence> sentences = new List<Sentence>();  // linear; branching = triggering new dialogues

    protected Sentence Say(string text) {
        Sentence sentence = new Sentence(text);
        sentences.Add(sentence);
        return sentence;
    }

    protected EmptySentence EmptySentence() {
        EmptySentence sentence = new EmptySentence();
        sentences.Add(sentence);
        return sentence;
    }

    protected DialogueAction GiveItem(Item item) {
        return new DialogueAction(() => {
            Inventory.Instance.AddItem(item);
        });
    }

    protected DialogueAction RemoveItem(Item item) {
        return new DialogueAction(() => {
            Inventory.Instance.RemoveItem(item);
        });
    }

    protected DialogueCondition DoesNotHaveItem(Item item) { // this one saves a 'new'! how efficient!
        return new DoesNotHaveItem(item);
    }

    protected DialogueCondition HasItem(Item item) { // this one saves a 'new'! how efficient!
        return new HasItem(item);
    }

    // TODO more of these shorthands?
}

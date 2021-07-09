# The Jank-o-Matic Dialogue System

This is our little custom dialogue system.  It should work well enough.

## Overview of components

*   `DialogueManager`:
    Top-level, global.  Handles display on the UI canvas and queues up
    sentences and triggers events of sentences and options.
*   `DialogueTrigger`:
    Behavior given to characters.  Manages one or more dialogue(s) for
    that character.
*   A `Dialogue` consists of multiple `Sentence`s that are displayed in
    order.  A sentence can trigger events (`onComplete`) and prompt the
    player for `DialogueOption`s


## Script Format

Requirements:
* Display dialogue sentences
* Conditions:
  * player has item/does not have item
  * NPC in a specific world
  * counter values
* Actions:
  * give/remove item
  * teleport
  * trigger arbitrary UnityEvent
  * increment/decrement counters
* Give player options to choose from:
  * text-based options
  * Allow selection of items
  * supports conditions
* Instant and keypress triggered dialogues
* Sentence pools:  Instead of a single sentence, allow a set of
  sentences to be defined, from which one sentence is selected at
  random.
  * Optional: Only show sentences that have not yet been said.
  * Optional: If all sentences have been said, output a default
    sentence.

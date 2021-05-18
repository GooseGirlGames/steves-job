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

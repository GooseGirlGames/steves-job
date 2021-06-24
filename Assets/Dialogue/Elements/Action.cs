using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : DialogueElement {
    public delegate void Run();
    Run run;
    public Action(Run func) {
        run = func;
    }
}
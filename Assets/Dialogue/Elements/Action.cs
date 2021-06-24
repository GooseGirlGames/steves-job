using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : DialogueElement {
    public delegate void Run();
    public Run run;
    public Action(Run func) {
        run = func;
        m_isBase = false;
    }
}
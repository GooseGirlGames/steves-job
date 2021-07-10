using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandymanDialogue : VoidNPCDiaTrigger {
    private static HandymanDialogue t;
    public override void UpdateStaticT() {
        t = this;
    }

    public override Dialogue NewFullRestoredDia() => new FullRestoredDia();
    public override Dialogue NewHalfRestoredDia() => new HalfRestoredDia();
    public override Dialogue NewGoneDia() => new GoneDia();



    public class GoneDia : Dialogue {
        public GoneDia() {
            Say("...");
        }
    }
    public class HalfRestoredDia : Dialogue {
        public HalfRestoredDia() {
            Say("What is happening... Why can i just feel half of myself...");
        }
    }
    public class FullRestoredDia : Dialogue {
        public FullRestoredDia() {
            Say("Thank you Janitor :D");
        }
    }

}
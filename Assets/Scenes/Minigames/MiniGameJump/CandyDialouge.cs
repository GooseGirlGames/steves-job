using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDialouge : DialogueTrigger
{   
    public Portal portalToMiniGame;
    public Item sweetDirt;
    public Item mop;
    public GameObject racoon;
/*     public Transform player;
    private float racoon_pos;
    private float player_pos; */

    public void Start()
    {
        Inventory.Instance.AddItem(mop);
        racoon.SetActive(false);
    }

    public void racoonActivated(){
        racoon.SetActive(true);
/*         racoon_pos = racoon.transform.position.x;
        player_pos = player.position.x;
        while(racoon_pos != player_pos - 3.0f){
            racoon.transform.position.x = racoon_pos + 0.1f;
        } */

    }

    public void floorCleaned(){
        Inventory.Instance.AddItem(sweetDirt);
        racoonActivated();
        Destroy(this.gameObject);
    }
    [SerializeField]
    public Dialogue hasMopDia;
    public Dialogue needMopDia;

    public override Dialogue GetActiveDialogue() {
        if (Inventory.Instance.HasItem(mop)) {
            return hasMopDia;
            
        }
       return needMopDia; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoreboxUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    public new TextMeshProUGUI name;
    public Image image;

    public void DisplayLore(Item item){
        text.SetText(Uwu.OptionalUwufy(item.description));
        name.SetText(Uwu.OptionalUwufy(item.name));
        image.sprite = item.icon;
    }    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

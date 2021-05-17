using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueOptionUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    [HideInInspector]
    public DialogueOption option;
    public void OptionChosen() {
        DialogueManager.Instance.DisplayNextSentence(option);
    }
}

using System.Collections;
using UnityEngine.UI;


public class UIUtility {
    // https://answers.unity.com/questions/1142958/buttonselect-doesnt-highlight.html
    public static IEnumerator SelectButtonLater(Button button) {
        yield return null;
        GameManager.Instance.EventSystem.SetSelectedGameObject(null);
        GameManager.Instance.EventSystem.SetSelectedGameObject(button.gameObject);
    }
}
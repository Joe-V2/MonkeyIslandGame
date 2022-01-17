using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyHintController : MonoBehaviour
{
    public Text hintText;
    public Text hintKey;
    public Image hintBG;
    public static KeyHintController instance;

    public void OnEnable()
    {
        instance = this;
    }

    public void ShowHint(KeyCode key, string action, bool visible = true)
    {
        hintText.text = action;
        hintKey.text = key.ToString();

        hintBG.enabled = hintText.enabled = hintKey.enabled = visible;
    }


}

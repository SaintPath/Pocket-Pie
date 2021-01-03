using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private GameObject popupCard = default;
    [SerializeField]
    private Image popupBackground = default;
    [SerializeField]
    private Text popupTitle = default;
    [SerializeField]
    private Text popupText = default;
    [SerializeField]
    private Text popupButton = default;
    [SerializeField]
    private Text popupFooter = default;

    Popup popupJsonParse;

    public bool displayingPopup;
    string json;
    string dataPath;

    void Start()
    {
        dataPath = Application.streamingAssetsPath;
        json = File.ReadAllText(dataPath + "/Popup.json");
        popupJsonParse = JsonUtility.FromJson<Popup>(json);
        popupCard.SetActive(false);
        displayingPopup = false;
    }

    public void DisplayPopup()
    {
        Color bgColor;
        popupCard.SetActive(true);
        displayingPopup = true;

        popupTitle.text = popupJsonParse.title;
        popupButton.text = popupJsonParse.button_text;
        
        if(ColorUtility.TryParseHtmlString(popupJsonParse.bg_color, out bgColor)){
            popupBackground.color = bgColor;
        }

        popupText.text = "Fun: " + popupJsonParse.stats.fun + "\n" +
                        "Strong: " + popupJsonParse.stats.strong + "\n" +
                        "Evil: " + popupJsonParse.stats.evil ;

        popupFooter.text = popupJsonParse.foot_note;
    }

    public void ClosePopup()
    {
        popupCard.SetActive(false);
        displayingPopup = false;
    }

    public void ClickedMainButton()
    {
        popupJsonParse.stats.fun += 10;
        popupJsonParse.stats.strong += 10;
        popupJsonParse.stats.evil += 10;

        popupText.text = "Fun: " + popupJsonParse.stats.fun + "\n" +
                        "Strong: " + popupJsonParse.stats.strong + "\n" +
                        "Evil: " + popupJsonParse.stats.evil;

        json = JsonUtility.ToJson(popupJsonParse);
        File.WriteAllText(dataPath + "/Popup.json", json);
    }

}

[Serializable]
class Popup
{
    public string title = default;
    public string bg_color = default;
    public string button_text = default;
    public Stats stats = default;
    public string foot_note = default;
}

[Serializable]
class Stats
{
    public int fun = default;
    public int strong = default;
    public int evil = default;
}
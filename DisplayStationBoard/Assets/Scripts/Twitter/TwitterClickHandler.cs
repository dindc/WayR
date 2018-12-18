using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TwitterClickHandler : MonoBehaviour
{
    public GameObject tweetPanel;
    public GameObject tweetPrefab;
    private TextMesh textObject;
    public GameObject[] tweetbuttonlist = new GameObject[4];

    public void Start()
    {
        textObject = transform.GetComponent<TextMesh>();
    }

    public void OnClick()
    {
            switch (EventSystem.current.currentSelectedGameObject.tag)
            {
                case "trump":
                    //textObject.text = Main.tweetList[0];
                    updateButtons(Main.alltweets[0]);
                    break;
                case "tagesanzeiger":
                    //textObject.text = Main.tweetList[1];
                    updateButtons(Main.alltweets[1]);
                    break;
                case "zvv":
                    //textObject.text = Main.tweetList[2];
                    updateButtons(Main.alltweets[2]);
                    break;
                case "reuters":
                    //textObject.text = Main.tweetList[3];
                    updateButtons(Main.alltweets[3]);
                    break;
        }
    }

    void updateButtons (List<string> alltweets)
    {
        for (int i = 0; i < Main.alltweets.Count; i++)
        {
            Destroy(tweetbuttonlist[i]);
            GameObject button = (GameObject)Instantiate(tweetPrefab);
            tweetbuttonlist[i] = button;
            button.name = string.Format("{0}", i);
            button.transform.SetParent(tweetPanel.transform);

            button.transform.GetChild(0).GetComponent<RectTransform>().offsetMin = new Vector2(15, 0);
            button.transform.GetChild(0).GetComponent<Text>().text = alltweets[i];
            button.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

            button.GetComponentInChildren<Text>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        }
    }
}

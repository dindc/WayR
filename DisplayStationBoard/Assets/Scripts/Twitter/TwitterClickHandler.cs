using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TwitterClickHandler : MonoBehaviour
{
    public GameObject tweetPanel;
    public GameObject tweetPrefab;
    private TextMesh textObject;

    public void Start()
    {
        textObject = transform.GetComponent<TextMesh>();
    }

    public void OnClick()
    {
            switch (EventSystem.current.currentSelectedGameObject.tag)
            {
                case "trump":
                    textObject.text = Main.tweetList[0];

                    //updateButtons(Main.alltweets[0]);
                    break;
                case "tagesanzeiger":
                    textObject.text = Main.tweetList[1];
                    break;
                case "zvv":
                    textObject.text = Main.tweetList[2];
                    break;
                case "reuters":
                    textObject.text = Main.tweetList[3];
                    break;
            }
    }

    void updateButtons (List<string> alltweets)
    {
        for (int i = 0; i < Main.alltweets.Count; i++)
        {
            GameObject button = (GameObject)Instantiate(tweetPrefab);
            button.name = string.Format("{0}", i);
            button.transform.SetParent(tweetPanel.transform);
        }
    }
}

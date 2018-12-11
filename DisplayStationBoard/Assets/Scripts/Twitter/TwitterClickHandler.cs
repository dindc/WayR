using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TwitterClickHandler : MonoBehaviour
{
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
}

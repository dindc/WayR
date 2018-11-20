using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{

    public Button[] buttonlist = new Button[10];

    // Use this for initialization
    void Start()
    {
        foreach (var button in buttonlist)
        {
            button.onClick.AddListener(delegate { DisplayStops(button); });
        }
    }

    void DisplayStops(Button button)
    {
        string message = button.GetComponentInChildren<Text>().text;
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
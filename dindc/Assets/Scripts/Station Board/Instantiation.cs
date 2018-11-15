using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Instantiation : MonoBehaviour {

    public GameObject buttonPrefab;
    public GameObject buttonPannel;
    public static GameObject[] buttonlist = new GameObject[Manager.n];
    public static GameObject[] stoplist = new GameObject[0];

    void Start () {

        foreach (var button in stoplist)
        {
            Destroy(button);
        }

        for (int i = 0; i < Manager.n; i++) 
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.name = string.Format("{0}", i);
            button.transform.SetParent(buttonPannel.transform);
            button.GetComponent<Button>().onClick.AddListener(OnClick);

            button.transform.GetChild(0).GetComponent<Text>().text = "";
            button.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

            Text clone = Instantiate(button.transform.GetChild(0).GetComponent<Text>(), button.transform);
            clone.alignment = TextAnchor.MiddleRight;

            buttonlist[i] = button;
        }

        
    }


	void OnClick ()
    {
        foreach (var button in buttonlist)
        {
            Destroy(button);
        }

        int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        Stationboard requested = Manager.destinations[num];

        int size = requested.passList.Count;
        stoplist = new GameObject[size + 1];

        for (int k = 0; k < size; k++)
        {
            DateTime dateTime = requested.passList[k].arrival??DateTime.Now;
            string departure = dateTime.ToString("t").PadRight(15, ' ');
            string name = requested.passList[k].station.name;

            if (name == null)
            {
                name = Manager.departureStation;
            }

            GameObject stopButton = (GameObject)Instantiate(buttonPrefab);
            stopButton.name = string.Format("{0}", k);
            stopButton.transform.SetParent(buttonPannel.transform);

            stopButton.transform.GetChild(0).GetComponent<Text>().text = String.Format("  ¬ {0}{1}", departure, name);
            stopButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

            stopButton.GetComponent<Button>().interactable = false;
            stopButton.transform.GetChild(0).GetComponent<Text>().color = new Color(200, 200, 200);

            stoplist[k] = stopButton;
        }

        GameObject backButton = (GameObject)Instantiate(buttonPrefab);
        backButton.name = string.Format("{0}", size);
        backButton.transform.SetParent(buttonPannel.transform);
        backButton.GetComponent<Button>().onClick.AddListener(Start);

        backButton.transform.GetChild(0).GetComponent<Text>().text = "  Back";
        backButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

        stoplist[size - 1] = backButton;

        Manager.gettext = false;
    } 
}

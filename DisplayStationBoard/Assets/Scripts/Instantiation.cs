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
        if (stoplist.Length != 0)
        {
            for (int j = 0; j < stoplist.Length; j++)
            {
                Destroy(stoplist[j]);
            }
        }

        for (int i = 0; i < Manager.n; i++) 
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.name = string.Format("{0}", i);
            button.transform.SetParent(buttonPannel.transform);
            button.GetComponent<Button>().onClick.AddListener(OnClick);
            button.transform.GetChild(0).GetComponent<Text>().text = "Default";
            button.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            buttonlist[i] = button;
        }
    }


	void OnClick ()
    { 
        for (int i = 0; i < Manager.n; i++)
        {
            Destroy(buttonlist[i]);
        }

        int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        Stationboard requested = Manager.destinations[num];

        int size = requested.passList.Count;
        stoplist = new GameObject[size + 1];

        for (int i = 0; i < size; i++)
        {
            string name = requested.passList[i].station.name;

            if (name == null)
            {
                name = Manager.departureStation;
            }

            GameObject stopButton = (GameObject)Instantiate(buttonPrefab);
            stopButton.name = string.Format("{0}", i);
            stopButton.transform.SetParent(buttonPannel.transform);
            stopButton.transform.GetChild(0).GetComponent<Text>().text = String.Format("  {0}", name);
            stopButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            buttonlist[i] = stopButton;
        }

        GameObject backButton = (GameObject)Instantiate(buttonPrefab);
        backButton.name = string.Format("  Back");
        backButton.transform.SetParent(buttonPannel.transform);
        backButton.GetComponent<Button>().onClick.AddListener(Start);
        backButton.transform.GetChild(0).GetComponent<Text>().text = String.Format("  {0}", name);
        backButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        buttonlist[size - 1] = backButton;
    } 
}

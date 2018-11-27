using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Instantiation : MonoBehaviour {

    public static string departureStation = "Zurich";
    public string remoteUri = String.Format("http://transport.opendata.ch/v1/stationboard?station={0}&limit=10/stationboard.json", departureStation);
    private string json = "";
    public static bool gettext = true;
    public static int n = 10;
    public static Stationboard[] destinations = new Stationboard[n];

    public GameObject buttonPrefab;
    public GameObject buttonPannel;
    public static GameObject[] buttonlist = new GameObject[n];
    public static GameObject[] stoplist = new GameObject[0];

    void Start () {

        buttonlist = new GameObject[n];

        for (int i = 0; i < n; i++) 
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

        if (destinations[0] != null)
        {
            for (int i = 0; i < n; i++)
            {
                string departure = destinations[i].stop.departure.ToString("t").PadRight(15, ' ');
                string platform = destinations[i].stop.platform;
                string destination = destinations[i].to;
                string[] arr = { departure, destination };
                string output = string.Join("\t", arr);

                buttonlist[i].GetComponentInChildren<Text>().text = string.Format("  {0}", output);
                buttonlist[i].transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}  ", platform);
            }
        }

        StartCoroutine(GetText());
    }

    void Back ()
    {
        foreach (var button in stoplist)
        {
            Destroy(button);
        }

        stoplist = new GameObject[0];
        StopAllCoroutines();
        gettext = true;
        Start();
    }


	void OnClick ()
    {
        foreach (var button in buttonlist)
        {
            Destroy(button);
        }

        int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        Stationboard requested = destinations[num];

        int size = requested.passList.Count;
        stoplist = new GameObject[size + 1];

        for (int k = 0; k < size; k++)
        {
            DateTime dateTime = requested.passList[k].arrival??DateTime.Now;
            string departure = dateTime.ToString("t").PadRight(15, ' ');
            string name = requested.passList[k].station.name;

            if (name == null)
            {
                name = departureStation;
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
        backButton.GetComponent<Button>().onClick.AddListener(Back);

        backButton.transform.GetChild(0).GetComponent<Text>().text = "  Back";
        backButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

        stoplist[size] = backButton;

        gettext = false;
    }

    void OnSelect()
    {
        foreach (var button in buttonlist)
        {
            Destroy(button);
        }

        int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        Stationboard requested = destinations[num];

        int size = requested.passList.Count;
        stoplist = new GameObject[size + 1];

        for (int k = 0; k < size; k++)
        {
            DateTime dateTime = requested.passList[k].arrival ?? DateTime.Now;
            string departure = dateTime.ToString("t").PadRight(15, ' ');
            string name = requested.passList[k].station.name;

            if (name == null)
            {
                name = departureStation;
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
        backButton.GetComponent<Button>().onClick.AddListener(Back);

        backButton.transform.GetChild(0).GetComponent<Text>().text = "  Back";
        backButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

        stoplist[size] = backButton;

        gettext = false;
    }

    IEnumerator GetText()
    {

        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get(remoteUri);
            yield return www.SendWebRequest();
            json = www.downloadHandler.text;
            RootObject stationBoard = new RootObject();
            stationBoard = JsonConvert.DeserializeObject<RootObject>(json);

            if (gettext)
            {
                for (int i = 0; i < n; i++)
                {
                    string departure = stationBoard.stationboard[i].stop.departure.ToString("t").PadRight(15, ' ');
                    string platform = stationBoard.stationboard[i].stop.platform;
                    string destination = stationBoard.stationboard[i].to;
                    string[] arr = { departure, destination };
                    string output = string.Join("\t", arr);

                    Instantiation.buttonlist[i].GetComponentInChildren<Text>().text = string.Format("  {0}", output);
                    Instantiation.buttonlist[i].transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}  ", platform);
                    destinations[i] = stationBoard.stationboard[i];
                }
                yield return new WaitForSeconds(10);
            }
        }
    }
}

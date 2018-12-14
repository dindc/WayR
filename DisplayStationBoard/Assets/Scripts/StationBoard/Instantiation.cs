using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

public class Instantiation : MonoBehaviour {

    private string departureStation;
    private string remoteUri;
    private string json = "";
    public static bool gettext = true;
    public static int size;
    public static int n = 10;
    public static Stationboard requested;
    public static Stationboard pinned;
    public static Stationboard[] destinations = new Stationboard[n];

    public GameObject buttonPrefab;
    public GameObject buttonPannel;
    public static GameObject[] buttonlist = new GameObject[n];
    public static GameObject[] stoplist = new GameObject[0];

    void Start () {

        departureStation = this.transform.name;
        remoteUri = String.Format("http://transport.opendata.ch/v1/stationboard?station={0}&limit=10/stationboard.json", departureStation);
        buttonlist = new GameObject[n];

        for (int i = 0; i < n; i++) 
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.name = string.Format("{0}", i);
            button.transform.SetParent(buttonPannel.transform);
            button.GetComponent<Button>().onClick.AddListener(OnClick);
            button.AddComponent<ButtonClick>();

            button.transform.GetChild(0).GetComponent<Text>().text = "";
            button.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

            Text clone = Instantiate(button.transform.GetChild(0).GetComponent<Text>(), button.transform);
            Text clone2 = Instantiate(button.transform.GetChild(0).GetComponent<Text>(), button.transform);
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
                string name = destinations[i].name;
                string[] arr = { departure, destination };
                string output = string.Join("\t", arr);

                buttonlist[i].GetComponentInChildren<Text>().text = string.Format("  {0}", output);
                buttonlist[i].transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}  ", platform);
                buttonlist[i].transform.GetChild(2).GetComponent<Text>().text = string.Format("                     {0}", name);
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
        size = 10;
        Start();
    }

    void Pin()
    {

        pinned = requested;
        DepartureWarning.checkPinned = true;
        DepartureWarning.platform = requested.stop.platform;
        DepartureWarning.destination = requested.to;
        DepartureWarning.departureTime = requested.stop.departure;

        foreach (var button in stoplist)
        {
            Destroy(button);
        }

        stoplist = new GameObject[0];
        StopAllCoroutines();
        gettext = true;
        size = 10;
        Start();
    }

    public void OnClick ()
    {
      
        int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);

        foreach (var button in buttonlist)
        {
            Destroy(button);
        }

        requested = destinations[num];

        size = requested.passList.Count;
        stoplist = new GameObject[size + 2];

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

            var colors = stopButton.GetComponent<Button>().colors;
            colors.normalColor = Color.grey;
            stopButton.GetComponent<Button>().colors = colors;
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
        size++;

        GameObject pinButton = (GameObject)Instantiate(buttonPrefab);
        pinButton.name = string.Format("{0}", size);
        pinButton.transform.SetParent(buttonPannel.transform);
        pinButton.GetComponent<Button>().onClick.AddListener(Pin);

        pinButton.transform.GetChild(0).GetComponent<Text>().text = "  Pin this train!";
        pinButton.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

        stoplist[size] = pinButton;
        size++;

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
                    string name = stationBoard.stationboard[i].name;
                    string[] arr = { departure, destination };
                    string output = string.Join("                            ", arr);
                     
                    Instantiation.buttonlist[i].GetComponentInChildren<Text>().text = string.Format("  {0}", output);
                    Instantiation.buttonlist[i].transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}  ", platform);
                    Instantiation.buttonlist[i].transform.GetChild(2).GetComponent<Text>().text = string.Format("                     {0}", name);
                    destinations[i] = stationBoard.stationboard[i];
                }
                yield return new WaitForSeconds(10);
            }
        }
    }
}

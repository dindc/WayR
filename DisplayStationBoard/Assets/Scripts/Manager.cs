using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Globalization;
using UnityEngine.Networking;
using System.Collections;

public class Coordinate
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}
public class Station
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate coordinate { get; set; }
    public object distance { get; set; }
}
public class Coordinate2
{
    public string type { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}
public class Station2
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate2 coordinate { get; set; }
    public object distance { get; set; }
}
public class Prognosis
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}
public class Coordinate3
{
    public string type { get; set; }
    public object x { get; set; }
    public object y { get; set; }
}
public class Location
{
    public string id { get; set; }
    public object name { get; set; }
    public object score { get; set; }
    public Coordinate3 coordinate { get; set; }
    public object distance { get; set; }
}
public class Stop
{
    public Station2 station { get; set; }
    public object arrival { get; set; }
    public object arrivalTimestamp { get; set; }
    public DateTime departure { get; set; }
    public int departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis prognosis { get; set; }
    public object realtimeAvailability { get; set; }
    public Location location { get; set; }
}
public class Coordinate4
{
    public string type { get; set; }
    public double? x { get; set; }
    public double? y { get; set; }
}
public class Station3
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate4 coordinate { get; set; }
    public object distance { get; set; }
}
public class Prognosis2
{
    public object platform { get; set; }
    public object arrival { get; set; }
    public object departure { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}
public class Coordinate5
{
    public string type { get; set; }
    public double? x { get; set; }
    public double? y { get; set; }
}
public class Location2
{
    public string id { get; set; }
    public string name { get; set; }
    public object score { get; set; }
    public Coordinate5 coordinate { get; set; }
    public object distance { get; set; }
}
public class PassList
{
    public Station3 station { get; set; }
    public DateTime? arrival { get; set; }
    public int? arrivalTimestamp { get; set; }
    public DateTime? departure { get; set; }
    public int? departureTimestamp { get; set; }
    public object delay { get; set; }
    public string platform { get; set; }
    public Prognosis2 prognosis { get; set; }
    public object realtimeAvailability { get; set; }
    public Location2 location { get; set; }
}
public class Stationboard
{
    public Stop stop { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public object subcategory { get; set; }
    public object categoryCode { get; set; }
    public string number { get; set; }
    public string @operator { get; set; }
    public string to { get; set; }
    public List<PassList> passList { get; set; }
    public object capacity1st { get; set; }
    public object capacity2nd { get; set; }
}
public class RootObject
{
    public Station station { get; set; }
    public List<Stationboard> stationboard { get; set; }
}


public class Manager : MonoBehaviour
{

    public TextMesh text;
    public string remoteUri = "http://transport.opendata.ch/v1/stationboard?station=Zurich&limit=10/stationboard.json";
    private string line = "";
    private string json = "";

    private void Start()
    {
        StartCoroutine(GetText());
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

            string[] lines = { };

            foreach (var board in stationBoard.stationboard)
            {
                string departure = board.stop.departure.ToString("t").PadRight(15, ' ');
                string train = board.name.PadRight(20, ' ');
                string destination = board.to;

                string[] info = { departure, train, destination };

                Array.Resize(ref lines, lines.Length + 1);
                lines[lines.Length - 1] = string.Join("", info);
            }

            line = string.Join("\r\n", lines);
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        text.text = line;
    }

}
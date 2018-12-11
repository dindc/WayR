using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartureWarning : MonoBehaviour {

    public GameObject warningPrefab;
    public GameObject arCamera;
    private GameObject warningText;
    public static bool checkPinned;
    public static string platform;
    public static DateTime departureTime;
    private TimeSpan warningMinutes;

	// Use this for initialization
	void Start () {
        warningText = Instantiate(warningPrefab);
        warningText.transform.SetParent(arCamera.transform);
        warningText.SetActive(false);
        checkPinned = false; 
    }
	
	// Update is called once per frame
	void Update () {
        if (checkPinned)
        {
            warningMinutes = departureTime.Subtract(DateTime.Now);
            int countMinutes = warningMinutes.Days * 24 * 60 + warningMinutes.Hours * 60 + warningMinutes.Minutes;
            if (warningText.activeSelf == false)
                if(countMinutes <= 5)
                    warningText.SetActive(true);
            warningText.transform.GetComponent<TextMesh>().text = "Warning! Train leaving in " + countMinutes  + " Minutes.\nGo to platform " + platform + ".";   
        }
        else
        {
            if (warningText.activeSelf == true)
                warningText.SetActive(false);
        }
	}
}
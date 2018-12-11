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
    public static string destination;
    public static DateTime departureTime;
    private TimeSpan warningMinutes;

	// Use this for initialization
	void Start () {
        warningText = Instantiate(warningPrefab);
        warningText.transform.SetParent(arCamera.transform);
        warningText.SetActive(false);
        checkPinned = false;

        // calling warningFunction every 10 seconds
        InvokeRepeating("warningFunction", 0, 1f);
    }
	
    // function to display a warning if departure of train is close
	public void warningFunction () {

        if (checkPinned)
        {
            warningMinutes = departureTime.Subtract(DateTime.Now);
            int countMinutes = warningMinutes.Days * 24 * 60 + warningMinutes.Hours * 60 + warningMinutes.Minutes;
            if (countMinutes <= 5)
            {
                if (warningText.activeSelf == false)
                    warningText.SetActive(true);
            }
            else
            {
                if (warningText.activeSelf == true)
                    warningText.SetActive(false);
            }
                warningText.transform.GetComponent<TextMesh>().text = "Warning! Train to " + destination + " leaving in " + countMinutes + " Minutes.\nGo to platform " + platform + ".";   
        }
        else
        {
            if (warningText.activeSelf == true)
                warningText.SetActive(false);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToMarker : MonoBehaviour {

    public GameObject stationboardObject;
    public GameObject coordObject;
    public static bool checkCoordUpdate;

    private void Start()
    {
        checkCoordUpdate = false;
    }

    void Update () {

        if (checkCoordUpdate)
            InvokeRepeating("moveObject", 0, 10f); 
        
	}
	
	public void moveObject()
    {
        stationboardObject.transform.localPosition = coordObject.transform.position;
        stationboardObject.transform.localRotation = coordObject.transform.rotation;
        stationboardObject.transform.localScale = coordObject.transform.localScale;
    }
}

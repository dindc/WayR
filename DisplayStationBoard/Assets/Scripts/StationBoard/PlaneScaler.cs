using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(0.364F, 1, 0.23F);
    }
	
	// Update is called once per frame
	void Update () {
        if (Instantiation.size > 10)
        {
            transform.localScale = new Vector3(0.364F, 1, 0.23F + 0.0135F * (Instantiation.size - 10));
        } else
        {
            transform.localScale = new Vector3(0.364F, 1, 0.23F);
        }
    }
}

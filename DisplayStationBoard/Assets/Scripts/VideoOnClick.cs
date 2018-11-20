using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoOnClick : MonoBehaviour {

    public GameObject go;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        var vp = go.GetComponent<VideoPlayer>();
        if (vp.isPlaying)
        {
            vp.Pause();
        }
        else
        {
            vp.Play();
        }
    }
}

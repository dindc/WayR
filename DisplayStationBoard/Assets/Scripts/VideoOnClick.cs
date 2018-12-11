using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoOnClick : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "video")
                {
                    VideoPlayer vp = hit.transform.GetComponent<VideoPlayer>();
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
        }
    }
    
}

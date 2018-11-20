using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Collections;



public class Main : MonoBehaviour {
    public TextMesh tweet_object;

    // Use this for initialization
    void Start () {
        TwitterAPI.instance.UserTimelineTwitter("realdonaldtrump", "extended", UserTimelineResultsCallBack);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void UserTimelineResultsCallBack(List<UserTimelineTwitterData> timelineList)
    {
        string tweets = "FAKE News from the POTUS Donald J. Trump:";
        foreach (UserTimelineTwitterData twitterData in timelineList)
        {
            tweets = tweets + "\n\n" + ResolveTextSize(twitterData.ToString(), 75, 30);
        }
        //tweets = ResolveTextSize(tweets, 75, 30);
        tweet_object.text = tweets;
    }

    // Source: https://answers.unity.com/questions/190800/wrapping-a-textmesh-text.html accessed: 15.11.2018
    private string ResolveTextSize(string input, int lineLength, int lineHeight)
    {
        string[] words = input.Split(" "[0]);
        string result = "";
        string line = "";
        int lineCount = 0;

        foreach (string s in words)
        {
            string temp = line + " " + s;
            if (temp.Length > lineLength)
            {
                result += line + "\n";
                line = s;
            }
            else
            {
                line = temp;
            }
            lineCount = result.Split('\n').Length;
            if (lineCount >= lineHeight) break;
        }

        result += line;
        return result.Substring(1, result.Length - 1);
    }
}



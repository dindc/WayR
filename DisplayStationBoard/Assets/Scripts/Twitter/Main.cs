using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;



public class Main : MonoBehaviour {
    public TextMesh tweetObject;
    public static List<string> tweetList = new List<string>();
    public static List<List<string>> alltweets = new List<List<string>>();

    public GameObject[] tweetButtons = new GameObject[5];

    private int nextUpdate = 60;

    // Use this for initialization
    void Start () {
        TwitterAPI.instance.UserTimelineTwitter("realdonaldtrump", "extended", UserTimelineResultsCallBack);
        TwitterAPI.instance.UserTimelineTwitter("tagesanzeiger", "extended", UserTimelineResultsCallBack);
        TwitterAPI.instance.UserTimelineTwitter("zvv", "extended", UserTimelineResultsCallBack);
        TwitterAPI.instance.UserTimelineTwitter("reutersworld", "extended", UserTimelineResultsCallBack);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 60;

            alltweets = new List<List<string>>();

            TwitterAPI.instance.UserTimelineTwitter("realdonaldtrump", "extended", UserTimelineResultsCallBack);
            TwitterAPI.instance.UserTimelineTwitter("tagesanzeiger", "extended", UserTimelineResultsCallBack);
            TwitterAPI.instance.UserTimelineTwitter("zvv", "extended", UserTimelineResultsCallBack);
            TwitterAPI.instance.UserTimelineTwitter("reutersworld", "extended", UserTimelineResultsCallBack);

        }
    }

    void UserTimelineResultsCallBack(List<UserTimelineTwitterData> timelineList)
    {
        List<string> tweetsaslist = new List<string>();
        string tweets = "";
        foreach (UserTimelineTwitterData twitterData in timelineList)
        {
            string toadd = ResolveTextSize(twitterData.ToString(), 75, 30);
            tweets = tweets + "\n\n" + toadd;
            tweetsaslist.Add(toadd);
        }

        tweetList.Add(tweets);
        alltweets.Add(tweetsaslist);
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



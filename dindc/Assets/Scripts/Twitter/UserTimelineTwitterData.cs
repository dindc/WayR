using System.Collections;
using UnityEngine;
using System;

public class UserTimelineTwitterData
{
	public string tweetText = "";
	public string tweetTime = "";
	
	public override string ToString(){
		return "\"" + tweetText + "\". \n" + tweetTime;
	}
}
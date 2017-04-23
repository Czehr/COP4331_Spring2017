using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour {

	public string[] scores;

	// Use this for initialization
	IEnumerator Start () {

		WWW scoreData = new WWW ("http://localhost/~kyleferguson/scores.php");
		yield return scoreData;
		string scoreString = scoreData.text;
		scores = scoreString.Split (';');
		Debug.Log (scores[0]);
		Debug.Log (scores[1]);
	}
}

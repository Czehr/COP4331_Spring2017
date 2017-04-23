using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	// The map we're building is going to look like:
	//
	//	LIST OF USERS -> A User -> LIST OF SCORES for that user
	//

	//Dictionary< string, Dictionary<string, int> > playerScores;
	private string [] scores;
	private string [] names;
	private string [] entries;
	private string [] diffs;
	private string [] imageids;
	int changeCounter = 0;
	private int len;

	// Use this for initialization
	IEnumerator Start () {

		WWW scoreData = new WWW ("http://22286084.ngrok.io/~kyleferguson/scores.php");
		yield return scoreData;
		string scoreString = scoreData.text;
		entries = scoreString.Split (';');
		len = entries.Length;
		for (int i = 0; i < len; i++) 
		{
			names[i] = SetValues(entries[i], "username:");
			imageids[i] = SetValues(entries[i], "image_id:");
			diffs[i] = SetValues(entries[i], "difficulty:");
			scores[i] = SetValues(entries[i], "score:");
		}
		for (int i = 0; i < 5; i++) 
		{
			Debug.Log (names [i]);
			Debug.Log (imageids [i]);
			Debug.Log (diffs [i]);
			Debug.Log (scores [i]);
		}
	}

	public void Reset() {
		changeCounter++;
		//playerScores = null;
	}

	public string SetValues(string data, string index) {

		string name = data.Substring (data.IndexOf (index) + index.Length);
		name = name.Remove(name.IndexOf("|"));
		return name;
	}

	public string GetName(int i)
	{
		return names [i];
	}

	public string GetDiff(int i)
	{
		return diffs [i];
	}

	public string GetScore(int i)
	{
		return scores [i];
	}

	public string GetImageid(int i)
	{
		return imageids [i];
	}
		
	public int GetLen()
	{
		return len;
	}


}

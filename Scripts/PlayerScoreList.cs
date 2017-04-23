using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;

	//ScoreManager scoreManager;

	int lastChangeCounter;
	private string [] scores;
	private string[] names;
	private string [] entries;
	private string [] diffs;
	private string [] imageids;
	//int changeCounter = 0;
	private int len;

	// Use this for initialization
	IEnumerator Start () {

		int i;
		names = new string[15];
		diffs = new string[15];
		imageids = new string[15];
		scores = new string[15];
		WWW scoreData = new WWW ("http://fdef083d.ngrok.io/~kyleferguson/scores.php");
		yield return scoreData;
		string scoreString = scoreData.text;
		entries = scoreString.Split (';');
		len = entries.Length;

		for (i = 0; i < len; i++) 
		{
			//Debug.Log(entries [i]);
			names[i] = SetValues(entries[i], "username:");
			imageids[i] = SetValues(entries[i], "image_id:");
			diffs[i] = SetValues(entries[i], "difficulty:");
			scores[i] = SetValues(entries[i], "score:");
		}

			
		while(this.transform.childCount > 0) {
			Transform c = this.transform.GetChild(0);
			c.SetParent(null);  // Become Batman
			Destroy (c.gameObject);
		}

		for(i = 0; i < len; i++) {
			
			if (names [i] == null)
				break;
			
			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(this.transform);

			go.transform.Find ("Username").GetComponent<Text> ().text = names[i];
			go.transform.Find ("Kills").GetComponent<Text>().text = imageids[i];
			go.transform.Find ("Deaths").GetComponent<Text>().text = diffs[i];
			go.transform.Find ("Assists").GetComponent<Text>().text = scores[i] + " sec";
		}
	}
	

	public string SetValues(string data, string index) {

		if (data.Length == 0)
			return null;
		string name = data.Substring (data.IndexOf (index) + index.Length);
		if(!index.Equals("score:"))
			name = name.Remove(name.IndexOf("|"));
		return name;
	}

}

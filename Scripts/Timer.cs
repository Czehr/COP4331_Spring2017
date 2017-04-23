using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private float timer = 0f;
	private GUIStyle style;

	// Use this for initialization
	void Start () {
		style = new GUIStyle ();
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

	}

	void OnGUI()
	{
		style.fontSize = 20;
		GUI.Label (new Rect (100, 100, 200, 100), timer.ToString("0"));
	}

}

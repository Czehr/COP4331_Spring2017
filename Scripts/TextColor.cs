using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextColor : MonoBehaviour {

	public float switchTime = .2f;
	public float timer;
	private int randInt;
	private int temp;

	Color[] colorList = { Color.blue, Color.green, Color.magenta, Color.yellow, Color.red, Color.white };

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		if (timer >= switchTime) {
			temp = randInt;
			while(randInt == temp)
				randInt = Random.Range (0, colorList.Length);
			
			gameObject.GetComponent<TextMesh> ().color = colorList [randInt];
			timer = 0f;
		}


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtonInteract : MonoBehaviour {

	public Texture2D img;
	public float gazeTime = 2f;
	private float timer;
	public AudioSource hover;
	public AudioSource select;
	public Material m1;
	public Material m2;
	public Scene s;
	private bool gazedAt;
	//public Color newColor;
	private Color ogColor;

	public int easyId;
	public int mediumId;
	public int hardId;
	public GameObject currentDif;
	public Material oldM;

	// Use this for initialization
	void Start () {
		//ogColor = gameObject.GetComponent<SpriteRenderer> ().color;
		StaticVariables.difficulty = 1;

		img = (this.m1.GetTexture("_MainTex") as Texture2D);
		timer = 0f;

	}
	
	// Update is called once per frame
	void Update () {

		if (gazedAt) {

			timer += Time.deltaTime;

			if (timer >= gazeTime)
			{
				ExecuteEvents.Execute (gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerDownHandler);
				timer = 0f;
			}
		}
			
	}

	public void PointerEnter()
	{
		timer = 0;
		hover.Play ();
		gazedAt = true;

		if (!(this.name.Equals ("MMButton") || this.name.Equals ("BackButton")))
			gameObject.GetComponent<Renderer> ().material = m2;
		if ((this.name.Equals ("BackButton")))
			this.GetComponent<SpriteRenderer> ().color = Color.gray;
		//gameObject.GetComponent<SpriteRenderer>().color = Color.red;
	}
		
	public void PointerExit()
	{
		//Debug.Log("Pointer Exit\n");
		gazedAt = false;
		//gameObject.GetComponent<SpriteRenderer> ().color = ogColor;
		if (!(this.name.Equals ("MMButton") || this.name.Equals ("BackButton")))
			gameObject.GetComponent<Renderer> ().material = m1;
		if ((this.name.Equals ("BackButton")))
			this.GetComponent<SpriteRenderer> ().color = Color.white;
		//gameObject.GetComponent<Renderer> ().material.color = originalColor;
	}

	public void PointerDown()
	{
		select.Play ();
		timer = 0f;

		if (this.name.Equals ("PlayButton")) {
			SceneManager.LoadScene(3);
			Debug.Log ("this is the play button");
		}

		if (this.name.Equals ("ScoreButton")) {
			SceneManager.LoadScene(2);
		}

		if (this.name.Equals ("QuitButton")){
			Debug.Log ("this is the quit button");
			//Application.Quit ();
		}

		if (this.name.Equals ("MMButton") || this.name.Equals ("BackButton")) {
			SceneManager.LoadScene(0);
		}
	}

	public void GoToGame()
	{
		StaticVariables.img = (this.m1.GetTexture("_MainTex") as Texture2D);
		SceneManager.LoadScene(1);
	}

	public void ChangeDifficulty()	{

		hover.Play ();

		if (gameObject.GetInstanceID () == easyId || this.name.Equals ("Easy2.0")) {
			StaticVariables.difficulty = 1;
			GameObject.Find("Medium2.0").GetComponent<SpriteRenderer>().color = Color.yellow;
			GameObject.Find ("Hard2.0").GetComponent<SpriteRenderer> ().color = Color.red;
			this.GetComponent<SpriteRenderer> ().color = Color.gray;

		}

		if (gameObject.GetInstanceID () == mediumId || this.name.Equals ("Medium2.0")) {
			StaticVariables.difficulty = 2;
			GameObject.Find("Easy2.0").GetComponent<SpriteRenderer>().color = Color.green;
			GameObject.Find ("Hard2.0").GetComponent<SpriteRenderer> ().color = Color.red;
			this.GetComponent<SpriteRenderer> ().color = Color.gray;
		}

		if (gameObject.GetInstanceID () == hardId || this.name.Equals ("Hard2.0")) {
			StaticVariables.difficulty = 3;
			GameObject.Find("Medium2.0").GetComponent<SpriteRenderer>().color = Color.yellow;
			GameObject.Find ("Easy2.0").GetComponent<SpriteRenderer> ().color = Color.green;
			this.GetComponent<SpriteRenderer> ().color = Color.gray;
		}
	}

	bool atScene(int x)
	{
		return SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInteraction : MonoBehaviour {

	public float gazeTime = 5f;
	private float timer;
	private bool gazedAt;
	Color originalColor;

	// Use this for initialization
	void Start () {
		originalColor = gameObject.GetComponent<Renderer> ().material.color;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gazedAt)
		{
			timer += Time.deltaTime;
			Debug.Log (timer);
			if (/*timer >= gazeTime*/true) {
				ExecuteEvents.Execute (gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerDownHandler);
				timer = 0f;
			}
		}

	}

	public void PointerEnter()
	{
		timer = 0f;
		gazedAt = true;
	}

	public void PointerExit()
	{
		timer = 0f;
		gazedAt = false;
		gameObject.GetComponent<Renderer> ().material.color = originalColor;
	}

	public void PointerDown()
	{
		Debug.Log("Pointer Down\n");
		gameObject.GetComponent<Renderer> ().material.color = Color.white;
	}

}

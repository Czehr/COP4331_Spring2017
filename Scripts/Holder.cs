using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Holder : MonoBehaviour
{
	public bool isHoveredOver;
	public bool clinked;
	public int index;
	public float gazeTime;
	public Color hoverColor;
	private float timer;
	private Puzzle puzzle;
	private Piece respPiece;

	void Start ()
	{
		GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0f);
		isHoveredOver = false;
		clinked = false;
		puzzle = GameObject.Find("PuzzleRoot").GetComponent<Puzzle>();
		respPiece = GameObject.Find("Piece " + index).GetComponent<Piece>();
	}

	void Update ()
	{
		if(isHoveredOver)
		{
			timer += Time.deltaTime;

			if(timer >= gazeTime)
			{
				ExecuteEvents.Execute (gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerDownHandler);
				timer = 0f;
			}
		}
		else
		{
			timer = 0f;
		}

		if(puzzle.paused)
			GetComponent<BoxCollider>().enabled = false;
		else
			GetComponent<BoxCollider>().enabled = true;
	}

	public void PointerEnter()
	{
		isHoveredOver = true;
		GetComponent<SpriteRenderer>().color = hoverColor;
	}

	public void PointerExit()
	{
		isHoveredOver = false;
		GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0f);
	}
		
	public void PointerDown()
	{
		if(!clinked && respPiece.isBeingHeld)
			clinked = true;
	}
}

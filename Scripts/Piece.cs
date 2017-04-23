using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
	public bool isHoveredOver;
	public bool isBeingHeld;
	public bool isPlacedCorrectly;
	public float gazeTime;
	public int index;
	public int ogSortingOrder;
	public int highSortOrder;
	public float pickupSpeed;
	public float dropSpeed;

	private bool handIsHoldingPieceAlready;
	private Vector3 ogPosition;
	private float timer;
	private GameObject respHolder;
	private GameObject hand;
	private GameObject puzzle;
	private GameObject dropButton;
	private GameSound sound;

	private bool startedLerp1;
	private bool completedLerp1;
	private bool startedLerp2;
	private bool completedLerp2;
	private bool startedLerp3;
	private bool completedLerp3;
	private float lerpTime;
	private float startTime;
	private float dist;

	void Start ()
	{
		timer = 0f;
		isHoveredOver = false;
		isBeingHeld = false;
		isPlacedCorrectly = false;
		ogPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		respHolder = GameObject.Find("Holder " + index);
		hand = GameObject.Find("The Hand");
		puzzle = GameObject.Find("PuzzleRoot");
		dropButton = GameObject.Find("DropButton");
		highSortOrder = puzzle.GetComponent<Puzzle>().numPieces + 1;
		sound = GameObject.Find("Soundifier").GetComponent<GameSound>();
		startedLerp1 = false;
		completedLerp1 = false;
		startedLerp2 = false;
		completedLerp2 = false;
		startedLerp3 = false;
		completedLerp3 = false;

	}

	void Update ()
	{
		handIsHoldingPieceAlready = hand.GetComponent<Hand>().isHolding;
		bool heldPieceHolderClicked = respHolder.GetComponent<Holder>().clinked && isBeingHeld;

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

		if(isBeingHeld && !dropButton.GetComponent<GameButton>().dropPiece && !heldPieceHolderClicked)
		{
			if(!startedLerp1)
			{
				startedLerp2 = false;
				startedLerp3 = false;

				startTime = Time.time;
				dist = Vector3.Distance(ogPosition, hand.transform.position);
				startedLerp1 = true;
				completedLerp1 = false;
			}
			else if(!completedLerp1)
			{
				lerpTime = (Time.time - startTime) * pickupSpeed;
				float f = lerpTime / dist;
				transform.position = Vector3.Lerp(ogPosition, hand.transform.position, f);
				transform.rotation = hand.transform.rotation;
				GetComponent<SpriteRenderer>().sortingOrder = highSortOrder;

				if(Vector3.Distance(transform.position, hand.transform.position) <= 0.05f)
				{
					completedLerp1 = true;
				}
			}
				
			if(completedLerp1)
			{
				transform.position = hand.transform.position;
				transform.rotation = hand.transform.rotation;
			}
		}

		if(heldPieceHolderClicked)
		{
			if(!startedLerp2)
			{
				startedLerp1 = false;
				startedLerp3 = false;
				startTime = Time.time;
				dist = Vector3.Distance(hand.transform.position, respHolder.transform.position);
				isPlacedCorrectly = true;
				removeSelfFromPuzzle();
				transform.rotation = respHolder.transform.rotation;
				startedLerp2 = true;
				completedLerp2 = false;
			}
			else if(!completedLerp2)
			{
				lerpTime = (Time.time - startTime) * dropSpeed;
				float f = lerpTime / dist;
				transform.position = Vector3.Lerp(hand.transform.position, respHolder.transform.position, f);
				GetComponent<SpriteRenderer>().sortingOrder = highSortOrder;

				if(Vector3.Distance(transform.position, respHolder.transform.position) <= 0.05f)
				{
					completedLerp2 = true;
					isBeingHeld = false;
					hand.GetComponent<Hand>().isHolding = false;
					transform.position = respHolder.transform.position;
					transform.rotation = respHolder.transform.rotation;
					GetComponent<SpriteRenderer>().sortingOrder = ogSortingOrder;
					sound.playSound("click");
				}
			}
		}

		if(isBeingHeld && dropButton.GetComponent<GameButton>().dropPiece)
		{
			if(!startedLerp3)
			{
				startedLerp1 = false;
				startedLerp2 = false;

				startTime = Time.time;
				dist = Vector3.Distance(hand.transform.position, ogPosition);
				transform.rotation = respHolder.transform.rotation;
				startedLerp3 = true;
				completedLerp3 = false;
			}
			else if(!completedLerp3)
			{
				lerpTime = (Time.time - startTime) * dropSpeed;
				float f = lerpTime / dist;
				transform.position = Vector3.Lerp(hand.transform.position, ogPosition, f);
				GetComponent<SpriteRenderer>().sortingOrder = highSortOrder;

				if(Vector3.Distance(transform.position, ogPosition) <= 0.05f)
				{
					hand.GetComponent<Hand>().isHolding = false;
					isBeingHeld = false;
					completedLerp3 = true;
					GetComponent<SpriteRenderer>().sortingOrder = ogSortingOrder;
					sound.playSound("click");
					transform.position = ogPosition;
					transform.rotation = respHolder.transform.rotation;
					dropButton.GetComponent<GameButton>().dropPiece = false;
				}
			}
		}

		if(puzzle.GetComponent<Puzzle>().paused)
			GetComponent<BoxCollider>().enabled = false;
		else
			GetComponent<BoxCollider>().enabled = true;
	}

	public void PointerEnter()
	{
		if(isPlacedCorrectly || handIsHoldingPieceAlready)
		{
			isHoveredOver = false;
		}
		else
		{
			isHoveredOver = true;
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
		}
	}

	public void PointerExit()
	{
		isHoveredOver = false;
		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}


	public void PointerDown()
	{
		if(!isBeingHeld && !handIsHoldingPieceAlready)
		{
			isBeingHeld = true;
			sound.playSound("swoop");
			hand.GetComponent<Hand>().isHolding = true;
		}
	}

	void removeSelfFromPuzzle()
	{
		puzzle.GetComponent<Puzzle>().removePieceFromUnplaced("Piece " + index);
	}

	public void changeOGStuff(Vector3 pos, int newSO)
	{
		this.transform.position = pos;
		ogPosition = new Vector3(pos.x, pos.y, pos.z);
		ogSortingOrder = newSO;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
	public bool isHoveredOver;
	public bool isMenuButton;
	public bool dropPiece;
	public float gazeTime;
	public Color baseColor;
	public Color hoverColor;
	public Color pressColor;
	private float timer;
	private Puzzle puzzle;
	private Hand hand;
	private Pauseulator p;
	private GameSound sound;
	private bool isOnPuzzle;

	void Start ()
	{
		isOnPuzzle = SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0);

		if(isOnPuzzle)
		{
			puzzle = GameObject.Find("PuzzleRoot").GetComponent<Puzzle>();
			hand = GameObject.Find("The Hand").GetComponent<Hand>();
			p = GameObject.Find("Pause-ulator 5000").GetComponent<Pauseulator>();
			sound = GameObject.Find("Soundifier").GetComponent<GameSound>();
		}

		isHoveredOver = false;
		GetComponent<SpriteRenderer>().color = baseColor;
	}

	void Update ()
	{
		if(isHoveredOver && !this.name.Contains("Arrow"))
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

		if(puzzle != null)
		{
			if(puzzle.paused && !isMenuButton && isOnPuzzle)
				GetComponent<BoxCollider>().enabled = false;
			else
				GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void PointerEnter()
	{
		isHoveredOver = true;
		GetComponent<SpriteRenderer>().color = hoverColor;
	}

	public void PointerExit()
	{
		isHoveredOver = false;
		GetComponent<SpriteRenderer>().color = baseColor;
	}


	public void PointerDown()
	{
		if(this.name.Equals("ShuffleButton"))
		{
			puzzle.shufflePieces();
			isHoveredOver = false;
			sound.playSound("ddpddlldlp");
		}

		if(this.name.Equals("DropButton"))
		{
			isHoveredOver = false;
			if(hand.isHolding)
			{
				dropPiece = true;
				hand.setHolding(false);
				sound.playSound("fweewp");
			}
		}
			
		if(this.name.Equals("PauseButton"))
		{
			p.enablePausyThings();
			puzzle.paused = true;
			isHoveredOver = false;
			sound.playSound("bloop");
		}

		if(this.name.Equals("Resume"))
		{
			p.disablePausyThings();
			puzzle.paused = false;
			isHoveredOver = false;
			sound.playSound("bloop");
		}

		if(this.name.Equals("ExitPuzzle"))
		{
			sound.playSound("bloop");
			puzzle.pushScore ();
			SceneManager.LoadScene(0);
			puzzle.paused = false;
			isHoveredOver = false;
		}

		if(this.name.Equals("ExitPuzzlePM"))
		{
			sound.playSound("bloop");
			SceneManager.LoadScene(0);
			puzzle.paused = false;
			isHoveredOver = false;
		}

		if(this.name.Equals("1LUp"))
			puzzle.editName(0, true);
		else if(this.name.Equals("1LDown"))
			puzzle.editName(0, false);
		if(this.name.Equals("2LUp"))
			puzzle.editName(1, true);
		else if(this.name.Equals("2LDown"))
			puzzle.editName(1, false);
		if(this.name.Equals("3LUp"))
			puzzle.editName(2, true);
		else if(this.name.Equals("3LDown"))
			puzzle.editName(2, false);

		GetComponent<SpriteRenderer>().color = pressColor;
	}
}

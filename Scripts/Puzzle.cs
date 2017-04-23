using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
	public Texture2D image;
	public int difficulty;
	public float puzzleScale;
	public GameObject piecePrefab;
	public GameObject holderPrefab;
	public bool paused;
	public float piecePickupSpeed;
	public float pieceDropSpeed;
	public int numPieces;
	public bool useKittyImage;
	public string username;

	private bool puzzleIsSolved;
	private List<GameObject> pieces;
	private List<GameObject> pieceHolders;
	private GameObject puzzleRoot;
	private GameObject imageHolder;
	private Clock clock;
	private GameSound sound;
	private bool playedClap = false;
	private Pauseulator p;
	private GameObject imageReference;
	private Text nameText;
	private int[] nameChars = new int[3];

	void Awake()
	{
		numPieces = 0;
		if(!useKittyImage)
		{
			image = StaticVariables.img;
			difficulty = StaticVariables.difficulty;
		}

		nameChars[0] = (int)'A';
		nameChars[1] = (int)'A';
		nameChars[2] = (int)'A';

		char[] nc = {(char)nameChars[0], (char)nameChars[1], (char)nameChars[2]};
		username = new string(nc);
		imageReference = GameObject.Find("ImageReference");
		puzzleIsSolved = false;
		pieces = new List<GameObject>();
		pieceHolders = new List<GameObject>();
		puzzleRoot = GameObject.Find("PuzzleRoot");
		imageHolder = GameObject.Find("ImageHolder");
		sound = GameObject.Find("Soundifier").GetComponent<GameSound>();
		p = GameObject.Find("Pause-ulator 5000").GetComponent<Pauseulator>();
		clock = GameObject.Find("Timer").GetComponent<Clock>();
	}

	void Start ()
	{
		Puzzlize();
		float newSize = puzzleScale / (Mathf.Max(image.width, image.height)/100.0f);
		imageHolder.transform.localScale = new Vector3(newSize, newSize, 1);
	}

	void Update ()
	{
		if(pieces.Count == 0)
			puzzleIsSolved = true;

		if(!playedClap && puzzleIsSolved)
		{
			p.enableWinScreen();
			sound.playSound("clappy");
			playedClap = true;
			paused = true;
			nameText = GameObject.Find("NameText").GetComponent<Text>();
		}
	}

	void Puzzlize()
	{
		Vector3 posPuz = this.transform.position;
		float newSize = puzzleScale / (Mathf.Max(image.width, image.height)/100.0f);
		imageReference.GetComponent<SpriteRenderer>().sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
		imageReference.transform.localScale = new Vector3(newSize, newSize, 0);

		int sizeX = image.width;
		int sizeY = image.height;

		int numX;
		int numY;

		switch(difficulty)
		{
			case 1:
				numX = 4;
				numY = 4;
				break;
			case 2:
				if(image.width >= image.height)
				{
					numX = 8;
					numY = 4;
				}
				else
				{
					numX = 4;
					numY = 8;
				}
				break;
			case 3:
				numX = 8;
				numY = 8;
				break;
			default:
				numX = 1;
				numY = 1;
				break;
		}

		numPieces = numX * numY;
		float pSizeX = sizeX / numX;
		float pSizeY = sizeY / numY;

		float initX = 0 - (((pSizeX / 100f) / 2.0f) * (float) numX - 1);
		float initY = 0 - (((pSizeY / 100f) / 2.0f) * (float) numY - 1);

		int counter = 0;

		for(int i = 0; i < numX; i++)
		{
			for(int j = 0; j < numY; j++)
			{
				Sprite newSprite = Sprite.Create(image, new Rect(i * pSizeX, j * pSizeY, pSizeX, pSizeY), new Vector2(0.5f, 0.5f));
				Sprite newSprite2 = Sprite.Create(image, new Rect(i * pSizeX, j * pSizeY, pSizeX, pSizeY), new Vector2(0.5f, 0.5f));
				GameObject n = Instantiate(piecePrefab, Vector3.zero, Quaternion.identity);
				GameObject ih = Instantiate(holderPrefab, Vector3.zero, Quaternion.identity);
				SpriteRenderer sr = n.GetComponent<SpriteRenderer>();
				SpriteRenderer srih = ih.GetComponent<SpriteRenderer>();
				BoxCollider bcn = n.GetComponent<BoxCollider>();
				BoxCollider bcih = ih.GetComponent<BoxCollider>();

				sr.sprite = newSprite;
				srih.sprite = newSprite2;
				sr.sortingOrder = -1 * (1 + ((i * numX) + j));
				bcn.size = new Vector3(pSizeX/100f, pSizeY/100f, 0f);
				bcih.size = new Vector3(pSizeX/100f, pSizeY/100f, 0f);

				pieces.Add(n);
				pieceHolders.Add(ih);

				n.name = "Piece " + counter;
				n.GetComponent<Piece>().index = counter;
				n.GetComponent<Piece>().ogSortingOrder = sr.sortingOrder;
				n.GetComponent<Piece>().pickupSpeed = piecePickupSpeed;
				n.GetComponent<Piece>().dropSpeed = pieceDropSpeed;
				ih.name = "Holder " + counter;
				ih.GetComponent<Holder>().index = counter;

				n.transform.parent = puzzleRoot.transform;
				ih.transform.parent = imageHolder.transform;

				float randX = Random.Range(1f, 4f);
				float randY = Random.Range(1f, 4f);
				n.transform.localPosition = new Vector3(randX, randY, 0);
				n.transform.localRotation = Quaternion.identity;
				n.transform.localScale = new Vector3(newSize, newSize, 1);

				ih.transform.localPosition = new Vector3(initX + (i * (pSizeX / 100f)), initY + (j * (pSizeY / 100f)), 0);
				counter++;
			}
		}
	}

	public void removePieceFromUnplaced(string pieceName)
	{
		pieces.Remove(GameObject.Find(pieceName));
	}

	public void shufflePieces()
	{
		List<int> orderNums = new List<int>();
		Vector3 posPuz = this.transform.position;
		int numPieces = (int) Mathf.Pow(4, difficulty);
		int sizeX = image.width;
		int sizeY = image.height;
		int numX = (int) Mathf.Sqrt(numPieces);
		int numY = (int) Mathf.Sqrt(numPieces);
		float pSizeX = sizeX / numX;
		float pSizeY = sizeY / numY;

		for(int i = 0; i < pieces.Count; i++)
			orderNums.Add(i + 1);

		for(int i = 0; i < pieces.Count; i++)
		{
			int r = Random.Range(0, orderNums.Count);
			float randX = Random.Range(1f, 4f);
			float randY = Random.Range(1f, 4f);
			int nso = orderNums[r] + 1;
			pieces[i].GetComponent<SpriteRenderer>().sortingOrder = nso;
			Vector3 newPos = new Vector3(posPuz.x + randX, posPuz.y + randY, posPuz.z);
			pieces[i].GetComponent<Piece>().changeOGStuff(newPos, nso);
			orderNums.RemoveAt(r);
		}
	}

	public bool pSolved()
	{
		return puzzleIsSolved;
	}

	public void pushScore()
	{
		WWWForm timeData = new WWWForm ();
		timeData.AddField ("unamePost", username);
		timeData.AddField ("image_idPost", image.name);
		timeData.AddField ("diffPost", diffString(difficulty));
		timeData.AddField ("timePost", clock.secondsElapsed - 1);

		WWW post = new WWW("http://fdef083d.ngrok.io/~kyleferguson/insert_time.php", timeData);
		return;
	}

	private string diffString(int x)
	{
		if (x == 1)
			return "Easy";
		else if (x == 2)
			return "Medium";
		else if (x == 3)
			return "Hard";
		else
			return "undefined";
	}

	public void editName(int i, bool inc)
	{
		if(inc)
			nameChars[i]--;
		else
			nameChars[i]++;

		if((nameChars[i] == ((int)'Z' + 1) && !inc)) 
			nameChars[i] = (int)'A';
		if((nameChars[i] == ((int)'A' - 1) && inc))
			nameChars[i] = (int)'Z';

		char[] nc = {(char)nameChars[0], (char)nameChars[1], (char)nameChars[2]};
		username = new string(nc);
		if(nameText == null)
			nameText = GameObject.Find("NameText").GetComponent<Text>();
		nameText.text = username;
	}
}

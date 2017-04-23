using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSelect : MonoBehaviour
{
	public bool scrollUp;
	public bool scrollDown;
	public float scrollSpeed;
	public float imageScaleBound;
	public List<Texture2D> images;

	private GameObject picDisplay1;
	private GameObject picDisplay2;
	private GameObject picDisplay3;

	private Texture2D p1;
	private Texture2D p2;
	private Texture2D p3;
	private int p1Index;
	private int p2Index;
	private int p3Index;

	private Vector3 topPortal; //It's masc4masc and browses /r/gaybros 
	private Vector3 bottomPortal; //It likes lady gaga

	void Start ()
	{
		picDisplay1 = GameObject.Find("Pic1");
		picDisplay2 = GameObject.Find("Pic2");
		picDisplay3 = GameObject.Find("Pic3");

		p1 = images[0];
		p2 = images[1];
		p3 = images[2];

		p1Index = 0;
		p2Index = 1;
		p3Index = 2;

		picDisplay1.GetComponent<SpriteRenderer>().sprite = Sprite.Create(p1, new Rect(0, 0, p1.width, p1.height), new Vector2(0.5f, 0.5f));
		picDisplay2.GetComponent<SpriteRenderer>().sprite = Sprite.Create(p2, new Rect(0, 0, p2.width, p2.height), new Vector2(0.5f, 0.5f));
		picDisplay3.GetComponent<SpriteRenderer>().sprite = Sprite.Create(p3, new Rect(0, 0, p3.width, p3.height), new Vector2(0.5f, 0.5f));

		resize(1);
		resize(2);
		resize(3);

		topPortal = new Vector3(0f, 6.22f, 4.5f);
		bottomPortal = new Vector3(0f, -6.12f, 4.5f);
	}

	void Update ()
	{
		float y1 = picDisplay1.transform.position.y;
		float y2 = picDisplay2.transform.position.y;
		float y3 = picDisplay3.transform.position.y;
		scrollUp = GameObject.Find("UpArrow").GetComponent<GameButton>().isHoveredOver;
		scrollDown = GameObject.Find("DownArrow").GetComponent<GameButton>().isHoveredOver;

		if(y1 >= 6.25)
			loopify(1, 1);
		else if(y2 >= 6.25)
			loopify(2, 1);
		else if(y3 >= 6.25)
			loopify(3, 1);

		if(y1 <= -6.15)
			loopify(1, 0);
		else if(y2 <= -6.15)
			loopify(2, 0);
		else if(y3 <= -6.15)
			loopify(3, 0);
	
		if(scrollUp)
		{
			picDisplay1.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
			picDisplay2.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
			picDisplay3.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
		}

		if(scrollDown)
		{
			picDisplay1.transform.Translate(Vector3.down * Time.deltaTime * scrollSpeed);
			picDisplay2.transform.Translate(Vector3.down * Time.deltaTime * scrollSpeed);
			picDisplay3.transform.Translate(Vector3.down * Time.deltaTime * scrollSpeed);
		}
	}

	void loopify(int pd, int portal)
	{
		GameObject pic = picDisplay1;
		print("p1: " + p1Index + " p2: " + p2Index + " p3: " + p2Index);

		switch(pd)
		{
			case 1:
				pic = picDisplay1;
				break;
			case 2:
				pic = picDisplay2;
				break;
			case 3:
				pic = picDisplay3;
				break;
		}

		if(portal == 0)
		{
			pic.transform.position = topPortal;
			nextIndex(0, pd);
		}
		else if(portal == 1)
		{
			pic.transform.position = bottomPortal;
			nextIndex(1, pd);
		}
	}

	void resize(int pd)
	{
		GameObject pic = picDisplay1;
		Texture2D t = p1;

		switch(pd)
		{
			case 1:
				pic = picDisplay1;
				t = p1;
				break;
			case 2:
				pic = picDisplay2;
				t = p2;
				break;
			case 3:
				pic = picDisplay3;
				t = p3;
				break;
		}

		float max = (Mathf.Max(t.width, t.height));
		float newSize = imageScaleBound / (max / 100.0f);
		pic.transform.localScale = new Vector3(newSize, newSize, 0);
	}

	void updateThings(int pd, int newIndex)
	{
		GameObject pic = picDisplay1;
		Texture2D t = p1;

		switch(pd)
		{
			case 1:
				pic = picDisplay1;
				p1 = images[newIndex];
				t = p1;
				p1Index = newIndex;
				break;
			case 2:
				pic = picDisplay2;
				p2 = images[newIndex];
				t = p2;
				p2Index = newIndex;
				break;
			case 3:
				pic = picDisplay3;
				p3 = images[newIndex];
				t = p3;
				p3Index = newIndex;
				break;
		}

		pic.GetComponent<SpriteRenderer>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
		resize(pd);
	}

	void nextIndex(int upOrDown, int pd)
	{
		//0 = down;
		//1 = up;

		GameObject pic = picDisplay1;
		Texture2D p = p1;
		int i = 0;

		switch(pd)
		{
			case 1:
				pic = picDisplay1;
				p = p1;
				i = p1Index;
				break;
			case 2:
				pic = picDisplay2;
				p = p2;
				i = p2Index;
				break;
			case 3:
				pic = picDisplay3;
				p = p3;
				i = p3Index;
				break;
		}

		if(i == 0 && upOrDown == 0)
		{
			updateThings(pd, images.Count - 1);
		}
		else if(i == images.Count - 1 && upOrDown == 1)
		{
			updateThings(pd, 0);
		}
		else
		{
			if(upOrDown == 0)
			{
				updateThings(pd, i + 3);
			}
			else
			{
				updateThings(pd, i - 3);
			}
		}
	}
}

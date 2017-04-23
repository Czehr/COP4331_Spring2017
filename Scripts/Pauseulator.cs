using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauseulator : MonoBehaviour
{
	public GameObject menuPanel;
	public GameObject sunglasses;
	public GameObject winScreen;
	public GameObject enterNameScreen;

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	public void enablePausyThings()
	{
		menuPanel.SetActive(true);
		sunglasses.SetActive(true);
	}

	public void disablePausyThings()
	{
		menuPanel.SetActive(false);
		sunglasses.SetActive(false);
	}

	public void enableWinScreen()
	{
		winScreen.SetActive(true);
		sunglasses.SetActive(true);
		enterNameScreen.SetActive(true);
	}

	public void disableWinScreen()
	{
		winScreen.SetActive(false);
		sunglasses.SetActive(false);
		enterNameScreen.SetActive(false);
	}
}

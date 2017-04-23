using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public bool isHolding;

	void Start ()
	{
		isHolding = false;	
	}

	void Update ()
	{
		
	}

	public void setHolding(bool b)
	{
		isHolding = b;
	}
}

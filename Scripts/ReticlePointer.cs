using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReticlePointer : MonoBehaviour
{
	public bool hideByDefault = true;
	public Image image;  // assign Fill_Image here.
	public float hideTimeoutPeriod = 3;
	public GameObject pointerObject; // assign Pointer Image here.

	public static ReticlePointer Instance;

	void Awake()
	{
		Instance = this;
		image.fillAmount = 0;
	}

	public void SetFillAmount(float amount)
	{
		image.fillAmount = amount;
	}

	void Update()
	{
		if (hideByDefault)
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				CancelInvoke("Hide");
				Show();
			}
			else
			{
				SetFillAmount(0);
				Invoke("Hide",hideTimeoutPeriod);
			}
		}
		else
		{
			Show();
		}
	}

	public void Show()
	{
		pointerObject.SetActive(true);
	}

	public void Hide()
	{
		pointerObject.SetActive(false);
	}
}
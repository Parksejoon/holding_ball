using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Material met;
	

	private void Start()
	{
		met.color = Color.HSVToRGB(0.5f, 0.5f, 1);
	}
}

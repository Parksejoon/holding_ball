using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatColorFinder : MonoBehaviour
{
	// 초기화
	void Start()
	{
		SpriteRenderer sr =	GetComponent<SpriteRenderer>();

		sr.color = sr.material.GetColor("_Color");
	}
}

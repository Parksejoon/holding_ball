using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	Rigidbody2D rigidbody2d;

	private void Awake()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		StartCoroutine(Zero());
		StartCoroutine(One());
	}

	private void Update()
	{
	}

	IEnumerator One()
	{
		yield return null;
		rigidbody2d.velocity = Vector3.one;

	}

	IEnumerator Zero()
	{
		yield return null;
		rigidbody2d.velocity = Vector3.zero;
	}
}

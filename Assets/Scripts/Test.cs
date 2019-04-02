using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	Rigidbody2D rigidbody2d;

	private void Awake()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		rigidbody2d.velocity = Vector3.zero;
		rigidbody2d.AddForce(Vector2.right * 1, ForceMode2D.Impulse);

		Debug.Log(rigidbody2d.velocity);

		rigidbody2d.velocity = Vector3.zero;
		rigidbody2d.AddForce(Vector2.up * 1, ForceMode2D.Impulse);

		Debug.Log(rigidbody2d.velocity);
	}
}

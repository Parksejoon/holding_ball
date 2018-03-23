using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitWall : MonoBehaviour
{
	// 충돌
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball")
		{
			collision.transform.position = Vector3.zero;
		}
	}

}

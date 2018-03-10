using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 볼이나 홀더일경우
		if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Holder")
		{
			// 벽 반응
		}
	}
}

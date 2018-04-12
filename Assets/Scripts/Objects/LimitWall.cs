using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitWall : MonoBehaviour
{
	// 충돌
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 공은 원상태로 복구
		if (collision.gameObject.tag == "Ball")
		{
			collision.transform.position = Vector3.zero;
		}

		// 홀더와 코인은 파괴처리
		if (collision.gameObject.tag == "Holder" || collision.gameObject.tag == "Coin")
		{
			Destroy(collision.gameObject);
		}
	}

}

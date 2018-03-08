using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 인스펙터 노출 변수
	// 수치
    [SerializeField] private int reflexY = 1;                // 물체의 X 반전여부
    [SerializeField] private int reflexX = 1;                // 물체의 Y 반전여부


	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 볼이나 홀더일경우
		if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Holder")
		{
			Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>().velocity);

			// reflection
			Vector2 normalVec = collision.contacts[0].normal;
			Vector2 ballVelo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

			collision.gameObject.GetComponent<Rigidbody2D>().velocity = ballVelo + 2 * normalVec * (Vector2.Dot(-ballVelo, normalVec));
		}
	}
}

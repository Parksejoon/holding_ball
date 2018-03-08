using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 인스펙터 노출 변수
    [SerializeField] private int reflexY = 1;                // 물체의 X 반전여부
    [SerializeField] private int reflexX = 1;                // 물체의 Y 반전여부


    // 트리거 진입
    private void OnTriggerEnter2D(Collider2D other)
    {
		//// 볼일경우 튕겨냄
        //if (other.tag == "Ball")
        //{
        //    Vector2 ballVelocity = other.GetComponent<Rigidbody2D>().velocity;
            
        //    other.GetComponent<Rigidbody2D>().velocity = new Vector2(ballVelocity.x * reflexX, ballVelocity.y * reflexY);
        //}
    }

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 볼일경우
		if (collision.gameObject.tag == "Ball")
		{
			// reflection
			Vector2 normalVec = collision.contacts[0].normal;
			Vector2 ballVelo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

			collision.gameObject.GetComponent<Rigidbody2D>().velocity = ballVelo + 2 * normalVec * (Vector2.Dot(-ballVelo, normalVec)) * 150;
		}
	}
}

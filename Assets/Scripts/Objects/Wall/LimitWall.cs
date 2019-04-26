using UnityEngine;

public class LimitWall : MonoBehaviour
{
	// 충돌
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 공은 원상태로 복구
		if (collision.CompareTag("Ball"))
		{
			collision.transform.position = Vector3.zero;
			collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}

		// 홀더와 코인은 파괴처리
		if (collision.gameObject.CompareTag("Holder") || collision.gameObject.CompareTag("Coin"))
		{
			Destroy(collision.gameObject);
		}
	}

}

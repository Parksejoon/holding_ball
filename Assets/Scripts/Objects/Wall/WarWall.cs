using UnityEngine;

public class WarWall : MonoBehaviour
{
	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 홀더일경우
		if (collision.gameObject.CompareTag("Holder"))
		{
			// 파괴 전 홀더 검사
			// GameManager.instance.HolderCheck(collision.gameObject);
			ObjectPoolManager.Release("Holder", collision.gameObject);
		}

		// 코인일경우
		if (collision.gameObject.CompareTag("Coin"))
		{
			Coin targetCoin = collision.gameObject.GetComponent<Coin>();
			
			Destroy(collision.gameObject);
		}
	}
}

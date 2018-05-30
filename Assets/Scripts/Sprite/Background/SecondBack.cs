using UnityEngine;
using Random = UnityEngine.Random;

namespace Sprite.Background
{
	public class SecondBack : MonoBehaviour
	{
		// 초기화
		private void Awake()
		{
			Initialize();
		}

		// 전체 초기화
		private void Initialize()
		{
			transform.position   = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
			transform.localScale = new Vector2(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
		}
	}
}

using UnityEngine;

namespace Sprite
{
	public class MatColorFinder : MonoBehaviour
	{
		// 초기화
		void Start()
		{
			SpriteRenderer sr =	GetComponent<SpriteRenderer>();

			sr.color = sr.material.GetColor("_Color");
		}
	}
}

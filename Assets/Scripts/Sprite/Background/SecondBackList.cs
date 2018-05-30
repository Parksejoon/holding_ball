using System;
using UnityEngine;

namespace Sprite.Background
{
	public class SecondBackList : MonoBehaviour
	{
		// 인스펙터 노출 변수
		// 일반
		[SerializeField]
		private GameObject	starPrefab;				// 별 프리팹
	
		// 수치
		[SerializeField]
		private int			starCount;              // 별 갯수		
		[SerializeField]
		private float	    rotationSpeed;			// 회전 속도


		// 초기화
		private void Awake()
		{
			for (int i = 0; i < starCount; i++)
			{
				Instantiate(starPrefab, transform);
			}
		}

		// 프레임
		private void Update()
		{
			transform.Rotate(Vector3.forward * rotationSpeed * GameManager.instance.timeValue);
		}
	}
}

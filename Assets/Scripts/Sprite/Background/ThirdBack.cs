using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sprite.Background
{
	public class ThirdBack : MonoBehaviour
	{
		// 인스펙터 비노출 변수
		// 일반
		private float	rotationSpeed;           // 회전률


		// 초기화
		private void Awake()
		{
			Initialize();
		}

		// 프레임
		private void Update()
		{
			transform.Rotate(Vector3.forward * rotationSpeed * GameManager.instance.timeValue);
		}

		// 전체 초기화
		private void Initialize()
		{
			rotationSpeed = Random.Range(-0.2f, 0.2f);

			transform.localPosition		= new Vector2(Random.Range(-22, 22), SetFloatPos(Random.Range(-5, 5)));
			transform.localScale		= new Vector2(Random.Range(5f, 6f), Random.Range(5f, 6f));
			transform.rotation			= Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 90)));
		}

		// 위치 설정
		private float SetFloatPos(float position)
		{
			if (position < 0)
			{
				return position - 25;
			}

			if (position >= 0)
			{
				return position + 25;
			}

			return 100f;
		}
	}
}

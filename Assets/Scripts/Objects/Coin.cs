using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
	public class Coin : MonoBehaviour
	{
		// 인스펙터 노출 변수
		// 일반
		[SerializeField]
		private ParticleSystem	destroyParticle;
		
		// 수치
		public float			bounceCount; 	         // 튕길 수 있는 횟수


		// 시작
		private void Start()
		{
			bounceCount = Random.Range(1, 4);
		}

		// 매 프레임
		private void Update()
		{
			transform.Rotate(new Vector3(0, 0, 3) * GameManager.timeValue);
		}

		// 파괴 이펙트
		public void DestroyParticle()
		{
			// 파티클 생성
			Instantiate(destroyParticle, transform.position, Quaternion.identity);
		}
	}
}

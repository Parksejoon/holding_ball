﻿using System.Collections.Generic;
using Systems.InGameSystems;
using Objects.Peripheral;
using UnityEngine;

namespace Objects.Ball
{
	public class ShotLineCollider : MonoBehaviour
	{
		// 인스펙터 노출 변수
		// 수치
		[SerializeField]
		private Material		powerHolderMat;       // 강화 홀더의 머티리얼
		[SerializeField]
		private ParticleSystem	powerHolderParticle;  // 강화 홀더의 이펙트 파티클
		[SerializeField]
		private float		    perfectDis;			  // 퍼펙트 판정범위
		[SerializeField]
		private float		    goodDis;	  		  // 굿 판정범위

		// 인스펙터 비노출 변수
		// 일반
		[HideInInspector]
		public List<Transform>  perfect;              // 퍼펙트 판정 오브젝트 리스트
		[HideInInspector]
		public List<Transform>  good;                 // 일반 판정 오브젝트 리스트
		[HideInInspector]
		public List<Transform>  holderList;           // 홀더 리스트

	
		// 판정 측정
		public void Judgment()
		{
			// 초기화
			float range = transform.lossyScale.x / 2f;		// 반지름
			float x		= transform.position.x;             // 중심 X좌표
			float y		= transform.position.y;             // 중심 Y좌표

			// 타겟 목록 초기화
			perfect		   = new List<Transform>();
			good		   = new List<Transform>();

			// 홀더 리스트 복사
			holderList = GameObject.Find("GameManager").GetComponent<HolderManager>().holderList;
		
			// 홀더들을 불러와 판정
			for (int i = 0; i < holderList.Count; i++)
			{
				float	distance = 0;                                       // 공과의 거리
				float	holdDistance = 0;									// 홀더와의 거리

				if (holderList[i] != null)
				{
					Vector3 holderListPosition = holderList[i].position;        // 홀더 각자의 좌표
				
					// 거리를 측정해서 판정진행
					distance = Mathf.Sqrt(((holderListPosition.x - x) * (holderListPosition.x - x)) + ((holderListPosition.y - y) * (holderListPosition.y - y)));
					holdDistance = Mathf.Abs(distance - range);

					// 퍼펙트
					if (holdDistance < perfectDis)
					{
						perfect.Add(holderList[i]);
					
						ChangeHolder(i, ScoreCompute(distance));

					}
					// 굿
					else if (holdDistance < goodDis)
					{
						good.Add(holderList[i]);
					
						ChangeHolder(i, ScoreCompute(distance));
					}
				}
			}
		}

		// 홀더 변환
		private void ChangeHolder(int i, int score)
		{
			holderList[i].gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			holderList[i].gameObject.tag = "PowerHolder";

			Holder targetHolder = holderList[i].gameObject.GetComponent<Holder>();

			targetHolder.holderPower     = score;
			targetHolder.rotationPower   = 0;
			targetHolder.destroyParticle = powerHolderParticle;
			targetHolder.StartDestroyer();

			holderList[i].GetChild(0).gameObject.GetComponent<Renderer>().material = powerHolderMat;
		}

		// 점수 계산기
		private int ScoreCompute(float distance)
		{
			int score = 0;

			for (int range = 5; range <= distance; range += 5)
			{
				score++;
			}

			return score;
		}
	}
}
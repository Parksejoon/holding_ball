using System;
using UnityEngine;

namespace Particle
{
	public class ParticleColorFinder : MonoBehaviour
	{
		// 인스펙터 노출 변수
		// 수치
		[SerializeField]
		private bool					  isColor;	                // 시작시 기본 컬러로 시작할지
		[SerializeField]
		private ShaderManager.Theme	      colorTheme;				// 색 테마

		// 인스펙터 비노출 변수
		// 일반
		private ParticleSystem.MainModule thisParticle;             // 현재 파티클


		// 초기화
		private void Awake()
		{
			thisParticle = GetComponent<ParticleSystem>().main;
		}

		// 시작
		private void Start()
		{
			if (isColor)
			{
				ChangeToMainColor();
			}
		}

		// 메인색으로 재설정
		private void ChangeToMainColor()
		{
			thisParticle.startColor = Color.HSVToRGB(ShaderManager.themeColor[(int)colorTheme], 0.4f, 0.9f);
		}

		// 하얀색으로 재설정
		private void ChangeToNoneColor()
		{
			thisParticle.startColor = Color.white;
		}
	}
}

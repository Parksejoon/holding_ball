﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorFinder : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private bool					  isDefault;			    // 시작시 기본 컬러로 시작할지

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
		if (isDefault)
		{
			ChangeToMainColor();
		}
	}

	// 메인색으로 재설정
	private void ChangeToMainColor()
	{
		thisParticle.startColor = Color.HSVToRGB(ShaderManager.nowH, 0.4f, 0.9f);
	}

	// 하얀색으로 재설정
	private void ChangeToNoneColor()
	{
		thisParticle.startColor = new Color(1f, 1f, 1f);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	// 중간 중간 하단에 터치, 드래그에 대해 텍스트를 통해 알려줌
	// ====== 튜토리얼 구성 =====
	// 1. 터치로 이동
	// 2. 드래그로 대쉬
	//	2-1. 대쉬를 사용하면 벽 통과 가능
	//	 2-1-1. 벽에 부딧히면 대쉬 초기화
	//	2-2. 대쉬를 사용하면 무적
	// 3. 홀딩시 원이 생김
	//	3-1. 원 안에 별들은 점수가됨
	// 4. 끝날 때 화이트 페이트 + 줌인 + 카메라 쉐이크

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Orbit	firstWallOrbit;		// 

	
}

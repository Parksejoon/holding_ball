using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLineCollider : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
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
        perfect = new List<Transform>();
        good	= new List<Transform>();

		// 홀더 리스트 복사
		holderList = GameObject.Find("GameManager").GetComponent<HolderManager>().holderList;
		
		// 홀더들을 불러와 판정
		for (int i = 0; i < holderList.Count; i++)
        {
            float	distance = 0;										// 거리

			// *issue : 홀더 정보를 얻기 전에 파괴됨*
			if (holderList[i] != null)
			{
				Vector3 holderListPosition = holderList[i].position;        // 홀더 각자의 좌표

				// 거리를 측정해서 판정진행
				distance = Mathf.Sqrt(((holderListPosition.x - x) * (holderListPosition.x - x)) + ((holderListPosition.y - y) * (holderListPosition.y - y)));
				distance = Mathf.Abs(distance - range);

				// 퍼펙트
				if (distance < perfectDis)
				{
					perfect.Add(holderList[i]);
				}
				// 굿
				else if (distance < goodDis)
				{
					good.Add(holderList[i]);
				}
			}
		}

        return;
    }
}

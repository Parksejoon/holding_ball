using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	private int				stack = 5;				// 방어력 스택

	// 인스펙터 비노출 변수
	// 일반
	private BoxCollider2D	boxCollider2D;			// 이 물체의 충돌체
	private GameObject		spriteObj;              // 스프라이트 오브젝트

	
	// 초기화
	private void Awake()
	{
		boxCollider2D	= GetComponent<BoxCollider2D>();
		spriteObj		= transform.GetChild(0).gameObject;
	}

	// 시작
	private void Start()
	{
		UIEffecter.instance.FadeEffect(spriteObj, Vector2.one, 0.5f, UIEffecter.FadeFlag.ALPHA);
	}

	// 벽 대미지 적용
	public void DamDealWall()
	{
		StartCoroutine(DamDealWallCor());
	}

	// 파괴 애니메이션
	private IEnumerator DamDealWallCor()
	{
		stack--;

		UIEffecter.instance.FadeEffect(spriteObj, Vector2.zero + (new Vector2(0.2f, 0.2f) * stack), 0.5f, UIEffecter.FadeFlag.ALPHA);

		// 스택이 0이되면 파괴
		if (stack <= 0)
		{
			boxCollider2D.enabled = false;

			yield return new WaitForSeconds(1f);

			Destroy(gameObject);
		}
	}
}

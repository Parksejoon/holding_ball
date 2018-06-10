using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	public  int				stack = 3;				// 방어력 스택

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
		UIEffecter.instance.FadeEffect(spriteObj, Vector2.one, 1f, UIEffecter.FadeFlag.ALPHA);
		AddStack(0);
	}

	// 스택 재조정
	public void AddStack(int value)
	{
		stack += value;

		if (stack == 0)
		{
			StartCoroutine(DestroyAnimation());
		}
		else
		{
			UIEffecter.instance.FadeEffect(spriteObj, new Vector2(0.2f * stack, 0), 1f, UIEffecter.FadeFlag.ALPHA);
		}
	}

	// 파괴 애니메이션
	private IEnumerator DestroyAnimation()
	{
		boxCollider2D.enabled = false;
		UIEffecter.instance.FadeEffect(spriteObj, Vector2.zero, 1.5f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(3f);

		Destroy(gameObject);
	}
}

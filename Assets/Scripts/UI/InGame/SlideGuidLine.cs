using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideGuidLine : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private GameObject[]	images;
	private IEnumerator		animationCor;


	// 초기화
	private void Awake()
	{
		Image[] temp = GetComponentsInChildren<Image>();

		images = new GameObject[temp.Length];
		for (int i = 0; i < temp.Length; i++)
		{
			images[i] = temp[i].gameObject;
		}
	}

	// 애니메이션 시작
	public void StartAnimation()
	{
		animationCor = AnimationCoroutine();
		StartCoroutine(animationCor);
	}

	// 애니메이션 중지
	public void StopAnimation()
	{
		if (animationCor != null)
		{
			StopCoroutine(animationCor);
		}
	}

	// 애니메이션 코루틴
	private IEnumerator AnimationCoroutine()
	{
		for (int i = 0; i < images.Length; i++)
		{
			UIEffecter.instance.FadeEffect(images[i], new Vector2(0.5f, 0.5f), 0.2f, UIEffecter.FadeFlag.ALPHA);

			yield return new WaitForSeconds(0.3f);

			UIEffecter.instance.FadeEffect(images[i], Vector2.zero, 0.5f, UIEffecter.FadeFlag.ALPHA);
		}

		yield return new WaitForSeconds(1f);

		animationCor = AnimationCoroutine();
		StartCoroutine(animationCor);
	}
}

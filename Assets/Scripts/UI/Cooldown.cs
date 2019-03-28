using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
	// 인스펙터 노출 변수
	[SerializeField]
	private Image	targetImg;			// 이미지


	// 쿨다운
	public void StartCooldown(float coolTime)
	{
		StartCoroutine(CooldownCorutine(coolTime));
	}

	// 쿨다운 코루틴
	private IEnumerator CooldownCorutine(float coolTime)
	{
		float cool = coolTime;

		targetImg.fillAmount = 1;

		while (cool >= 0)
		{
			cool -= 0.005f;
			targetImg.fillAmount = 1 - (coolTime - cool) / coolTime;
			yield return new WaitForSeconds(0.005f);
		}

		targetImg.fillAmount = 0;
	}
}

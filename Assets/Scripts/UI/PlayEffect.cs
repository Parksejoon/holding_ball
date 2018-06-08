using UnityEngine;
using System.Collections;

public class PlayEffect : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private GameObject	playParticle;

	// 파티클 시작
	public void StartEffect()
	{
		StartCoroutine(ParticleRoutine());
	}

	// 입자 생성
	private void CreateParticle()
	{
		GameObject target;

		target = Instantiate(playParticle, transform.position, Quaternion.identity, transform);
		UIEffecter.instance.FadeEffect(target, new Vector2(60, 60), 2f, UIEffecter.FadeFlag.SCALE);
		UIEffecter.instance.FadeEffect(target, new Vector2(0, 0), 0.4f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(target, Vector2.zero, 5f, UIEffecter.FadeFlag.FINDESTROY);
	}

	// 일정 시간동안 수행 후 파괴
	private IEnumerator ParticleRoutine()
	{
		CreateParticle();

		yield return new WaitForSeconds(0.15f);

		CreateParticle();


		Destroy(this);
	}
}

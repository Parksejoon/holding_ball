using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParticlePicker : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	// 일반
	public	Material		material;			// 머티리얼

	// 수치
	public	int				index;				// 파티클 인덱스

	// 인스펙터 비노출 변수
	// 일반
	private Image			image;				// 이미지
	private ParticleSystem	particle;			// 파티클

	// 수치
	private float			speed = 40f;		// 회전 속도


	// 초기화
	private void Awake()
	{
		image			= GetComponent<Image>();
		particle		= GetComponentInChildren<ParticleSystem>();

		particle.Stop();
	}

	// 시작
	private void Start()
	{
		material.SetTexture("_MainTex", BallParticleManager.instance.particleTextures[index].texture);
	}

	// 클릭시
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		BallParticleManager.instance.SetBallParticle(index);
	}

	// 파티클 온 오프
	public void SetParticle(bool enabled)
	{
		if (enabled)
		{
			particle.Play();
		}
		else
		{
			particle.Stop();
		}
	}
}

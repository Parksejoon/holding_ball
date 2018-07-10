using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParticlePicker : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	// 수치
	public	int		index;          // 파티클 인덱스

	// 인스펙터 비노출 변수
	// 일반
	private Image	image;           // 이미지


	// 초기화
	private void Awake()
	{
		image = GetComponent<Image>();	
	}

	// 시작
	private void Start()
	{
		image.sprite = BallParticleManager.instance.particleTextures[index];
	}

	// 클릭시
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		BallParticleManager.instance.SetBallParticle(index);
	}
}

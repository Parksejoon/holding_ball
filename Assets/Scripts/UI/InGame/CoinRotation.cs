using UnityEngine;

public class CoinRotation : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private RectTransform	rectTransform;           // 사각 트랜스


	// 초기화
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	// 프레임
	private void Update()
	{
		rectTransform.Rotate(Vector3.forward * 0.1f);
	}
}

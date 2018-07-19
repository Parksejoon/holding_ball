using UnityEngine;

public class SelfMovement : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 수치
	private Vector2		originPos;              // 원래 위치
	private float		randomValue;			// 랜덤값
	

	// 시작
	private void Start()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 90f)));

		originPos = transform.localPosition;
		randomValue = Random.Range(-20, 20);
	}

	// 프레임
	private void Update()
	{
		float moveValue = Mathf.Cos(Time.timeSinceLevelLoad + randomValue) * 80 * Time.timeScale;

		transform.localPosition = new Vector2(originPos.x, originPos.y + moveValue);
		transform.Rotate(new Vector3(0, 0, moveValue * 0.001f));
	}
}

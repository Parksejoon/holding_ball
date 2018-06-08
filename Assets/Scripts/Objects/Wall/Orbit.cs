using UnityEngine;

public class Orbit : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private Wall[]	walls;                      // 일반 벽들

	// 수치
	private float	rotationSpeed;              // 회전 속도
	private int		nextWallIndex = 1;			// 다음 벽 인덱스


	// 초기화 
	private void Awake()
	{
		walls			= GetComponentsInChildren<Wall>();
		rotationSpeed	= Random.Range(-0.5f, 0.5f);
	}

	// 시작 초기화
	private void Start()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
	}
}

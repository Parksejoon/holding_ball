using System.Collections;
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
		ResetRotationSpeed(1);
	}

	// 시작 초기화
	private void Start()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

		CreateWall(Random.Range(30, 38));
	}

	// 매 프레임
	private void FixedUpdate()
	{
		transform.Rotate(Vector3.forward * rotationSpeed);
	}

	// 회전 초기화 
	public void ResetRotationSpeed(float val)
	{
		val = Mathf.Min(7, val);

		rotationSpeed = Random.Range(-0.5f * val, 0.5f * val);
	}

	// 벽 생성
	public void CreateWall(int size)
	{
		for (int i = 0; i < size; i++)
		{
			GameObject target = Instantiate(WallManager.instance.wallPrefab, Vector3.zero, Quaternion.identity, transform);

			target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 6.7f * nextWallIndex++));
		}
	}
}

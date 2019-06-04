using System.Collections;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	// 인스펙터 비노출 변수

	// 수치
	private float	rotationSpeed;              // 회전 속도
	private int		nextWallIndex = 1;          // 다음 벽 인덱스
	private bool	colliderEnabled = false;	// 충돌체 상태


	// 초기화 
	private void Awake()
	{
		ResetRotationSpeed();
	}

	// 시작 초기화
	private void Start()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
	}

	// 매 프레임
	private void FixedUpdate()
	{
		transform.Rotate(Vector3.forward * rotationSpeed);
	}

	// 회전 초기화 
	public void ResetRotationSpeed()
	{
		rotationSpeed = Random.Range(-0.5f, 0.5f);
	}

	// 벽 생성
	public void CreateWall(int size, int stack)
	{
		for (int i = 0; i < size; i++)
		{
			GameObject	target		= Instantiate(WallManager.instance.wallPrefab, Vector3.zero, Quaternion.identity, transform);
			Wall		targetWall	= target.GetComponent<Wall>();

			target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 32.5f * nextWallIndex++));
			targetWall.stack = stack;

			if (colliderEnabled)
			{
				target.GetComponent<BoxCollider2D>().enabled = true;
			}
		}
	}

	// 충돌체 끄고 키기
	public void SetCollider(bool enabled)
	{
		BoxCollider2D[] colliders = transform.GetComponentsInChildren<BoxCollider2D>();

		colliderEnabled = enabled;

		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].enabled = enabled;
		}
	}
}

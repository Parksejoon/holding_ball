using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	laserPrefab;                // 레이저 프리팹

	// 수치
	public int			amountPerShot = 1;			// 한 번 발사시 나가는 양

	[SerializeField]
	private float		minDelay;					// 최소 딜레이
	[SerializeField]
	private float		maxDelay;					// 최대 딜레이


	// 초기화
	private void Awake()
	{
	}

	// 시작
	private void Start()
	{
		ObjectPoolManager.AddObject("Laser", laserPrefab, transform);
		ObjectPoolManager.Create("Laser", 50);

		StartCoroutine(LaserLoop());
	}

	// 레이저 생성
	public void CreateLaser(float speed)
	{
		Laser targetLaser = 
			ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))).GetComponent<Laser>();

		targetLaser.rotationSpeed = speed;
	}

	// 레이저 루틴
	private IEnumerator LaserLoop()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

			for (int i = 0; i < GameManager.instance.level; i++)
			{
				CreateLaser(0);
			}
		}
	}
}

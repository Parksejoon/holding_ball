using UnityEngine;

public class WallManager : MonoBehaviour
{
	public static WallManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Transform			wallsTransform;             // 벽들의 트랜스폼

	public  GameObject			wallPrefab;	                // 벽 프리팹
	
	// 수치
	[SerializeField]
	private float				originalRotationSpeed = 1f; // 원시 회전속도

	// 인스펙터 비노출 변수
	// 일반
	private Orbit[]				orbits;						// 궤도들
	private float				rotationSpeed;				// 회전속도


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;				
		}
		
		orbits			= GetComponentsInChildren<Orbit>();

		rotationSpeed = originalRotationSpeed;
	}

	// 시작
	private void Start()
	{

		for (int i = 0; i < orbits.Length; i++)
		{
			// 가장 적게 (5, 8)
			orbits[i].CreateWall(Random.Range(30, 40), i + 1);
		}
	}

	// 프레임
	private void Update()
	{
		wallsTransform.Rotate(Vector3.forward * rotationSpeed * GameManager.instance.timeValue * Time.deltaTime);
	}

	// 벽들 새로 생성
	public void CreateWalls()
	{
		int level = GameManager.instance.level;

		for (int i = 0; i < Mathf.Min(3, level * 2); i++)
		{
			orbits[Random.Range(0, 5)].CreateWall(Random.Range(level * 10, level * 30), orbits.Length - i);
		}
	}

	// 벽 새로 생성
	public void CreateWall(int orbitNum, int wallCount, int wallStack)
	{
		orbits[orbitNum].CreateWall(wallCount, wallStack);
	}
}

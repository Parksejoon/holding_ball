using UnityEngine;

public class WallManager : MonoBehaviour
{
	public static WallManager instance;
		
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject			warWallPrefab;				// 위험 벽 프리팹
	[SerializeField]
	private Material			warWallsMat;                // 위험 벽 질감

	// 수치
	[SerializeField]
	private float				originalRotationSpeed = 1f; // 원시 회전속도

	// 인스펙터 비노출 변수
	// 일반
	private Transform			wallsTransform;             // 벽들의 트랜스폼
	private Wall[]				walls;						// 일반 벽들
	private float				rotationSpeed;				// 회전속도


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;				
		}

		wallsTransform  = GameObject.Find("Walls"). GetComponent<Transform>();
		walls			= GetComponentsInChildren<Wall>();

		rotationSpeed = originalRotationSpeed;
	}

	// 프레임
	private void Update()
	{
		wallsTransform.Rotate(Vector3.forward * rotationSpeed * GameManager.instance.timeValue);
	}

	// 끝날때
	private void OnDestroy()
	{
		Color warWallColor = warWallsMat.GetColor("_Color");

		warWallsMat.SetColor("_Color", new Color(warWallColor.r, warWallColor.g, warWallColor.b, 1f));
	}

	// 벽 초기화
	public void InitWalls()
	{
		// 벽 체력 초기화
		foreach (Wall wall in walls)
		{
			wall.ResetHP();
		}

		// 회전 방향 전환
		rotationSpeed *= -1;
		Wall.signValue *= -1;
	}
}

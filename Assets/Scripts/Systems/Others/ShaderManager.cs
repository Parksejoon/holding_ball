using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	public static ShaderManager instance;
	
	// 인스펙터 노출 변수
	// 수치
	public	Color		particleAdditive;       // 파티클 농도

	// 컬러
	[Space(40)]
	public  Color		baseColor;              // 베이스 컬러
	public	Color		subColor;				// 서브 컬러

	[Space(20)]
	public  Color	    warWallColor;           // 위험 벽 컬러
	public  Color		topBackColor;           // 배경 위쪽 컬러
	public  Color		botBackColor;           // 배경 아래쪽 컬러

	// 베이스 머티리얼
	[Space(40)]
	[SerializeField]
	private Material[]	baseMat;                // 베이스 머티리얼
	[SerializeField]
	private Material[]	varBaseMat;             // 변하는 베이스 머티리얼

	// 서브 머티리얼
	[Space(20)]
	[SerializeField]
	private Material[]	subMat;					// 서브 머티리얼

	// 위험 머티리얼
	[Space(20)]
	[SerializeField]
	private Material[]	warWall;                // 위험 벽 머티리얼

	// 뒷배경 머티리얼
	[Space(20)]
	[SerializeField]
	private Material	backGround;             // 뒷배경 머티리얼
	[SerializeField]
	private Material[]	topBack;                // 뒷배경 위 머티리얼
	[SerializeField]
	private Material[]  botBack;				// 뒷배경 아래 머티리얼

	// 인스펙터 비노출 변수
	// 일반
	Parser				parser;					// 데이터 파서


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		parser		= new Parser();
	}

	// 시작
	private void Start()
	{
		RefreshColor();

		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{

		}
	}

	// 컬러 불러오기
	public void LoadColor()
	{
		int index;

		// 베이스
		index = PlayerPrefs.GetInt("BallColor");
		baseColor = parser.GetColor(index);
		
		// 서브
		index = PlayerPrefs.GetInt("SubColor");
		subColor = parser.GetColor(index);
		
		// 위험 벽
		index = PlayerPrefs.GetInt("WarWallColor");
		warWallColor = parser.GetColor(index);
		
		// 뒷배경 위
		index = PlayerPrefs.GetInt("TopBackColor");
		topBackColor = parser.GetColor(index);
		
		// 뒷배경 아래
		index = PlayerPrefs.GetInt("BotBackColor");
		botBackColor = parser.GetColor(index);
	}

	// 색 설정
	private void SetColor()
	{
		// base
		foreach (Material material in baseMat)
		{
			material.SetColor("_Color", baseColor);
			material.SetColor("_TintColor", baseColor - particleAdditive);
		}

		foreach (Material material in varBaseMat)
		{
			material.SetColor("_Color", baseColor);
			material.SetColor("_TintColor", baseColor - particleAdditive);
		}

		// sub
		foreach (Material material in subMat)
		{
			material.SetColor("_Color", subColor);
			material.SetColor("_TintColor", subColor - particleAdditive);
		}

		// war wall
		foreach (Material material in warWall)
		{
			material.SetColor("_Color", warWallColor);
			material.SetColor("_TintColor", warWallColor);
		}

		// back
		backGround.SetColor("_TopColor", topBackColor);
		backGround.SetColor("_BotColor", botBackColor);

		// top back
		foreach (Material material in topBack)
		{
			material.SetColor("_Color", topBackColor);
			material.SetColor("_TintColor", topBackColor);
		}

		// bot back
		foreach (Material material in botBack)
		{
			material.SetColor("_Color", botBackColor);
			material.SetColor("_TintColor", botBackColor);
		}
	}

	// 색 변경
	public void ChangeBaseColor(bool colored)
	{
		Color color;

		if (colored)
		{
			color = baseColor;
		}
		else
		{
			color = subColor;
		}

		for (int i = 0; i < varBaseMat.Length; i++)
		{
			varBaseMat[i].SetColor("_Color", color);
			varBaseMat[i].SetColor("_TintColor", color - particleAdditive);
		}
	}

	// 색 갱신
	public void RefreshColor()
	{
		LoadColor();
		SetColor();
	}
}

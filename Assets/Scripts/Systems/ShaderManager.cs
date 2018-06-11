using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	public static ShaderManager instance;
	
	static public float[] themeColor;           // 테마 컬러들

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

	// 나머지 머티리얼
	[Space(20)]
	[SerializeField]
	private Material	backGround;             // 뒷배경 머티리얼
	[SerializeField]
	private Material	warWall;				// 위험 벽 머티리얼
	
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

		themeColor = new float[3];

		parser = new Parser();
	}

	// 시작
	private void Start()
	{
		parser.GetColor(this);

		InitializeColor();
		SetColor();
	}

	// 색 초기화
	private void InitializeColor()
	{
		// BASE SIDE BACK순서
		float temp;
		float baseH, subH, backH;

		Color.RGBToHSV(baseColor, out baseH, out temp, out temp);
		themeColor[0] = baseH;

		Color.RGBToHSV(subColor, out subH, out temp, out temp);
		themeColor[1] = subH;

		Color.RGBToHSV(botBackColor, out backH, out temp, out temp);
		themeColor[2] = backH;
	}

	// 색 설정
	private void SetColor()
	{
		// base
		foreach (Material material in baseMat)
		{
			material.SetColor("_Color", baseColor);
		}

		foreach (Material material in varBaseMat)
		{
			material.SetColor("_Color", baseColor);
		}

		// sub
		foreach (Material material in subMat)
		{
			material.SetColor("_Color", subColor);
		}

		// others
		// war wall
		warWall.SetColor("_Color", warWallColor);

		// back
		backGround.SetColor("_TopColor", topBackColor);
		backGround.SetColor("_BotColor", botBackColor);
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
}

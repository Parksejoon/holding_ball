using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	public enum Theme
	{
		BASE = 0,
		SIDE = 1,
		BACK = 2
	}

	static public float[] themeColor;           // 테마 컬러들

	// 인스펙터 노출 변수
	// 일반
	public  Color		baseColor;              // 베이스 컬러
	public  Color		wallColor;              // 벽 컬러
	public  Color	    warWallColor;           // 위험 벽 컬러
	public  Color		topBackColor;           // 배경 위쪽 컬러
	public  Color		botBackColor;           // 배경 아래쪽 컬러

	[SerializeField]
	private Material	backGround;             // 뒷배경
	[SerializeField]
	private Material	ball;					// 공
	[SerializeField]
	private Material	wall;                   // 벽
	[SerializeField]
	private Material	warWall;				// 위험 벽
	[SerializeField]
	private Material	powHolder;              // 강화 홀더

	// 인스펙터 비노출 변수
	// 일반
	Parser				parser;					// 데이터 파서


	// 초기화
	private void Awake()
	{
		themeColor = new float[3];

		parser = new Parser();
	}

	// 시작
	private void Start()
	{
		parser.GetColor(this);

		InitializeColor();
		ChangeColor();
	}

	// 색 초기화
	private void InitializeColor()
	{
		// BASE SIDE BACK순서
		float temp;
		float baseH, sideH, backH;

		Color.RGBToHSV(baseColor, out baseH, out temp, out temp);
		themeColor[0] = baseH;

		Color.RGBToHSV(wallColor, out sideH, out temp, out temp);
		themeColor[1] = sideH;

		Color.RGBToHSV(botBackColor, out backH, out temp, out temp);
		themeColor[2] = backH;
	}

	// 색 변경
	private void ChangeColor()
	{
		// base
		ball.SetColor("_Color", baseColor);
		powHolder.SetColor("_Color", Color.HSVToRGB(themeColor[(int)Theme.BASE], 0.2f, 1f));

		// wall
		wall.SetColor("_Color", wallColor);
		warWall.SetColor("_Color", warWallColor);
		
		// back
		backGround.SetColor("_TopColor", topBackColor);
		backGround.SetColor("_BotColor", botBackColor);
	}

	// 공 색 변경
	public void BallColor(bool isDouble)
	{
		// 더블 ( 유색 )
		if (isDouble)
		{
			ball.SetColor("_Color", baseColor);
		}
		// 무색
		else
		{
			ball.SetColor("_Color", Color.white);
		}
	}
}

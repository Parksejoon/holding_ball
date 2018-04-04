using System.Collections;
using System.Collections.Generic;
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
	public  Color		baseColor;              // 주 컬러
	public  Color		sideColor;              // 보조 컬러
	public  Color		backColor;              // 배경 컬러

	[SerializeField]
	private Material	backGround;             // 뒷배경
	[SerializeField]
	private Material	ball;					// 공
	[SerializeField]
	private Material	wall;                   // 벽
	[SerializeField]
	private Material	warWall;                // 위험 벽
	[SerializeField]
	private Material	powHolder;              // 강화 홀더


	// 초기화
	private void Awake()
	{
		themeColor = new float[3];
	}

	// 시작
	private void Start()
	{
		//InitializeColor();
		//ChangeColor();
	}

	// 색 초기화
	private void InitializeColor()
	{
		// BASE SIDE BACK순서
		float temp;
		float baseH, sideH, backH;

		Color.RGBToHSV(baseColor, out baseH, out temp, out temp);
		themeColor[0] = baseH;

		Color.RGBToHSV(sideColor, out sideH, out temp, out temp);
		themeColor[1] = sideH;

		Color.RGBToHSV(backColor, out backH, out temp, out temp);
		themeColor[2] = backH;
	}

	// 색 변경
	private void ChangeColor()
	{
		// base
		wall.SetColor("_Color", Color.HSVToRGB(themeColor[(int)Theme.BASE], 0.45f, 1f));
		ball.SetColor("_Color", Color.HSVToRGB(themeColor[(int)Theme.BASE], 0.5f, 1f));
		powHolder.SetColor("_Color", Color.HSVToRGB(themeColor[(int)Theme.BASE], 0.2f, 1f));

		// side
		warWall.SetColor("_Color", Color.HSVToRGB(themeColor[(int)Theme.SIDE], 0.45f, 1f));

		// back
		backGround.SetColor("_TopColor", Color.HSVToRGB(themeColor[(int)Theme.BACK], 0.3f, 0.9f));
		backGround.SetColor("_BotColor", Color.HSVToRGB(themeColor[(int)Theme.BACK], 0.8f, 0.2f));
	}

	// 공 색 변경
	public void BallColor(bool isDouble)
	{
		// 더블 ( 유색 )
		if (isDouble)
		{
			ball.SetColor("_Color", Color.HSVToRGB(themeColor[0], 0.5f, 1f));
		}
		// 무색
		else
		{
			ball.SetColor("_Color", Color.HSVToRGB(themeColor[0], 0f, 1f));
		}
	}
}

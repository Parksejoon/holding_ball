using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	// 정적 변수
	public static float ballColorS = 0.6f;      // 공 색(HSV)중 S값
	public static float nowH = 0;				// 현재의 H값

	// 인스펙터 노출 변수
	// 일반
	public	Color		mainColor;				// 메인 컬러

	[SerializeField]
	private Material	backGround;             // 뒷배경
	[SerializeField]
	private Material	ball;					// 공
	[SerializeField]
	private Material	wall;                   // 벽
	[SerializeField]
	private Material	warWall;                // 위험 벽
	[SerializeField]
	private Material	powHolder;	            // 강화 홀더


	// 시작
	private void Update()
	{
		float temp;
		float realH;

		Color.RGBToHSV(mainColor, out realH, out temp, out temp);
		ChangeColor(realH);
	}

	// 색 변경
	private void ChangeColor(float H)
	{
		backGround.SetColor("_TopColor", Color.HSVToRGB(H, 0.3f, 0.9f));
		backGround.SetColor("_BotColor", Color.HSVToRGB(H, 0.8f, 0.2f));
		wall.SetColor("_Color", Color.HSVToRGB(H, 0.45f, 1f));
		ball.SetColor("_Color", Color.HSVToRGB(H, ballColorS, 1f));
		powHolder.SetColor("_Color", Color.HSVToRGB(H, 0.2f, 1f));

		nowH = H;
	}

	// 공 색 변경
	public void BallColor(float S)
	{
		ball.SetColor("_Color", Color.HSVToRGB(nowH, S, 1f));
	}

	// 색 변경 코루틴 ( 무한 반복 )
	public IEnumerator ChangeColorLoop()
	{
		float hue = 0;

		while (true)
		{
			ChangeColor(hue);
			hue = (hue + 0.01f) % 1f;

			yield return new WaitForSeconds(0.05f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	// 정적 변수
	public static float ballColorS = 0.6f;		// 공 색(HSV)중 S값

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Material	backGround;				// 뒷배경 질감
	[SerializeField]
	private Material	ball;					// 공 질감
	[SerializeField]
	private Material	wall;                   // 벽 질감
	[SerializeField]
	private Material	warWall;                // 위험 벽 질감

	// 인스펙터 비노출 변수
	// 수치
	private float		nowH = 0;                // 현재의 H값


	// 시작
	private void Start()
	{
		ChangeColor(0.6f);
	}

	// 색 변경
	private void ChangeColor(float H)
	{
		backGround.SetColor("_TopColor", Color.HSVToRGB(H, 0.3f, 0.9f));
		backGround.SetColor("_BotColor", Color.HSVToRGB(H, 0.8f, 0.2f));
		wall.SetColor("_Color", Color.HSVToRGB(H, 0.45f, 1f));
		ball.SetColor("_Color", Color.HSVToRGB(H, ballColorS, 1f));

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

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerGauge : MonoBehaviour
{
	public static PowerGauge instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Image	gauge;					// 파워 게이지

	// 수치
	[SerializeField]
	private float	power;					// 파워

	// 인스펙터 비노출 변수
	// 수치
	private bool	isStopping = false;		// 중지 플래그
	private bool	isMinus = false;		// 감소 플래그


	// 초기화
	private void Awake()
	{
		instance = this;
	}

	// 시작
	private void Start()
	{
		StartCoroutine(Charge());
	}

	// 파워 설정
	public bool AddPower(float value)
	{
		power += value;

		if (power <= 0)
		{
			power = 0;
			gauge.fillAmount = 0;
			Ball.instance.UnHolding();

			return false;
		}

		if (power >= 100)
		{
			power = 100;
		}

		gauge.fillAmount = power / 100;

		return true;
	}

	// 차지 중지
	public void StopCharge()
	{
		isStopping = true;
		isMinus = true;
	}

	// 차지 다시 시작
	public void ReCharge()
	{
		isMinus = false;
		StartCoroutine(ReChargeCorutine());
	}

	// 차징 코루틴
	private IEnumerator Charge()
	{
		while (true)
		{
			if (!isStopping)
			{
				AddPower(0.05f);
				yield return new WaitForSeconds(0.01f);
			}
			else if (isMinus)
			{
				AddPower(-0.3f);
				yield return null;
			}
			else
			{
				yield return null;
			}
		}
	}

	// 차징 중지 코루틴
	private IEnumerator ReChargeCorutine()
	{
		yield return new WaitForSeconds(1f);

		isStopping = false;
	}
}

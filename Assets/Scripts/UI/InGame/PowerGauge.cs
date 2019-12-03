using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerGauge : MonoBehaviour
{
	public static PowerGauge instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Image				gauge;						// 파워 게이지
	[SerializeField]
	private SpecialLaserManager specialLaserManager;		// 특수 레이저 매니저

	// 수치
	[SerializeField]
	private float	power;					// 파워

	// 인스펙터 비노출 변수
	// 수치
	private bool	isReduce = false;       // 감소 플래그
	private bool	isStop = true;          // 완전 중지 플래그
	private bool	gaugeCool = false;		// 게이지 쿨다운 플래그


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

	// 게이지 시작
	public void StartGauge()
	{
		isReduce = true;
		isStop = false;
	}

	// 파워 설정
	public bool AddPower(float value)
	{
		if (Time.timeScale == 0) return false;

		power += value;

		if (power <= 0)
		{
			power = 0;
			gauge.fillAmount = 0;

			// 게이지 쿨다운중이 아니면
			if (!gaugeCool)
			{
				// 게이지 비어있음
				GaugeEmpty();
			}

			return false;
		}

		if (power >= 100)
		{
			power = 100;
		}

		gauge.fillAmount = power / 100;

		return true;
	}

	// 게이지 0
	private void GaugeEmpty()
	{
		// 게이지 쿨다운 실행
		StartCoroutine(GaugeCoolDownCor());

		// 레이저 특수패턴 실행
		specialLaserManager.ShotLaser();
	}

	// 감소 중지
	public void StopReduce()
	{
		isReduce = false;
	}

	// 감소 다시 시작
	public void ReReduce()
	{
		StartCoroutine(ReReduceCorutine());
	}

	//// 차지 중지
	//public void StopCharge()
	//{
	//	//isStopping = true;
	//	//isMinus = true;
	//}

	//// 차지 다시 시작
	//public void ReCharge()
	//{
	//	//isMinus = false;
	//	//StartCoroutine(ReChargeCorutine());
	//}

	// 차징 코루틴
	private IEnumerator Charge()
	{
		while (true)
		{
			if (!gaugeCool)
			{
				// 감소중이면
				if (isReduce)
				{
					AddPower(-0.2f);
				}
				// 완정중지가 아니면
				else if (!isStop)
				{
					AddPower(-0.6f);
				}
				else
				{
				}
			}

			yield return null;
		}
	}

	// 차징 중지 코루틴
	private IEnumerator ReReduceCorutine()
	{
		isStop = true;

		yield return new WaitForSeconds(1f);

		isStop = false;
		isReduce = true;
	}

	// 게이지 쿨다운 코루틴
	private IEnumerator GaugeCoolDownCor()
	{
		gaugeCool = true;
		
		yield return new WaitForSeconds(2f);

		while (power < 100)
		{
			AddPower(0.2f);

			yield return null;
		}

		yield return new WaitForSeconds(0.5f);

		gaugeCool = false;
	}
}

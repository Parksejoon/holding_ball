using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class UIEffecter : MonoBehaviour
{
    public enum FadeFlag
    {
        POSITION    = 0x01,             // 위치 변경 페이드
        SCALE       = 0x02,             // 크기 변경 페이드
        ALPHA       = 0x04,	            // 알파 변경 페이드
		ANGLE		= 0x08, 			// 각도 변경 페이드
        FINDISABLE  = 0x10,             // 종료 후 UI 끄기
        FINDESTROY  = 0x20              // 종료 후 UI 파괴
    }
        
    public static UIEffecter	instance;
    
    // 인스펙터 노출 변수
    // 일반
    public  Text[]				texts;					// 텍스트 집합
    public  GameObject[]		panels;					// ui 집합
    
    // 수치
    [SerializeField]
    private float				fadeGap = 0.01f;		// 페이드 효과 간격
	[SerializeField]
	private float				finTimeGap = 0.3f;		// 일정 시간 후 종료할 때 뒤에 잠시 부하를 위한 기다림 시간
    
    
    // 초기화
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // UI 텍스트 설정
    public void SetText(int index, string str)
    {
        texts[index].text = str;
    }
    
    // UI 설정
    public void SetUI(int index, bool enable)
    {
        panels[index].gameObject.SetActive(enable);
    }
        
    // UI 페이드 효과
    public void FadeEffect(GameObject target, Vector2 goalVal, float time, FadeFlag optionFlag)
    {
		// 위치 변경 페이드
		if ((optionFlag & FadeFlag.POSITION) == FadeFlag.POSITION)
		{
			StartCoroutine(FadePosition(target.GetComponent<RectTransform>(), goalVal, time));
		}

		// 크기 변경 페이드
		if ((optionFlag & FadeFlag.SCALE) == FadeFlag.SCALE)
		{
			StartCoroutine(FadeScale(target.GetComponent<RectTransform>(), goalVal, time));
		}

		// 알파 변경 페이드
		if ((optionFlag & FadeFlag.ALPHA) == FadeFlag.ALPHA)
		{
			if (target.GetComponent<Image>() != null)
			{
				StartCoroutine(FadeAlpha(target.GetComponent<Image>(), goalVal.x, time));
			}
			else if (target.GetComponent<Text>() != null)
			{
				StartCoroutine(FadeAlpha(target.GetComponent<Text>(), goalVal.x, time));
			}
			else if (target.GetComponent<SpriteRenderer>() != null)
			{
				StartCoroutine(FadeAlpha(target.GetComponent<SpriteRenderer>(), goalVal.x, time));
			}
			else
			{ }
		}

		// 각도 변경 페이드
		if ((optionFlag & FadeFlag.ANGLE) == FadeFlag.ANGLE)
		{
			StartCoroutine(FadeAngle(target.GetComponent<RectTransform>(), goalVal, time));
		}
            
        // 종료 후 enable 설정
        if ((optionFlag & FadeFlag.FINDISABLE) == FadeFlag.FINDISABLE)
        {
            StartCoroutine(AfterEnable(target.gameObject, time, false));
        }
            
        // 종료 후 disable 설정
        if ((optionFlag & FadeFlag.FINDESTROY) == FadeFlag.FINDESTROY)
        {
            StartCoroutine(AfterEnable(target.gameObject, time, true));
		}
    }
	
	// 숫자 변경 효과
	public void ChangeNumberEffect(Text target, int goalValue, float time)
	{
		StartCoroutine(ChangeNumber(target, goalValue, time));
	}

	// 위치 페이드
	private IEnumerator FadePosition(RectTransform target, Vector2 goalPos, float time)
    {
        Vector2 startPos    = target.position;
        int     count       = (int)(time / fadeGap);
        int     originCount = count;
       
        while (count > 0)
        {
			if (target == null)
			{
				break;
			}

			target.position = Vector2.Lerp(goalPos, startPos, (float)count / originCount);
            
            count -= 1;
            yield return new WaitForSeconds(fadeGap);
        }

		if (target != null)
		{
			target.position = goalPos;
		}
    }
    
    // 크기 페이드
    private IEnumerator FadeScale(RectTransform target, Vector2 goalScale, float time)
    {
        Vector2 originScale = target.localScale;
        int     count = (int)(time / fadeGap);
        int     originCount = count;
        
        
        while (count > 0)
		{
			if (target == null)
			{
				break;
			}

			target.localScale = Vector2.Lerp(goalScale, originScale, (float)count / originCount);
            
            count -= 1;
            yield return new WaitForSeconds(fadeGap);
        }

		if (target != null)
		{
			target.localScale = goalScale;
		}
	}

	// 회전 페이드
	private IEnumerator FadeAngle(RectTransform target, Vector2 goalAngle, float time)
	{
		Quaternion originAngle = target.localRotation;
		Quaternion goalAngleQuat = Quaternion.Euler(goalAngle);
		int count = (int)(time / fadeGap);
		int originCount = count;


		while (count > 0)
		{
			if (target == null)
			{
				break;
			}

			target.localRotation = Quaternion.Lerp(goalAngleQuat, originAngle, (float)count / originCount);

			count -= 1;
			yield return new WaitForSeconds(fadeGap);
		}

		if (target != null)
		{
			target.localRotation = goalAngleQuat;
		}
	}

	// 알파 페이드 ( 이미지 )
	private IEnumerator FadeAlpha(Image target, float goalAlpha, float time)
	{
		Color originColor = target.color;
		float startAlpha = target.color.a;
		int count = (int)(time / fadeGap);
		int originCount = count;

		while (count >= 0)
		{
			if (target == null)
			{
				break;
			}

			originColor.a = Mathf.Lerp(goalAlpha, startAlpha, (float)count / originCount);
			target.color = originColor;

			count -= 1;
			yield return new WaitForSeconds(fadeGap);
		}

		originColor.a = goalAlpha;

		if (target != null)
		{
			target.color = originColor;
		}
	}

	// 알파 페이드 ( 텍스트 )
	private IEnumerator FadeAlpha(Text target, float goalAlpha, float time)
	{

		Color originColor = target.color;
		float startAlpha = target.color.a;
		int count = (int)(time / fadeGap);
		int originCount = count;

		while (count > 0)
		{
			if (target == null)
			{
				break;
			}

			originColor.a = Mathf.Lerp(goalAlpha, startAlpha, (float)count / originCount);
			target.color = originColor;

			count -= 1;
			yield return new WaitForSeconds(fadeGap);
		}

		originColor.a = goalAlpha;

		if (target != null)
		{
			target.color = originColor;
		}
	}

	// 알파 페이드 ( 스프라이트 )
	private IEnumerator FadeAlpha(SpriteRenderer target, float goalAlpha, float time)
	{
		Color originColor = target.color;
		float startAlpha = target.color.a;
		int count = (int)(time / fadeGap);
		int originCount = count;

		while (count > 0)
		{
			if (target == null)
			{
				break;
			}

			originColor.a = Mathf.Lerp(goalAlpha, startAlpha, (float)count / originCount);
			target.color = originColor;

			count -= 1;
			yield return new WaitForSeconds(fadeGap);
		}

		originColor.a = goalAlpha;

		if (target != null)
		{
			target.color = originColor;
		}
	}

	// 페이드 종료 후 disalbe or destroy
	private IEnumerator AfterEnable(GameObject target, float time, bool isDes)
    {
        yield return new WaitForSeconds(time + finTimeGap + 0.5f);
        
		if (isDes)
		{
			Destroy(target.gameObject);
		}

        target.SetActive(false);
    }

	// 숫자 변경 효과
	private IEnumerator ChangeNumber(Text target, int goalValue, float time)
	{
		int val;
		int startValue = int.Parse(target.text);
		int count = (int)(time / fadeGap);
		int originCount = count;

		while (count > 0)
		{
			val = (int)Mathf.Lerp(goalValue, startValue, (float)count / originCount);
			target.text = val.ToString();

			count -= 1;
			yield return new WaitForSeconds(fadeGap);
		}
		
		target.text = goalValue.ToString();
	}
}

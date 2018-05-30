using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIEffecter : MonoBehaviour
    {
        public static UIEffecter instance;
    
        // 인스펙터 노출 변수
        // 일반
        [SerializeField]
        private Text[]             texts;				 // 텍스트 집합
        [SerializeField]
        private GameObject[]       panels;              // ui 집합
    
        // 수치
        [SerializeField]
        private float              fadeGap = 0.01f;     // 페이드 효과 간격
    
    
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
    
        // UI 위치 페이드
        public void FadePositionFunc(int index, Vector2 goalPos, float time, bool useFinalEnable, bool finalEnable)
        {
            StartCoroutine(FadePosition(panels[index].GetComponent<RectTransform>(), goalPos, time, useFinalEnable, finalEnable));
        }
    
        // UI 알파 페이드
        public void FadeAlphaFunc(int arrayOffset, int index, float goalAlpha, float time, bool useFinalEnable, bool finalEnable)
        {
            switch (arrayOffset)
            {
                case 0:
                    StartCoroutine(FadeAlpha(panels[index].GetComponent<Image>(), goalAlpha, time, useFinalEnable, finalEnable));
                
                    break;
                case 1:
                    StartCoroutine(FadeAlpha(texts[index], goalAlpha, time, useFinalEnable, finalEnable));
                
                    break;
                default:
                    Debug.Log("arrayOffset");
                
                    break;
            }
        }
    
        // UI 크기 페이드
        public void FadeScaleFunc(int index, Vector2 goalScale, float time, bool useFinalEnable, bool finalEnable)
        {
            StartCoroutine(FadeScale(panels[index].GetComponent<RectTransform>(), goalScale, time, useFinalEnable, finalEnable));
        }
        
        // 옵션 플레그 확인
        private void CheckFlag(GameObject target, float time, bool useFinalEnable, bool finalEnable)
        {
            // 종료 후 enable 설정
            if (useFinalEnable)
            {
                StartCoroutine(AfterEnable(target, time, finalEnable));
            }
        }
    
        // 위치 페이드
        public IEnumerator FadePosition(RectTransform target, Vector2 goalPos, float time, bool useFinalEnable, bool finalEnable)
        {
            CheckFlag(target.gameObject, time, useFinalEnable, finalEnable);
            
            Vector2 startPos    = target.position;
            int     count       = (int)(time / fadeGap);
            int     originCount = count;
       
            while (count > 0)
            {
                target.position = Vector2.Lerp(goalPos, startPos, (float)count / originCount);
            
                count -= 1;
                yield return new WaitForSeconds(fadeGap);
            }

            target.position = goalPos;
        }
    
        // 알파 페이드 ( 이미지 )
        public IEnumerator FadeAlpha(Image target, float goalAlpha, float time, bool useFinalEnable, bool finalEnable)
        {
            CheckFlag(target.gameObject, time, useFinalEnable, finalEnable);

            Color originColor = target.color;
            float startAlpha  = target.color.a;
            int   count       = (int)(time / fadeGap);
            int   originCount = count;      
        
            while (count > 0)
            {
                originColor.a = Mathf.Lerp(goalAlpha, startAlpha, (float)count / originCount);
                target.color  = originColor;
            
                count -= 1;
                yield return new WaitForSeconds(fadeGap);
            }

            originColor.a = goalAlpha;
            target.color = originColor;
        }
    
        // 알파 페이드 ( 텍스트 )
        public IEnumerator FadeAlpha(Text target, float goalAlpha, float time, bool useFinalEnable, bool finalEnable)
        {
            CheckFlag(target.gameObject, time, useFinalEnable, finalEnable);

            Color originColor = target.color;
            float startAlpha  = target.color.a;
            int   count       = (int)(time / fadeGap);
            int   originCount = count;
        
            while (count > 0)
            {
                originColor.a = Mathf.Lerp(goalAlpha, startAlpha, (float)count / originCount);
                target.color  = originColor;
                   
                count -= 1;
                yield return new WaitForSeconds(fadeGap);
            }
        
            originColor.a = goalAlpha;
            target.color = originColor;
        }
    
        // 크기 페이드
        public IEnumerator FadeScale(RectTransform target, Vector2 goalScale, float time, bool useFinalEnable, bool finalEnable)
        {
            CheckFlag(target.gameObject, time, useFinalEnable, finalEnable);

            Vector2 originScale = target.localScale;
            int     count = (int)(time / fadeGap);
            int     originCount = count;
        
        
            while (count > 0)
            {
                target.localScale = Vector2.Lerp(goalScale, originScale, (float)count / originCount);
            
                count -= 1;
                yield return new WaitForSeconds(fadeGap);
            }
        
            target.localScale = goalScale;
        }
        
        // 페이드 종료 후 enable
        public IEnumerator AfterEnable(GameObject target, float time, bool enable)
        {
            yield return new WaitForSeconds(time);
            
            target.SetActive(enable);
        }
    }
}


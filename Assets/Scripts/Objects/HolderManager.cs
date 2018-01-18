using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
    // 일반 변수
    public  List<Transform> holderList = new List<Transform>();    // 홀더 리스트
    public  GameObject      holderPrefab;                          // 생성될 Holder 프리팹
    

    // 초기화
    void Start()
    {
        StartCoroutine("CreateHolder");
    }

    // 랜덤하게 생성
    IEnumerator CreateHolder()
    {
        holderList.Add((Instantiate(holderPrefab, new Vector3(Random.Range(-4f, 4f), -8f), Quaternion.identity)).transform);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        StartCoroutine("CreateHolder");
    }
}

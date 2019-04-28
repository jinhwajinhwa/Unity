using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWork : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GetMoney()); //iEnumerator함수는 코루틴함수로 실행함 !! 그냥 메서드만 써주면 실행안됨
	}
	
    IEnumerator GetMoney() //iEnumerator : 초 같은 시간을 다루는 함수, Update()는 매 프레임마다 반복되기 떄문에 시간을 계산해서 뭘 하기는 부적합함!
    {

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        while (true)//게임이 끝나기 전까지 조건문을 빠져나가지 않는다
        {           
            gm.money += gm.personPrice; //직원의 단가만큼 소지금 증가
            yield return new WaitForSeconds(1); //1초마다 무한반복 하는 구조 (1초뒤에 다시 조건문 처음으로 돌아가고 돌아가고...)
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlappy : MonoBehaviour {

    //연동되는 컴포넌트를 가져온다. 변수 선언(rigidbody를 자료형으로 받아온다)
    public Rigidbody2D rb;
    public float power;
        
	// Use this for initialization
	void Start () { //한번만 실행
		
	}
	
	// Update is called once per frame
	void Update () { //매 프레임마다 반복해서 실행

        if (Input.GetMouseButtonDown(0)) //터치 입력도 똑같이 작동(0:왼쪽버튼 / 1: 오른쪽버튼 / 2:마우스휠) 주로 0을 쓴다
        {
            rb.AddForce(Vector2.up * power);
        }
    }

}

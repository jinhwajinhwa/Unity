using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    //연동되는 컴포넌트를 가져온다. 변수 선언(rigidbody를 자료형으로 받아온다)
    public Rigidbody2D rb;

        
	// Use this for initialization
	void Start () { //한번만 실행
		
	}
	
	// Update is called once per frame
	void Update () { //매 프레임마다 반복해서 실행
        if (Input.GetKey(KeyCode.LeftArrow)) //GetKey: 누르고있을때, GetKeyUp:키보드에서 손 뗄때 GetKeyDown:키보드 누르는 그 순간
        {
            rb.AddForce(Vector2.left * 10);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //이제 막 충돌했을 때(1번)
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //충돌에서 벗어났을 때(1번)
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("stay"); 
        //충돌을 계속 할 때
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 300);
        }
    }
}

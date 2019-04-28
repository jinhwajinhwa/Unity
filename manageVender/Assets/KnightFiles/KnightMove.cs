using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour {

    Animator anim;
    SpriteRenderer sr;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        //A, D키 또는 왼쪽, 오른쪽 화살표 키 (조이스틱 왼쪽 오른쪽도 가능)
        float x = Input.GetAxis("Horizontal"); //축을 입력 받음 -1 ~ 1 (왼쪽 ~ 오른쪽)
        rb.AddForce(Vector2.right * x * 5); //힘을 가하여 움직임
        //transform.Translate(Vector2.right * x * 0.05f);
        
        if(x<0) //왼쪽으로 움직일 때
        {
            sr.flipX = true; //X축기준으로 이미지가 뒤집어짐
            anim.SetBool("Move", true); //걷기 애니메이션 활성화

        }else if(x == 0){
            anim.SetBool("Move", false); //걷기 애니메이션 활성화
        }
        else //x>0일때
        {
            sr.flipX = false; //원래 그림
            anim.SetBool("Move", true); //걷기 애니메이션 활성화
        }

        //스페이스 바를 눌렀을때 공격 애니메이션
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }
		
	}
}

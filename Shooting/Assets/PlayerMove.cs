using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Transform tr;
    public float speed;

    public Vector2 limit1;
    public Vector2 limit2;

	// Use this for initialization
	void Start () {

        tr = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
        LimitCheck();
        KeyboardMove();
        MouseMove();
        LimitCheck();

	}

    void KeyboardMove()
    {
        float x = Input.GetAxis("Horizontal");  //(왼쪽)-1 ~ 1(오른쪽)
        float y = Input.GetAxis("Vertical"); //(아래)-1 ~ 1(위)
                                             //각각 가로축과 세로축의 입력값을 받아옴!

        // x = Input.GetAxisRaw("")     //-1, 0, 1만 가져옴. 좀더 딱딱한 느낌으로 가져올 수 있다 (캐릭터를 움직이는 경우)
        
        /*
        Transform과 Rigidbody의 차이점
        Transform: 일단 움직이고 충돌을 검사 (움직임을 제한하거나 Collider로 막을 경우 튕길 수 있음)
        Rigidbody: 충돌을 검사하면서 움직임 (Collider의 경계를 넘지 않음)
        ex) rb.AddForce() -> 힘을 가하는 함수
            rb.MovePosition(좌표) -> 해당 좌표로 이동       
         
         */

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //rb.AddForce(Vector2.right * x * speed);
        //rb.AddForce(Vector2.up * y * speed);

        //tr.Translate(Vector2.right * x * speed);
        //tr.Translate(Vector2.up * y * speed);

        Vector2 target = new Vector2(rb.position.x + speed * x, rb.position.y + speed * y);
        rb.MovePosition(target);
        
    }

    void MouseMove()
    {
        if (Input.GetMouseButton(0)) //마우스 누르고 있을 때
        {
            //카메라가 바라보는 좌표랑 마우스가 클릭하는 좌표는 커다란 차이가 있다. 이걸 맞춰줘야 함
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스가 클릭한 좌표를 카메라의 좌표로 변환해줌

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            //MoveTowards() -> A에서 B지점까지의 거리를 계산해서 움직이도록해줌
            //tr.position = Vector2.MoveTowards(tr.position, mousePosition, speed * 5);

            rb.MovePosition(Vector2.MoveTowards(rb.position, mousePosition, speed));

        }

    }


    void LimitCheck()
    {
        if (tr.position.x < limit1.x)
            tr.Translate(Vector2.right * speed);
        if (tr.position.x > limit2.x)
            tr.Translate(Vector2.left * speed);
        if (tr.position.y > limit1.y)
            tr.Translate(Vector2.down * speed);
        if (tr.position.y < limit2.y)
            tr.Translate(Vector2.up * speed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("ENEMY"))
            GameObject.Find("GameManager").GetComponent<GameManager>().life -= 1; //부딪혔을때 라이프 감소
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector2 limit3 = new Vector2(limit2.x, limit1.y);
        Vector2 limit4 = new Vector2(limit1.x, limit2.y);

        Gizmos.DrawLine(limit1, limit3);
        Gizmos.DrawLine(limit1, limit4);
        Gizmos.DrawLine(limit3, limit2);
        Gizmos.DrawLine(limit4, limit2);

        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour {

    private Transform tr;
    private Vector2 firstTouch;
    private Vector2 currentTouch;

    public float limitMinY; //최소로 내릴 수 있는 좌표
    public float limitMaxY; //최소로 올릴 수 있는 좌표

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))//마우스를 눌렀을때 한 순간
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        if(Input.GetMouseButton(0)) //마우스를 누르고 있을때를 계속 감지
        {
            currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition); //실시간으로 있는 곳을 계속 입력받음
        }

        if(Vector2.Distance(firstTouch, currentTouch) >0.4 ) //두개의 벡터 사이에 거리가얼마나 떨어져있는지를 계산함, 0.4는 손가락의 뭉툭함이 이정도 되겠다를 생각한 임의의 값
        {
            if(firstTouch.y < currentTouch.y) //스크롤을 위로 올리면 화면은 내려감
            {
                if (tr.position.y > limitMinY) //카메라 화면이 최소 제한값보다 클때만 움직여야 함
                    tr.Translate(Vector2.down * 0.1f); //해당방향으로 움직이는 함수. 0.05f는 속도값

            }
            else if(firstTouch.y > currentTouch.y)
            {
                if (tr.position.y < limitMaxY) //카메라 화면이 최대값보다 작을떄만 움직여야 함
                    tr.Translate(Vector2.up * 0.1f);
            }
        }
	}


    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(transform.position.x, limitMinY), 0.2f); //transform.position.x는 카메라 따라다니면서 위치 보여줌, 중요한건 y축이 어디로 내려갈것이냐임!
        //tr.position.x는 안되고 transform.position.x로 해야되는 이유?
        //tr이 Start()에 선언되어 있기 때문에 게임을 시작하기 전에 에디터 상에서는 tr이 정의되지 않아서 기즈모를 볼 수 없다
        //에디터 상에서 조절하려면 transform자체를 불러와야 한다 !
        Gizmos.DrawSphere(new Vector2(transform.position.x, limitMaxY), 0.2f);
    }

    //void OnApplicationQuit : 게임 종료하기 전에 한번 저장해야하니 그때 쓰는 메스도
    //void OnApplicationPause : 홈버튼 누르는 등 백그라운드 돌아갔을때(게임 일시정지 했을때)도 저장해서 데이터 누락을 막는다
    

}

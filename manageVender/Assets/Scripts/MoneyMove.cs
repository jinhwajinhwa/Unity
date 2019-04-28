using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //게임오브젝트찾기 ----> 해당컴포넌트 ------>변수
        Vector2 target = GameObject.Find("GameManager").GetComponent<GameManager>().targetPoint;
        long price = GameObject.Find("GameManager").GetComponent<GameManager>().price;

        //날아가기(MoveTowards함수: 속도만큼 계산해서 남은 거리를 리턴해주고 프레임마다 계속 그 거리만큼 가는 함수이기 때문에 Update에 써야 한다)
        transform.position =  Vector2.MoveTowards(transform.position, target, 0.2f); //transform.position: 자신의 현재 위치 (파라미터: 시작점, 목표점, 속도값)

        //날아갈때 투명하게 만들기
        //색 변경(투명도 감소)
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.02f); //r, g , b, a순
        
        //차일드오브젝트인 텍스트 컴포넌트의 색 변경(자식오브젝트 이름이 하나일 때)
        GetComponentInChildren<Text>().color = new Color(0, 0, 0, GetComponent<SpriteRenderer>().color.a - 0.02f); //1,1,1 화이트 , 0,0,0 블랙

        //돈 날아갈때 텍스트도 함께 오른 값으로 변경
        GetComponentInChildren<Text>().text = "+" + price.ToString("###,###");

        //이름이 똑같은 오브젝트중 특정한 자식오브젝트 찾기
        //GameObject.Find("오브젝트").transform.Find("차일드오브젝트").GetComponent<컴포넌트>()
    }
}

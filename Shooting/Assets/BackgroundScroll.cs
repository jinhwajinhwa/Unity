using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    private Renderer rend;
    public float speed;


	// Use this for initialization
	void Start () {

        rend = GetComponent<Renderer>();
        //Time.deltaTime ==> 전 프레임에서 현재 프레임까지 걸린 시간 측정
        
	}
	
	// Update is called once per frame
	void Update () {
        //현재 메테리얼의 텍스쳐 오프셋 값
        Vector2 offset = rend.sharedMaterial.mainTextureOffset;

        //오프셋 값의 y를 speed값 만큼 상승
        rend.sharedMaterial.mainTextureOffset = new Vector2(0, offset.y + speed * Time.deltaTime); //진행방향 거꾸로 하려면 -offset하면 됨 !, speed에 Time.deltaTime을 곱해주면 기기별로 다른 프레임 시간을 보완해줄 수 있다 !

        //Debug.Log("걸린시간 : " + Time.deltaTime); //프레임마다 걸리는 시간이 다름. 그래서 유니티에서는 리듬게임 만들기가 어렵다.
	}
}

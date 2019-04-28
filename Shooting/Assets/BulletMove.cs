using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    //총알, 적 비행기에 적용되어 있음 !

    public float speed;
    public bool isEnemy;


	// Use this for initialization
	void Start () {

        Destroy(this.gameObject, 10f); //생성되고나서 10초후에 제거됨
		
	}
	
	// Update is called once per frame
	void Update () {

        if(isEnemy== true)
        {
            transform.Translate(Vector2.down * speed);
        }else//아군의 총알
        {
            transform.Translate(Vector2.up * speed);
        }
        
		
	}

    private void OnTriggerEnter2D(Collider2D collision) //총알에 넣는다고 가정
    {
        if (collision.CompareTag("ENEMY")) //Enemy프리팹에 태그 추가한다 !
        {
            Destroy(collision.gameObject); //적 비행기 파괴
            Destroy(this.gameObject); //총알이 파괴됨, 이걸 안해주면 총알이 쭉 진행되면서 적 비행기를 모두 타격함
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject prefabBullet;
    public float shootSpeed;


	// Use this for initialization
	void Start () {
        StartCoroutine(AutoShoot());
	}
	
    IEnumerator AutoShoot()
    {
        while (true)
        {
            Vector2 spot = new Vector2(transform.position.x, transform.position.y + 0.3f); //아군 비행기에 넣을거기떄문에 transform.position에서 발사, 비행기 날개쪽에서 나와야 하므로 살짝 위로 해줌
            Instantiate(prefabBullet, spot, Quaternion.identity); 
            yield return new WaitForSeconds(shootSpeed); //대기
        }
    }

}

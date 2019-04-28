using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour {

    public Vector2[] spots;
    public GameObject prefabEnemy;

	// Use this for initialization
	void Start () {
        StartCoroutine(AutoCreate());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AutoCreate()
    {
        while (true)
        {
            int r = Random.Range(0, spots.Length);

            GameObject obj = Instantiate(prefabEnemy, spots[r], Quaternion.identity);
            Destroy(obj, 10f); //10초뒤에 파괴됨

            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        }
    }

    private void OnDrawGizmos()
    {
        for(int i=0; i<spots.Length; i++)
        {
            Gizmos.DrawSphere(spots[i], 0.2f);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int life;
    public GameObject[] lifeObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i < lifeObj.Length; i++)
        {
            if (i < life)
                lifeObj[i].SetActive(true);
            else
                lifeObj[i].SetActive(false);
                
        }

        if(life < 0)
        {
            //Debug.Log("Game Over!!!");
            SceneManager.LoadScene(1); //아니면 SceneManager.LoadScene("GameOver")로 해도됨
        }
	}
}

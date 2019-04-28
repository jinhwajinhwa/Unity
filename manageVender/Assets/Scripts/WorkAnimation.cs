using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkAnimation : MonoBehaviour {

    //public Sprite[] sps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false) //Ui위에 마우스가 있지 않을때 !
            {
                GetComponent<Animator>().SetTrigger("Click");
                //if (GetComponent<SpriteRenderer>().sprite == sps[0])
                //    GetComponent<SpriteRenderer>().sprite = sps[1];
                //else
                //    GetComponent<SpriteRenderer>().sprite = sps[0];
            }

        }
	}
}

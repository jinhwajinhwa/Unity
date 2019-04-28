using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (transform.childCount < 1) 
        {
            DragItem.currentSelect.transform.SetParent(this.transform); //슬롯이 자기자신을 부모로 함으로써 아이템을 자식으로 넣는것
        }
        

    }
}

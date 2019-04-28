using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // UI의 상호작용

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler { //UI에서의 드래그를 다루기 위한 것들임 !

    public static GameObject currentSelect = null;
    public static GameObject originParent = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) //드래그 시작했을때 
    {
        currentSelect = this.gameObject;
        originParent = gameObject.transform.parent.gameObject;
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform); //다른 슬롯이 아이템을 가리는걸 방지, 캔버스를 부모로 지정해서 따로 빼준다 !
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData) //드래그 하는 중 
    {
        currentSelect.transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) //드래그 끝났을 떄 
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (currentSelect.transform.parent.GetComponent<DropSlot>() == null) //dropslot 스크립트가 안들어가있는 상태
        {
            currentSelect.transform.SetParent(originParent.transform); //엉뚱한데 가면 원래 칸으로 돌아오도록 만들 수 있음
        }

        currentSelect = null;
    }

      
}

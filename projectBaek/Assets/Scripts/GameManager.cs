using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    public int customerNumber; //사람 수

    public GameObject prefabBaek; 
    public GameObject[] prefabCustomers; //고객

    public AudioClip clip; //고객 생길때 나는 소리

    //번돈 총합
    public long totalMoney;

    public Restaurant[] restaurant = new Restaurant[6];


    private void Awake()
    {
        //음식점 생성
        for (int i = 0; i < restaurant.Length; i++)
            restaurant[i] = new Restaurant();
    }
    // Use this for initialization
    void Start () {

       



	}
	
	// Update is called once per frame
	void Update () {

        NewCustomer();        
		
	}

    void NewCustomer()
    {

        if (Input.GetMouseButtonDown(0))
        {           
                //고객 생성
                CustomerCome();
        }
    }


    void CustomerCome()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false && SpotColliderCheck.isMouseOverSpot == false) //마우스가 UI위에 있지 않을때
            {
                customerNumber += 1; //고객을 1씩 증가

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //고객 오브젝트 생성
                GameObject obj;               
                int r = Random.Range(0, prefabCustomers.Length);
                obj = Instantiate(prefabCustomers[r], mousePosition, Quaternion.identity);    

                //소리 재생(효과음 한 번만 실행 (클립, 볼륨))
                GetComponent<AudioSource>().PlayOneShot(clip, 1);

            }

        }
    }



    //ServingSpeed초뒤
    void CustomerDecrease()
    {     
                customerNumber -= 1; //고객을 1씩 감소   


                //돈 올라감(ORDER에 따라..) BEST: 10000 // SOSO: 5000 //WORST: 0


                //오브젝트 삭제
                //customerSpot1.Remove()
                //Destroy(obj, 5);  
    }

  
}

public class Restaurant
{ 
    public int taste; //맛(100점)
    public int service; //서비스
    public int clean; //청결도
    public int exception; //예외
    public int servingSpeed; //서빙속도
    public long money; //번돈
    public int resCapacity; //용량
    public List<GameObject> resCustomer; //실제 고객수
    public string model;

    public Restaurant()
    {
        resCustomer = new List<GameObject>();
        resCapacity = 2;    //실제론 3명 제한
    }

}
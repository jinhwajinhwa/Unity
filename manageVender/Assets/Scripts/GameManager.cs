using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    //변수 선언: 유니티에서 직접 입력 or 드래그앤드롭을 통해 가져올 수 있음

    public long money; //소지금(cf. int:21억까지 표현, 게임 오래한 사람들은 돈을 더 벌수도.. 그래서 long을 쓴다)
    public int person; //직원수
    public int priceLevel; //단가 업그레이드 진행
    public long price; //클릭당 단가
    public long priceCost; //단가 업그레이드 가격

    public int personPrice; //직원이 버는 단가
    public long personCost; //직원 채용 가격

    public Text textMoney; //소지금 표시 텍스트
    public Text textPerson; //직원 수 표시 텍스트
    public Text textUpgradePrice; //단가 업그레이드 정보
    public Text textRecruit; //직원 고용 업그레이드 정보

    public GameObject prefabMoneyGet; //돈 상승 프리팹
    public GameObject prefabPerson; //직원 프리팹
    public GameObject prefabFloor; //바닥 프리팹

    public Vector2 targetPoint; //돈 상승 프리팹의 목적지
    public Vector2[] createSpots; //직원 생성 지점

    public int personOrigin = -1; //세이브랑 직원 수랑 다른 경우를 위함

    public AudioClip clip; //드래그앤드롭으로 오디오 클립을 스크립트로 가져올 수 있음

    Vector2 camPrev; //카메라가 직전을 찍었던 위치
    Vector2 camcurr; //카메라의 현재 위치

    public int floorNumber; //바닥 개수
    public float floorSpacing = 12.48f; //바닥 스페이싱

	// Use this for initialization
	void Start () {

        //세이브 파일이 있는지 검사
        if(XmlManager.IsExist(Application.persistentDataPath + "/save.xml"))
        {
            Load();
            LoadPerson();
        }        
		
	}

	
	// Update is called once per frame
	void Update () {
        ShowInfo();
        MoneyIncrease();
        CreateFloor();

        if (Input.GetKeyDown(KeyCode.S))
            Save();

        if (Input.GetKeyDown(KeyCode.Escape)) //안드로이드에서 뒤로가기에 해당
            Application.Quit();


        if (Input.GetKeyDown(KeyCode.D))
        {
            //세이브 파일 삭제
            //세이브 파일 위치: C:\Users\jinhw\AppData\LocalLow\JH Company\외주회사 키우기 --> 여기 save.xml이 생긴다
            //지금은 종료할떄 저장하게끔 해놨기 떄문에 여기다가 해도 소용이 없음!
            System.IO.File.Delete(Application.persistentDataPath + "/save.xml");

        }


    }

    //텍스트로 정보를 표시하는 것을 담당
    void ShowInfo()
    {
        if (money == 0)
            textMoney.text = "0원";
        else
            textMoney.text = money.ToString("###,###") + "원"; //3자리씩 쉼표로 끊어읽음        

        textPerson.text = person + "명";


        CheckUpgradePrice();
        CheckUpgradeRecruit();
    }


    //단가 업그레이드 텍스트 수정 및 버튼 활성화 /비활성화
    void CheckUpgradePrice()
    {
        if (GameObject.Find("Panel_PriceUpgrade") != null) //GameObject.Find()는 비활성화된 오브젝트를 검색할 수 없다. 그래서 !=null로 찾는다. 
        {
            textUpgradePrice.text = "Lv." + priceLevel + "단가 상승\n"
                                    + "클릭당 단가: \n"
                                    + price.ToString("###,###") + "원\n" +
                                    "업그레이드 가격: \n" +
                                    priceCost.ToString("###,###") + "원\n";

            if (money < priceCost) //소지금이 업그레이드 가격보다 작다면
            {
                GameObject.Find("Panel_PriceUpgrade").transform.Find("Button_Upgrade").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("Panel_PriceUpgrade").transform.Find("Button_Upgrade").GetComponent<Button>().interactable = true;
            }
        }
    }

    //직원 고용 텍스트 수정 및 버튼 활성화 / 비활성화
    void CheckUpgradeRecruit()
    {
        if (GameObject.Find("Panel_Recruit") != null) //GameObject.Find()는 비활성화된 오브젝트를 검색할 수 없다. 그래서 !=null로 찾는다. 
        {
            if (personPrice == 0)
            {
                textRecruit.text = "Lv." + person + "직원 고용\n"
                                    + "초당 단가: \n"
                                    + "0원\n" +
                                    "업그레이드 가격: \n" +
                                    personCost.ToString("###,###") + "원\n";
            }
            else
            {
                textRecruit.text = "Lv." + person + "직원 고용\n"
                                    + "초당 단가: \n"
                                    + personPrice.ToString("###,###") + "원\n" +
                                    "업그레이드 가격: \n" +
                                    personCost.ToString("###,###") + "원\n";
            }
            

            if (money < personCost) //소지금이 업그레이드 가격보다 작다면
            {
                GameObject.Find("Panel_Recruit").transform.Find("Button_Upgrade").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("Panel_Recruit").transform.Find("Button_Upgrade").GetComponent<Button>().interactable = true;
            }
        }
    }

    void MoneyIncrease()
    {
        camcurr = Camera.main.transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false) //마우스가 UI위에 있지 않을때
            {
                if(camcurr == camPrev)
                {
                    money += price; //소지금을 현재 단가만큼 증가

                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //Vector:x, y, z를 가지는 변수타입
                    //Input.mousePosition: 기기상에서 입력된 좌표를 날 것으로 가져옴
                    //오른쪽 구석을 클릭한다고 가정했을 때 기기상의 좌표는 너무 큼 --> 월드스크린 좌표로 변환 필요
                    // Camera.main.ScreenToWorldPoint : 기기상의 좌표를 월드스크린의 좌표로 변환함

                    //돈 상승 프리팹 오브젝트 생성
                    GameObject obj;
                    obj = Instantiate(prefabMoneyGet, mousePosition, Quaternion.identity); //프리팹 복사 (파라미터: 프리팹, 마우스지점, 각도), Quaternion.identity: 현재 각도 그대로..
                    Destroy(obj, 5); //(파라미터: 제거할대상, 몇초뒤제거)

                    //소리 재생(효과음 한 번만 실행 (클립, 볼륨))
                    GetComponent<AudioSource>().PlayOneShot(clip, 1); //배경음악을 돌리는 도중 효과음이 나오게 하려면 play아니고 playoneshot을 쓴다 (play는 주로 배경음악에 쓰임), 0:0%, 1:100%
                }
                

                camPrev = camcurr;
            }
            
        }
    }

    //단가 업그레이드 패널 열기, 앞에 public을 선언한 이유는 이 함수를 외부에서도 쓸 수 있도록 만들기 위함. 
    //public으로 선언하면 인스펙터에서 함수로 넣을 수 있다. 
    public void OpenPanel(GameObject obj)
    {
        //SetActive: 게임오브젝트 파라미터로 받아오면 쓸 수 있는 메서드
        obj.SetActive(true);
    }

    //단가 업그레이드 패널닫기
    public void ClosePanel(GameObject obj)
    {
        //SetActive: 게임오브젝트 파라미터로 받아오면 쓸 수 있는 메서드
        obj.SetActive(false);
    }

    //단가 업그레이드 진행
    public void UpgradePrice()
    {
        if(money >= priceCost) //가진 돈이 업그레이드 가격보다 같거나 클 때 
        {
            money -= priceCost; //소지금 차감
            //price += price / 10 * 12; //1.2배정도 높아짐
            price += 100 * priceLevel; //단가 업그레이드 진행, 레벨값만큼 곱한 값을 더해준다. (레벨값은 계속 높아질거기 때문에)
            priceCost += 500 * priceLevel; //업그레이드 가격 올림
            priceLevel += 1; //레벨 상승
        }
    }

    //직원 구매
    public void CheckRecruit()
    {
        if(money >= personCost)
        {
            money -= personCost; //소지금 차감
            personPrice += 10 * person;
            personCost += 700 * person;

            Recruit();

            person += 1;

        }
    }

    //직원 정렬 및 생성
    void Recruit()
    {
        int row = person % 3;
                
        Vector2 spot = new Vector2( createSpots[row].x ,
                                        createSpots[row].y - (person / 3) * 3.88f ); //Vector2: x, y축 값 새로 지정한 spot
        //3.88은 내리고싶은 Y축의 좌표

        GameObject obj = Instantiate(prefabPerson, spot, Quaternion.identity);
    }

    //바닥 생성
    void CreateFloor()
    {
        int capacity = person / 9 + 1; //직원 수보다 한발 먼저 플로워가 생겨야 하므로 +1 해준다.

        if(capacity > floorNumber)
        {
            Vector2 spot = GameObject.Find("Background").transform.position;

           // if (floorNumber == 0)
           // {
           //   spot += Vector2.down * 15.79f; 

           //  }

            spot += Vector2.down * floorSpacing * floorNumber; //Vector2.down 은 아래쪽으로 값을 곱한다는 것... x, y축에 모두 곱해지지만 x는 0이기때문에 안곱해짐
            Instantiate(prefabFloor, spot, Quaternion.identity);
            Camera.main.GetComponent<CameraDrag>().limitMinY -= floorSpacing;
            floorNumber += 1;
        
        }


    }

    //OnDrawGizmos(): 자동으로 씬 뷰에 기즈모를 그림
    private void OnDrawGizmos() //Gizmos: 아이콘  ex) 씬 뷰의 카메라 아이콘, 조명 아이콘, 볼륨 아이콘
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPoint, 0.2f); // 해당 포인트에 구를 그림, 두번째파라미터: 반지름 (소수점을 쓸때 뒤에 f를 쓴다)

        Gizmos.color = Color.blue;
        for(int i = 0; i < createSpots.Length; i++)
        {
            Gizmos.DrawSphere(createSpots[i], 0.2f);
        }
    }

    //강사님의 XmlManager.cs파일 활용...!
    void Save()
    {
        SaveData sd = new SaveData();
        sd.money = money;
        sd.person = person;
        sd.personCost = personCost;
        sd.personPrice = personPrice;
        sd.price = price;
        sd.priceCost = priceCost;
        sd.priceLevel = priceLevel;

        //저장
        XmlManager.XMLSerialize<SaveData>(sd, Application.persistentDataPath+"/save.xml"); //Application.persistentDataPath: 유저이름폴더> appdata> local.. 어쩌구 라는 절대경로
    }

    void Load()
    {
        //로드
        SaveData sd = new SaveData();
        sd = XmlManager.XMLDeserialize<SaveData>(Application.persistentDataPath + "/save.xml");

        //원래 변수로 분배
        money = sd.money;
        person = sd.person;
        personCost = sd.personCost;
        personPrice = sd.personPrice;
        price = sd.price;
        priceCost = sd.priceCost;
        priceLevel = sd.priceLevel;

    }

    void LoadPerson()
    {
        for(int i=personOrigin; i<person; i++)
        {
            Recruit2();
            personOrigin += 1;
        }
    }

    void Recruit2()
    {
        int row = personOrigin % 3;

        Vector2 spot = new Vector2(createSpots[row].x,
                                        createSpots[row].y - (personOrigin / 3) * 3.88f); //Vector2: x, y축 값 새로 지정한 spot
        //3.88은 내리고싶은 Y축의 좌표

        GameObject obj = Instantiate(prefabPerson, spot, Quaternion.identity);
    }

    //게임이 꺼질 때 자동으로 실행되는 함수
    private void OnApplicationQuit()
    {
        Save();
        
    }

    //게임이 일시정지 되었을때 : 홈버튼을 눌렀을 때 등..
    private void OnApplicationPause(bool pause)
    {
        //Save();
    }

    //인앱 결제 완료 후 보상 지급 함수
    public void Purchase()
    {
        money += 200000;
    }



}

[System.Serializable] //정렬
public class SaveData
{

    //저장해야할 변수들
    public long money; //소지금(cf. int:21억까지 표현, 게임 오래한 사람들은 돈을 더 벌수도.. 그래서 long을 쓴다)
    public int person; //직원수
    public int priceLevel; //단가 업그레이드 진행
    public long price; //클릭당 단가
    public long priceCost; //단가 업그레이드 가격

    public int personPrice; //직원이 버는 단가
    public long personCost; //직원 채용 가격
}

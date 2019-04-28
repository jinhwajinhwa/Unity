using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬을 관리

public class TitleManager : MonoBehaviour {

    public bool isEnable;
    public string sentence;

    // Use this for initialization
    void Start () {

        StartCoroutine(Timer()); //iEnumerator함수는 이렇게 실행해야 함
        SimpleSave();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            // SceneManager.LoadScene("SampleScene"); //해당 씬으로 넘어감(이름으로 씬 로드)

            if(isEnable == true)
            {
                SceneManager.LoadScene(1); //번호로 씬 로드 (File-Build Setting에서 씬 번호 설정, 번호순대로 로드하기떄문에 처음에 0번을 로드함)
            }
            
            //참고: 스테이지에서 다음 씬으로 넘어가기 (다음 스테이지 넘어갈때 씬에서 변수들이 초기화되는것을 받음)             
            //int nextSceneNumber = SceneManager.GetActiveScene().buildIndex + 1; //buildIndex: 현재 활성화되어있는 씬에 인덱스를 받아옴 
            //SceneManager.LoadScene(nextSceneNumber);

        }

        if (Input.GetKeyDown(KeyCode.S))
            SimpleSave();

        if (Input.GetKeyDown(KeyCode.L))
        {
            SimpleLoad();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //안드로이드에서 뒤로가기에 해당
            Application.Quit();


    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2); //괄호 안에 있는 초 만큼 대기함
        isEnable = true;

    }

    void SimpleSave()
    {
        //Set = 저장
        //PlayerPrefs.SetInt("NUMBER", 5);

        //Get = 로드
        //int load = PlayerPrefs.GetInt("NUMBER", 0); //두번쨰 파라미터는 defaultValue이다. "NUMBER"가 없으면 0을 불러옴
        //Debug.Log(load); //prinf와동일, ()내용을 콘솔에 찍음 

        PlayerPrefs.SetString("SENTENCE", sentence);


        //*long형을 저장하는 방법
        //long money = 123123123123123123;
        //PlayerPrefs.SetString("LONG", money.ToString());

    }

    void SimpleLoad()
    {
        sentence = PlayerPrefs.GetString("SENTENCE", "");

        //*long형을 다시 로드하는 방법
        //string moneyStr = PlayerPrefs.GetString("LONG");
        //long money = long.Parse(moneyStr);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

    //ad는 Window-General-Service 창 켜서 ad 활성화 하고 해야함!

    string id;


	// Use this for initialization
	void Start () {

        //id는 unity Dashboard에서 Monetization - Platforms 에서 확인한다 !
        id = "2915232";
        //Advertisement.Initialize(id);
		
	}
	
	// Update is called once per frame
	public void ShowRewardedVideo () {

        Advertisement.Initialize(id);

        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult; //광고 재생이 끝나고 실행될 함수

        Advertisement.Show("rewardedVideo", options); //광고 재생
		
	}

    void HandleShowResult(ShowResult result)
    {
        if(result == ShowResult.Finished) //광고 재생이 스킵 없이 완료되었을 때
        {
            GetComponent<GameManager>().money += 100000;
        }

        if(result == ShowResult.Skipped) //스킵되었을때
        {
            //알림같은걸 넣으면 좋다
        }
        if(result == ShowResult.Failed) //네트워크 등의 이유로 광고 재생이 실패하였을 때
        {

        }
    }

}

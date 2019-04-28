using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

    string id;

	// Use this for initialization
	void Start ()
    {
        id = "2928593"; //Service-Ads에서 go to dashboard가서 Monetization- Platform에서 아이디 쓰면 됨
        //Monetization-Placement에서 add to placement눌러서 새로 만든다 (banner라고 썼으므로 스펠링 똑같이 banner라고 만들어야 함 !)

        Advertisement.Initialize(id); //광고초기화
        StartCoroutine(ShowBanner());
	}
	
    IEnumerator ShowBanner()
    {
        while (!Advertisement.IsReady("banner")) //준비가 안되었을 때
        {
            yield return new WaitForSeconds(0.5f);
        }

        //준비가 완료되었을 때
        Advertisement.Banner.Show("banner");


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine; //namespace 
using UnityEngine.UI; //UI영역은 따로 추가해줘야 한다!

public class MoneyManager : MonoBehaviour {

    public int money;
    public Text text;

    void Update()
    {
        text.fontSize = 60;
        text.text = "돈 : " + money;
    }

    public void MoneyIncrease()
    {
        money += 1;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMove : MonoBehaviour
{
    public Vector2[] points; //도는 스팟
    public Vector2[] creatingPoints; //고객 생성후 모이는 지점

    public Vector2 nextTarget;
    public float speed;

    private bool isReverse;

    //음식점별 고객서있는 지점
    public Vector2[] res1Spot;
    public Vector2[] res2Spot;
    public Vector2[] res3Spot;
    public Vector2[] res4Spot;
    public Vector2[] res5Spot;
    public Vector2[] res6Spot;

    //말풍선 프리팹
    public GameObject ballon;

    //주문 멘트 칭찬
    public string[] tasteBest;
    public string[] serviceBest;
    public string[] cleanBest;

    //주문 멘트 보통
    public string[] tasteSoso;
    public string[] serviceSoso;
    public string[] cleanSoso;

    //주문 멘트 최악
    public string[] tasteWorst;
    public string[] serviceWorst;
    public string[] cleanWorst;


    // Use this for initialization
    void Start ()
    {
        nextTarget = CheckFirstPoint();//points[Random.Range(0, points.Length)];
        if (Random.Range(0, 2) == 0)
            isReverse = true;

        StartCoroutine(CustomerCome());
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        CheckPoint();
        //CustomerCome();
        CustomerOrder();
	}

    void CheckPoint()
    {
        if(Vector2.Distance(transform.position, nextTarget) <= 0.1f)
        {
            if (!isReverse)
                nextTarget = ChangePoint(nextTarget);
            else
                nextTarget = ChangePointReverse(nextTarget);
        }
    }

    Vector2 CheckFirstPoint()
    {
        Vector2 nearest = creatingPoints[0];
        foreach(var a in creatingPoints)
        {
            if (Vector2.Distance(transform.position, a) < Vector2.Distance(transform.position, nearest))
                nearest = a;
        }
        return nearest;
    }

    Vector2 ChangePoint(Vector2 v)
    {
        for(int i = 0; i < points.Length; i++)
        {
            if(v == points[i])
            {
                if(i == points.Length - 1)
                {
                    return points[0];                    
                }
                else
                {
                    return points[i + 1];
                }
            }
        }
        return points[0];
    }
    Vector2 ChangePointReverse(Vector2 v)
    {
        for (int i = points.Length - 1; i >= 0; i--)
        {
            if (v == points[i])
            {
                if (i == 0)
                {
                    return points[points.Length - 1];
                }
                else
                {
                    return points[i - 1];
                }
            }
        }
        return points[points.Length - 1];
    }


    //만약 각각에 스팟에 3명이 다 차있으면 돌아다니면서 대기함
    IEnumerator CustomerCome()
    {
            var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            int r = Random.Range(0, gm.restaurant.Length);
            while (true)
            {
                r = Random.Range(0, gm.restaurant.Length);
                Debug.Log(r);
                try
                {
                    List<GameObject> resCustomer = gm.restaurant[r].resCustomer;
                    int resCapacity = gm.restaurant[r].resCapacity;

                    if (resCustomer.Count < resCapacity)
                        break;
                }
                catch { }
                transform.position = Vector2.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);
                yield return null;
            }

        while (true)
        {
            Vector2 target = new Vector2();
            try
            {
               
                switch (r)
                {
                    case 0:
                        transform.position = Vector2.MoveTowards(transform.position, res1Spot[gm.restaurant[r].resCustomer.Count], speed * Time.deltaTime);
                        target = res1Spot[gm.restaurant[r].resCustomer.Count];
                        break;
                    case 1:
                        transform.position = Vector2.MoveTowards(transform.position, res2Spot[gm.restaurant[r].resCustomer.Count], speed * Time.deltaTime);
                        target = res2Spot[gm.restaurant[r].resCustomer.Count];
                        break;
                    case 2:
                        transform.position = Vector2.MoveTowards(transform.position, res3Spot[gm.restaurant[r].resCustomer.Count], speed * Time.deltaTime);
                        target = res3Spot[gm.restaurant[r].resCustomer.Count];
                        break;
                    case 3:
                        transform.position = Vector2.MoveTowards(transform.position, res4Spot[gm.restaurant[r].resCustomer.Count], speed * Time.deltaTime);
                        target = res4Spot[gm.restaurant[r].resCustomer.Count];
                        break;
                    case 4:
                        transform.position = Vector2.MoveTowards(transform.position, res5Spot[gm.restaurant[r].resCustomer.Count], speed * Time.deltaTime);
                        target = res5Spot[gm.restaurant[r].resCustomer.Count];
                        break;
                    case 5:
                        transform.position = Vector2.MoveTowards(transform.position, res6Spot[gm.restaurant[r].resCustomer.Count], speed * Time.deltaTime);
                        target = res6Spot[gm.restaurant[r].resCustomer.Count];
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                transform.position = Vector2.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);
            }

            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                if(gm.restaurant[r].resCustomer.Count <= gm.restaurant[r].resCapacity + 1)
                    gm.restaurant[r].resCustomer.Add(this.gameObject);
                yield return new WaitForSeconds(3.0f);  //3초 대기
                                                        //주문하거나 대사
                break;
            }

            yield return null;
        }
    }


    void CustomerOrder()
    {

        //랜덤으로 맛, 서비스, 청결 멘트 던짐
        //음식점의 맛, 서비스, 청결 수치(맛/맛+서비스+청결+익셉션)로 BEST, SOSO, WORST 던짐 
        int r = Random.Range(0, tasteBest.Length);

        //프리팹 상태 바뀜?


    }


    private void OnDrawGizmos()
    {
        foreach(var a in points)
        {
            Gizmos.DrawSphere(a, 0.3f);
        }
        foreach(var a in creatingPoints)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(a, 0.3f);
        }
        foreach (var a in res1Spot)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(a, 0.2f);
        }
        foreach (var a in res2Spot)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(a, 0.2f);
        }
        foreach (var a in res3Spot)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(a, 0.2f);
        }
        foreach (var a in res4Spot)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(a, 0.2f);
        }
        foreach (var a in res5Spot)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(a, 0.2f);
        }
        foreach (var a in res6Spot)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(a, 0.2f);
        }
    }
}

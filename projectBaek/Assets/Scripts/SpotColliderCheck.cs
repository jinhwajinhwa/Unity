using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotColliderCheck : MonoBehaviour
{
    public static bool isMouseOverSpot;

    private void OnMouseOver()
    {
        isMouseOverSpot = true;
    }
    private void OnMouseExit()
    {
        isMouseOverSpot = false;
    }

}

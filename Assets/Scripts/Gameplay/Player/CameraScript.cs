using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool isLookingRight;
    public bool isLookingLeft;
    public bool isLookingFront;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "CannonPointA":
                isLookingFront = true;
                isLookingLeft = false;
                isLookingRight = false;
                Debug.Log("Mirando a adelante");
                break;
            case "CannonPointB":
                isLookingFront = false;
                isLookingLeft = false;
                isLookingRight = true;
                Debug.Log("Mirando a la Derecha");
                break;
            case "CannonPointC":
                isLookingFront = false;
                isLookingLeft = true;
                isLookingRight = false;
                Debug.Log("Mirando a la Izquierda");
                break;
            default:
                isLookingFront = false;
                isLookingLeft = false;
                isLookingRight = false;
                break;
        }
    }
}

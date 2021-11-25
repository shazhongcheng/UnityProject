using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVCam : MonoBehaviour
{

   
    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            GameController._instance.SeePlayer(other.transform);
        }
    }
}

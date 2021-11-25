using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUnit : MonoBehaviour
{
    public GameObject laser;
    public Material unlockMat;
    public GameObject screen;


    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                laser.SetActive(false);
                GetComponent<AudioSource>().Play();
                screen.GetComponent<Renderer>().material = unlockMat;
            }
        }
    }
}

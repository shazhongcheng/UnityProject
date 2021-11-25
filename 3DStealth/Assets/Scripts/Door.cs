using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool reqireKey = false;
    public AudioSource musicDenied;

    public int count = 0;
    private Animator anim;

    private AudioSource audio;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Colse", count <= 0);

        if (anim.IsInTransition(0))
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (reqireKey)
        {
            if (other.tag == Tags.player)
            {
                Player player = other.GetComponent<Player>();
                if (player.hasKey)
                {
                    count++;
                }
                else
                {
                    musicDenied.Play();
                }
            }
        }
        else
        {
            if (other.tag == Tags.player)
            {
                count++;
            }
          
            else if (other.tag == Tags.enemy && other.isTrigger == false)
            {
                
                count++;
            }
            print(other.isTrigger);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (reqireKey)
        {
            if (other.tag == Tags.player)
            {
                Player player = other.GetComponent<Player>();
                if (player.hasKey)
                {
                    count--;
                }
            }
        }
        else
        {
            if (other.tag == Tags.player)
            {
                count--;
            }
          
            else if (other.tag == Tags.enemy && other.isTrigger == false)
            {
                
                count--;
            }
            print(other.isTrigger);
        }
    }

}

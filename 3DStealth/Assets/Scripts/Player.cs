using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 3;
    public float stopSpeed = 20;
    public float rotateSpeed = 0;
    public bool hasKey = false;

    private Animator anim;


    private AudioSource audio;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Sneak", true);
        }
        else
        {
            anim.SetBool("Sneak", false);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
        {
            float newSpeed = Mathf.Lerp(anim.GetFloat("Speed"), 5.6f, moveSpeed * Time.deltaTime);
            anim.SetFloat("Speed", newSpeed);

            Vector3 targetDir = new Vector3(h, 0, v);

            Quaternion newRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            PlayFootMusic();
        }
        else
        {
            StopFootMusic();
        }

    }


    private void PlayFootMusic()
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
    private void StopFootMusic()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }

}

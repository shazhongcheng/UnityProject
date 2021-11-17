using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 5;
    public float angularSpeed = 30;

    public float number = 1;

    private Rigidbody rigidbody;

    public AudioClip idleAudio;
    public AudioClip drivingAudio;

    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("VerticalPlayer" + number);
        rigidbody.velocity = transform.forward * v * speed;

        float h = Input.GetAxis("HorizontalPlayer" + number);
        rigidbody.angularVelocity = transform.up * h * angularSpeed;

        if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
        {
            audio.clip = drivingAudio;
            if (audio.isPlaying == false)
                audio.Play();
        }
        else
        {
            audio.clip = idleAudio;
            if (audio.isPlaying == false)
                audio.Play();
        }

    }
}

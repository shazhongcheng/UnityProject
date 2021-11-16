using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControlller : MonoBehaviour
{

    public float speed = 10;
    public KeyCode upKey;

    private Rigidbody2D rigidbody2D;


    private Animator animator;
    public bool IsStopAll = false;

    public AudioClip wingSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upKey) && (!IsStopAll))
        {
            SoundManager.instance.PlaySingle(wingSound);

            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.velocity = new Vector2(0, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.instance.StopAll();
        IsStopAll = true;
        animator.enabled = false;
    }

}

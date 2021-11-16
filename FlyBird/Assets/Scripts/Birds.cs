using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;

    private Animator animator;

    public float speed = 10;
    public KeyCode upKey;

    public bool IsStopAll = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(upKey) && (!IsStopAll))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.velocity = new Vector2(0, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.StopAll();
        IsStopAll = true;
        animator.enabled = false;
    }

}

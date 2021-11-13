using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public KeyCode upKey;
    public KeyCode downKey;
    public float speed = 10;

    private Rigidbody2D rigidbody2D;

    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            rigidbody2D.velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(downKey))
        {
            rigidbody2D.velocity = new Vector2(0, -speed);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, 0);
        }
    }

    //void OnCollisionEnter2D()
    //{
    //    SoundManager.instance.PlaySingle(ClickSound);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;

    public AudioClip HitSound;
    public AudioClip ClickSound;


    private bool CanA;

    private bool CanB;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        GoBall();
    }


    void GoBall()
    {
        int number = Random.Range(0, 2);
        rigidbody2D.velocity = Vector2.zero;
        if (number == 1)
        {
            rigidbody2D.AddForce(new Vector2(100, 0));
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(-100, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidbody2D.velocity;
        if (velocity.x < 9 && velocity.x > -9 && velocity.x != 0)
        {
            if (velocity.x > 0)
            {
                velocity.x = 10;
            }
            else
            {
                velocity.x = -10;
            }
            rigidbody2D.velocity = velocity;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            Vector2 velocity = rigidbody2D.velocity;
            velocity.y = velocity.y / 2f + col.rigidbody.velocity.y / 2;
            rigidbody2D.velocity = velocity;
            SoundManager.instance.PlaySingle(ClickSound);
        }
        if ((col.gameObject.name == "RightWall" && CanA) || (col.gameObject.name == "LeftWall" && CanB))
        {
            GameManager.Instance.ChangeScore(col.gameObject.name);           
            CanA = false;
            CanB = false;
        }
        if (col.gameObject.name.Contains("Wall") )
        {
            SoundManager.instance.PlaySingle(HitSound);
        }

       
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        GoBall();
        //Debug.Log("Yes");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "right")
        {
            CanA = true;
        }
        else if (collision.tag == "left")
        {
            CanB = true;
        }
    }

}

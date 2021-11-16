using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -20f)
        {
            Destroy(this.gameObject);
        }
    }


    public void StopMove()
    {
        rigidbody2D.velocity = Vector2.zero;
    }

}

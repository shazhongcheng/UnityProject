using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    public float minDamage = 30;

    private Animator anim;
    private bool haveShoot = false;
    private PlayerHp health;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("Shot") > 0.5)
        {
            Shooting();
        }
        else
        {
            haveShoot = false;
        }
    }


    private void Shooting()
    {
        if (haveShoot == false)
        {
            // º∆À„ …À∫¶
            float damage = minDamage + 90 - 9 * (transform.position - health.transform.position).magnitude;
            health.TakeDamage(damage);

            haveShoot = true;
        }

    }
}

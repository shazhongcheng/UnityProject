using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{

    public float hp = 100;
    private Animator anim;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            anim.SetBool("Dead", true);
            StartCoroutine(ReloadScene());
        }
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(4f);
        Application.LoadLevel(0);
    }

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

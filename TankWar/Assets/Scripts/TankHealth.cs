using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    public int hp = 100;
    public GameObject tankExplosion;

    public AudioClip tankExplosionAudio;

    private int hpTotal;

    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hpTotal = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void TakeDamage()
    {
        if (hpTotal <= 0) return;
        hpTotal -= Random.Range(10, 20);
        hpSlider.value = (float)hpTotal / hp;
        if (hpTotal <= 0)
        {//收到伤害之后 血量为0 控制死亡效果
            AudioSource.PlayClipAtPoint(tankExplosionAudio, transform.position);
            GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, transform.rotation);
          
            GameObject.Destroy(this.gameObject);         

        }
    }


 

}

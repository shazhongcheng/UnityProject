using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public Slider healthSlider;

    public bool destroyOnDeath = false;


    private NetworkStartPosition[] spawnPoints;

    public void TakeDamage(int damage)
    {

        if (isServer == false) return;// 血量的处理只在服务器端执行

        currentHealth -= damage;
        if (currentHealth <= 0)
        {

            if (destroyOnDeath)
            {
                Destroy(this.gameObject); return;
            }

            currentHealth = maxHealth;

            Debug.Log("Dead");
            RpcRespawn();
        }

    }

    void OnChangeHealth(int oldhealth,int newhealth)
    {
        healthSlider.value = newhealth / (float)maxHealth;
    }



    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer == false) return;

        Vector3 spawnPosition = Vector3.zero;


        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        transform.position = spawnPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

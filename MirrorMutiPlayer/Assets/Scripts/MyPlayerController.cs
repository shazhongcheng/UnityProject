using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyPlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(isLocalPlayer);

        if (GetComponent<NetworkIdentity>().isLocalPlayer == false)
        {
            return;
        }
       
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Debug.Log("Hello");

        transform.Rotate(Vector3.up * h * 120 * Time.deltaTime);
        transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

    }

    public override void OnStartLocalPlayer()
    {
        //�������ֻ���ڱ��ؽ�ɫ�������  ����ɫ��������ʱ��
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command]
    void CmdFire()//���������Ҫ��server�������
    {
        // �ӵ������� ��Ҫserver��ɣ�Ȼ����ӵ�ͬ��������client
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
        Destroy(bullet, 2);

        NetworkServer.Spawn(bullet);
    }

}

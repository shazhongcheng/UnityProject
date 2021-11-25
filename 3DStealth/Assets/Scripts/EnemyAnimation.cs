using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{

    public float speedDampTime = 0.3f;
    public float anglarSpeedDampTime = 0.3f;

    private NavMeshAgent navAgent;

    private Animator anim;

    private EnemySight sight;

    private void Awake()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();

        sight = this.GetComponent<EnemySight>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.desiredVelocity == Vector3.zero)
        {
            anim.SetFloat("Speed", 0, speedDampTime, Time.deltaTime);
            anim.SetFloat("AnglarSpeed", 0, anglarSpeedDampTime, Time.deltaTime);

        }
        else
        {

            float angle = Vector3.Angle(transform.forward, navAgent.desiredVelocity);
            float angleRad = 0f;
            if (angle > 90)
            {
                anim.SetFloat("Speed", 0, speedDampTime, Time.deltaTime);
            }
            else
            {
                Vector3 projection = Vector3.Project(navAgent.desiredVelocity, transform.forward);
                anim.SetFloat("Speed", projection.magnitude, speedDampTime, Time.deltaTime);
            }
            angleRad = angle * Mathf.Deg2Rad;

            Vector3 crossRes = Vector3.Cross(transform.forward, navAgent.desiredVelocity);
            if (crossRes.y < 0)
            {
                angleRad = -angleRad;
            }

            anim.SetFloat("AnglarSpeed", angleRad, anglarSpeedDampTime, Time.deltaTime);

        }

        anim.SetBool("PlayerInSight", sight.playerInSight);
    }
}

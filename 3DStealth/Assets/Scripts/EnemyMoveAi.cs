using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAi : MonoBehaviour
{
    public Transform[] wayPoints;

    public float patrolTime = 3f;
    private float patrolTimer = 0;

    public float chaseTime = 3f;
    private float chaseTimer = 0;


    private NavMeshAgent navAgent;
    [SerializeField]
    private int index = 0;

    private EnemySight sight;

    private PlayerHp playerHp;

    private void Awake()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
        navAgent.destination = wayPoints[index].position;
        //navAgent.updatePosition = false;
        //navAgent.updateRotation = false;
        sight = this.GetComponent<EnemySight>();

        playerHp= GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHp>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerHp.hp);
        if (sight.playerInSight&& playerHp.hp>0)
        {
            //shoot
            Shooting();
        }
        else if (sight.alertPosition != Vector3.zero && playerHp.hp > 0)
        {
            //chase
            navAgent.isStopped = false;
            Chasing();
        }
        else
        {
            navAgent.isStopped = false;
            Patrolling();
        }

    }

    private void Patrolling()
    {
        navAgent.speed = 3;
        navAgent.destination = wayPoints[index].position;
        //navAgent.updatePosition = false;
        //navAgent.updateRotation = false;

        //Debug.Log(navAgent.remainingDistance);

        if (navAgent.remainingDistance < 0.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer > patrolTime)
            {
                index++;
                index %= 4;
                navAgent.destination = wayPoints[index].position;
                //navAgent.updatePosition = false;
                //navAgent.updateRotation = false;
                patrolTimer = 0;
            }
        }
    }

    private void Chasing()
    {
        navAgent.speed = 6;
        navAgent.destination = sight.alertPosition;
        //navAgent.updatePosition = false;
        //navAgent.updateRotation = false;

        if (navAgent.remainingDistance < 2f)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer > chaseTime)
            {
                sight.alertPosition = Vector3.zero;
                GameController._instance.lastPlayerPostion = Vector3.zero;
                GameController._instance.alermOn = false;
            }
        }
    }

    private void Shooting()
    {
        navAgent.isStopped=true;
    }

}

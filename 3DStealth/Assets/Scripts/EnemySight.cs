using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{

    public bool playerInSight = false;
    public float fieldOfView = 110;
    public Vector3 alertPosition = Vector3.zero;


    private SphereCollider collider;
    private Animator playerAnim;
    private NavMeshAgent navAgent;


    private Vector3 preLastPlayerPosition = Vector3.zero;

    private void Awake()
    {
        playerAnim = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Animator>();
        navAgent = this.GetComponent<NavMeshAgent>();
        collider = this.GetComponent<SphereCollider>();

    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            Vector3 forward = transform.forward;
            Vector3 playerDir = other.transform.position - transform.position;
            float temp = Vector3.Angle(forward, playerDir);
            RaycastHit hitInfo;
            bool res = Physics.Raycast(transform.position + Vector3.up, other.transform.position - transform.position, out hitInfo);
            if (temp < 0.5f * fieldOfView && (res == false || hitInfo.collider.tag == Tags.player))
            {
                playerInSight = true;
                alertPosition = other.transform.position;
                GameController._instance.SeePlayer(other.transform);
            }
            else
            {
                playerInSight = false;
            }

            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
            {
                NavMeshPath path = new NavMeshPath();
                if (navAgent.CalculatePath(other.transform.position, path))
                {
                    Vector3[] wayPoints = new Vector3[path.corners.Length + 2];
                    wayPoints[0] = transform.position;
                    wayPoints[wayPoints.Length - 1] = other.transform.position;
                    for (int i = 0; i < path.corners.Length; i++)
                    {
                        wayPoints[i + 1] = path.corners[i];
                    }
                    float length = 0;
                    for (int i = 1; i < wayPoints.Length; i++)
                    {
                        length += (wayPoints[i] - wayPoints[i - 1]).magnitude;
                    }
                    if (length < collider.radius)
                    {
                        alertPosition = other.transform.position;
                    }
                }
            }

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.player)
        {
            playerInSight = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        preLastPlayerPosition = GameController._instance.lastPlayerPostion;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController._instance.lastPlayerPostion != preLastPlayerPosition)
        {
            alertPosition = GameController._instance.lastPlayerPostion;

            preLastPlayerPosition = GameController._instance.lastPlayerPostion;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowTarget : MonoBehaviour
{

    public Transform player1;
    public Transform player2;

    private Camera camera;
    private Vector3 offset;

    public float distacheRatio=0.58f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - (player1.position + player2.position) / 2;
        camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 == null || player2 == null) 
        {
            Invoke("RestartGame", 2.0f);
            return;
        }
        
        transform.position = (player1.position + player2.position) / 2 + offset;
        float distance = Vector3.Distance(player1.position, player2.position);
        float size = distance * distacheRatio;
        camera.orthographicSize = size;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

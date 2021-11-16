using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public GameObject gameObject;
    public float createTime=3f;
    private bool Created;

    public float StartDistance = 0.42f;
    public float EndDistance = 3.17f;

    public float YDistance;

    public BackGround backGround;

    public static GameManager instance = null;

    public bool IsStopAll = false;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private Vector3 markA;
    private Vector3 markB;
    // Start is called before the first frame update
    void Start()
    {
        IsStopAll = false;
        backGround.rigidbody2D1.velocity= new Vector2(backGround.speed, 0);
        backGround.rigidbody2D2.velocity = new Vector2(backGround.speed, 0);
        markA = backGround.rigidbody2D1.transform.position;
        markB = backGround.rigidbody2D2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(backGround.rigidbody2D2.transform.position.x-markA.x <0)
        {
            backGround.rigidbody2D1.transform.position = markA;
            backGround.rigidbody2D2.transform.position = markB;
        }

        //Debug.Log(backGround.rigidbody2D2.transform.position.x - markA.x);

        if (Created || IsStopAll)
        {
            return;
        }
        StartCoroutine(GenerateObj());
    }


    IEnumerator GenerateObj()
    {
        Created = true;
        YDistance = Random.Range(StartDistance, EndDistance);
        GameObject instance = Instantiate(gameObject, new Vector3(5, YDistance,0), Quaternion.identity) as GameObject;
        instance.transform.SetParent(this.transform);
        yield return new WaitForSeconds(Random.Range(createTime-1, createTime));
        Created = false;
    }

    public void StopAll()
    {
        this.BroadcastMessage("StopMove");
        IsStopAll = true;
        backGround.rigidbody2D1.velocity = Vector2.zero;
        backGround.rigidbody2D2.velocity = Vector2.zero;
        Invoke("RestartGame", 2.0f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}

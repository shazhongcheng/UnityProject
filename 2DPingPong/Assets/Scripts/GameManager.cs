using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private BoxCollider2D rightWall;
    private BoxCollider2D leftWall;
    private BoxCollider2D downWall;
    private BoxCollider2D upWall;
    private BoxCollider2D midLeft;
    private BoxCollider2D midRight;

    public Transform player1;
    public Transform player2;


    private int scoreA;
    private int scoreB;

    public Text scoreAText;
    public Text scoreBText;

    void ResetWall()
    {
        rightWall = transform.Find("RightWall").GetComponent<BoxCollider2D>();
        leftWall = transform.Find("LeftWall").GetComponent<BoxCollider2D>();
        downWall = transform.Find("DownWall").GetComponent<BoxCollider2D>();
        upWall = transform.Find("UpWall").GetComponent<BoxCollider2D>();//  x = Screen.width/2  y = Screen.height

        midLeft = transform.Find("LeftMid").GetComponent<BoxCollider2D>();//  x = Screen.width/2  y = Screen.height
        midRight = transform.Find("RightMid").GetComponent<BoxCollider2D>();//  x = Screen.width/2  y = Screen.height


        //Vector3 upWallPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height));
        //upWall.transform.position = upWallPosition + new Vector3(0, 0.5f,0);
        //float width = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x * 2;
        //upWall.size = new Vector2(width, 1);

        Vector3 tempPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        upWall.transform.position = new Vector3(0, tempPosition.y + 0.5f, 0);
        upWall.size = new Vector2(tempPosition.x * 2, 1);

        downWall.transform.position = new Vector3(0, -tempPosition.y - 0.5f, 0);
        downWall.size = new Vector2(tempPosition.x * 2, 1);

        rightWall.transform.position = new Vector3(tempPosition.x + 0.5f, 0, 0);
        rightWall.size = new Vector2(1, tempPosition.y * 2);

        leftWall.transform.position = new Vector3(-tempPosition.x - 0.5f, 0, 0);
        leftWall.size = new Vector2(1, tempPosition.y * 2);

        midLeft.transform.position = new Vector3(-1, 0, 0);
        midLeft.size = new Vector2(1, tempPosition.y * 2);

        midRight.transform.position = new Vector3(1, 0, 0);
        midRight.size = new Vector2(1, tempPosition.y * 2);
    }

    void Awake()
    {
        _instance = this;
    }

    void ResetPlayer()
    {
        Vector3 player1Position = Camera.main.ScreenToWorldPoint(new Vector3(100, Screen.height / 2, 0));
        player1Position.z = 0;
        player1.position = player1Position;
        Vector3 player2Position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 100, Screen.height / 2, 0));
        player2Position.z = 0;
        player2.position = player2Position;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetWall();
        ResetPlayer();
    }

    public void ChangeScore(string wallName)
    {
        if (wallName == "LeftWall")
        {
            scoreB++;
        }
        else if (wallName == "RightWall")
        {
            scoreA++;
        }

        scoreAText.text = scoreA.ToString();
        scoreBText.text = scoreB.ToString();
    }

    public void Reset()
    {
        scoreA = 0;
        scoreB = 0;
        scoreAText.text = scoreA.ToString();
        scoreBText.text = scoreB.ToString();
        GameObject.Find("Ball").SendMessage("Reset");
    }

    public void CloseGame()
    {
        /*将状态设置false才能退出游戏*/
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

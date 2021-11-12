using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{

    //Time to wait before starting level, in seconds.
    public float levelStartDelay = 2f;
    //Delay between each Player turn.
    public float turnDelay = 0.1f;
    //Starting value for Player food points.
    public int playerFoodPoints = 100;
    //Static instance of GameManager which allows it to be accessed by any other script.

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;

    //Boolean to check if it's players turn, hidden in inspector but public.
    [HideInInspector] public bool playersTurn = true;


    //Text to display current level number.
    private Text levelText;
    //Image to block out level as levels are being set up, background for levelText.
    private GameObject levelImage;

    //Store a reference to our BoardManager which will set up the level.
    private BoardManager boardScript;
    //Current level number, expressed in game as "Day  1".
    private int level = 1;

    //List of all Enemy units, used to issue them move commands.
    private List<Enemy> enemies;
    //Boolean to check if enemies are moving.
    private bool enemiesMoving;
    //Boolean to check if we're setting up board, prevent Player from moving during setup.
    private bool doingSetup = true;

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

        //Assign enemies to a new List of Enemy objects.
        enemies = new List<Enemy>();

        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }


    //This is called each time a scene is loaded.
    void OnLevelWasLoaded(int index)
    {
        //Add one to our level number.
        level++;
        //Call InitGame to initialize our level.
        InitGame();
    }


    //Initializes the game for each level.
    void InitGame()
    {
        //While doingSetup is true the player can't move, prevent player from moving while title card is up.
        doingSetup = true;

        //Get a reference to our image LevelImage by finding it by name.
        levelImage = GameObject.Find("LevelImage");

        //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        //Set the text of levelText to the string "Day" and append the current level number.
        levelText.text = "Day " + level;

        //Set levelImage to active blocking player's view of the game board during setup.
        levelImage.SetActive(true);

        //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
        Invoke("HideLevelImage", levelStartDelay);

        //Clear any Enemy objects in our List to prepare for next level.
        enemies.Clear();
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene(level);

    }

    //Hides black image used between levels
    void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);

        //Set doingSetup to false allowing player to move again.
        doingSetup = false;
    }

    //Update is called every frame.
    void Update()
    {
        //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
        if (playersTurn || enemiesMoving || doingSetup)

            //If any of these are true, return and do not start MoveEnemies.
            return;

        //Start moving enemies.
        StartCoroutine(MoveEnemies());
    }

    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies()
    {
        //While enemiesMoving is true player is unable to move.
        enemiesMoving = true;

        //Wait for turnDelay seconds, defaults to .1 (100 ms).
        yield return new WaitForSeconds(turnDelay);

        //If there are no enemies spawned (IE in first level):
        if (enemies.Count == 0)
        {
            //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
            yield return new WaitForSeconds(turnDelay);
        }

        //Loop through List of Enemy objects.
        for (int i = 0; i < enemies.Count; i++)
        {
            //Call the MoveEnemy function of Enemy at index i in the enemies List.
            enemies[i].MoveEnemy();

            //Wait for Enemy's moveTime before moving next Enemy, 
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;

        //Enemies are done moving, set enemiesMoving to false.
        enemiesMoving = false;
    }


    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {

        //Set levelText to display number of levels passed and game over message
        levelText.text = "After " + level + " days, you starved.";

        //Enable black background image gameObject.
        levelImage.SetActive(true);

        //Disable this GameManager.
        enabled = false;

        //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
        Invoke("CloseGame", 2.0f);
    }

    public void CloseGame()
    {
        /*将状态设置false才能退出游戏*/
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }


}

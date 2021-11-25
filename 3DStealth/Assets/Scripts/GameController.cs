using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController _instance;

    public bool alermOn = false;
    public Vector3 lastPlayerPostion = Vector3.zero;

    public AudioSource musicNormal;
    public AudioSource musicPanic;
    public float musicFadeSpeed = 1;

    private GameObject[] sirens;


    


    private void Awake()
    {
        _instance = this;

        alermOn = false;
        sirens = GameObject.FindGameObjectsWithTag(Tags.siren);
    }

    // Start is called before the first frame update
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        AlremLight._intance.SetAlermStatus(alermOn);

        if (alermOn)
        {
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0, Time.deltaTime * musicFadeSpeed);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0.2f, Time.deltaTime * musicFadeSpeed);
            PlaySiren();
        }
        else
        {
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0.2f, Time.deltaTime * musicFadeSpeed);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0, Time.deltaTime * musicFadeSpeed);
            StopSiren();
        }

    }


    public void SeePlayer(Transform player)
    {
        alermOn = true;
        lastPlayerPostion = player.position;
    }

    private void PlaySiren()
    {
        foreach (GameObject go in sirens)
        {
            if (!go.GetComponent<AudioSource>().isPlaying)
            {
                go.GetComponent<AudioSource>().Play();
            }
        }
    }
    private void StopSiren()
    {
        foreach (GameObject go in sirens)
        {
            go.GetComponent<AudioSource>().Stop();
        }
    }

}

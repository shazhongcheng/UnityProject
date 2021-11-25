using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public float coldTime = 2.0f;

    public Image image;

    public KeyCode mykey;

    private float timer = 0;

    private bool isStartTimer=false;

    // Start is called before the first frame update
    void Start()
    {
        isStartTimer = true;
    }

    public  void OnClick()
    {
        isStartTimer = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(mykey) && !isStartTimer)
        {
            isStartTimer = true;
        }

        if (isStartTimer)
        {
            timer += Time.deltaTime;
            image.fillAmount = (coldTime - timer) / coldTime;
            if (timer >= coldTime)
            {
                image.fillAmount = 0;
                timer = 0;
                isStartTimer = false;
            }
        }
    }
}

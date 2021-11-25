using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyToogle : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject isOnGameObject;

    public GameObject isOffGameObject;

    private Toggle toggle;


    void Start()
    {
        toggle = GetComponent<Toggle>();
    
        toggle.onValueChanged.AddListener(OnValueChanged);
        toggle.isOn = true;
        //OnValueChanged(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged(bool isOn)
    {
        isOnGameObject.SetActive(isOn);
        isOffGameObject.SetActive(!isOn);

    }

}

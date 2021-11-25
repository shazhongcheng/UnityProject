using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlremLight : MonoBehaviour
{

    public static AlremLight _intance;

    
    public float animationSpeed = 1;

    [SerializeField]
    private bool alermOn = false;

    private float lowIntentsity = 0;
    private float highIntensity = 0.5f;

    private float targetIntensity;

    private Light light;


    private void Awake()
    {
        _intance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();

        targetIntensity = highIntensity;
        alermOn = false;
        
    }

    public void SetAlermStatus(bool status)
    {
        alermOn = status;
    }

    // Update is called once per frame
    void Update()
    {
        if (alermOn)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, Time.deltaTime * animationSpeed);
            if (Mathf.Abs(light.intensity - targetIntensity) < 0.05f)
            {
                if (targetIntensity == highIntensity)
                {
                    targetIntensity = lowIntentsity;
                }
                else if (targetIntensity == lowIntentsity)
                {
                    targetIntensity = highIntensity;
                }
            }
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime * animationSpeed);
        }
    }
}

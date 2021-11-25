using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonScrollList : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{

    public ScrollRect scrollRect;
    private float[] pageArray = new float[] { 0f, 0.33f, 0.66f, 1f };
    public float smooth = 4;
    private bool isDragging=false;

    public Toggle[] toggleArray;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        //throw new System.NotImplementedException();
    }
    private float targetHorizontalNormalizedPosition = 0;
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        //throw new System.NotImplementedException();
        float posX = scrollRect.horizontalNormalizedPosition;

        int index = 0;
        float offset = Mathf.Abs(pageArray[index] - posX);

        for (int i = 0; i < pageArray.Length; i++)
        {
            float offsettemp = Mathf.Abs(pageArray[i] - posX);
            if (offsettemp < offset)
            {
                index = i;
                offset = offsettemp;
            }
        }
        targetHorizontalNormalizedPosition= pageArray[index];
        toggleArray[index].isOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = this.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging==false)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetHorizontalNormalizedPosition, Time.deltaTime * smooth);
        }
        
    }

    public void MoveToPage1(bool isOn)
    {
        targetHorizontalNormalizedPosition = pageArray[0];
    }

    public void MoveToPage2(bool isOn)
    {
        targetHorizontalNormalizedPosition = pageArray[1];
    }

    public void MoveToPage3(bool isOn)
    {
        targetHorizontalNormalizedPosition = pageArray[2];
    }

    public void MoveToPage4(bool isOn)
    {
        Debug.Log(isOn);
        targetHorizontalNormalizedPosition = pageArray[3];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draging_Resizing : MonoBehaviour
{
    private  RectTransform instanceGO;

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool isOriginal;


  
    void OnMouseDown()
    {
        if (isOriginal)
        {
            instanceGO = Instantiate<RectTransform>(transform as RectTransform) ;
            instanceGO.GetComponent<Draging_Resizing>().isOriginal = false;
        }
        
      

        screenPoint = Camera.main.WorldToScreenPoint(instanceGO.transform.position);
        offset = instanceGO.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        instanceGO.transform.position = curPosition;
    }

  
}

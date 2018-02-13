using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateClone : MonoBehaviour,IPointerDownHandler {
    public GameObject dragArea;
    RectTransform newInstance;
    public static int InstanceId = 0;

    // Use this for initialization

    
    /*public void Create(GameObject dragArea)
    {
        
        GameObject newInstance = Instantiate(gameObject, gameObject.transform.position , gameObject.transform.rotation , dragArea.transform) as GameObject;
        newInstance.gameObject.GetComponent<DragableUI>().isOriginal = false;
        newInstance.gameObject.GetComponent<CreateClone>().enabled = false;
        
    }*/
     void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.GetComponent<DragableUI>().isIsntance) return;
        newInstance = Instantiate(gameObject.transform, gameObject.transform.position, gameObject.transform.rotation, dragArea.transform) as RectTransform;
        I_Stcky_Manager.ObjectsToHide.Add(newInstance.gameObject);
        I_Stcky_Manager.ObjectsToHide.Remove(gameObject);
        newInstance.gameObject.name = "in" + InstanceId as string;
        InstanceId++;
        newInstance.gameObject.GetComponent<DragableUI>().isIsntance = false;
        gameObject.GetComponent<DragableUI>().isIsntance = true;
        // newInstance.gameObject.GetComponent<CreateClone>().enabled = false;
        foreach (var item in I_Stcky_Manager.ObjectsToHide)
        {
            Debug.Log(item.name);
        }
    }

    

}

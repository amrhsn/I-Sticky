using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zooming : MonoBehaviour {

    public float scaleFactor;
    public int scaleLimit;
    Vector3 startScale;
   


    public GameObject selectedObject;
    private bool scaling = false;
    // Use this for initialization
    void Start () {
        startScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchZero.position);
            //RaycastHit hit1;
            Ray ray1 = Camera.main.ScreenPointToRay(touchOne.position);
            if (Physics.Raycast(ray, out hit) || Physics.Raycast(ray1, out hit))
                if (hit.collider != null)
                {
                    selectedObject = hit.collider.gameObject;
                    selectedObject.transform.localScale = new Vector3(startScale.x * 1.2f , startScale.y * 1.2f , startScale.z *1.2f);

                    // Find the position in the previous frame of each touch.
                    /*Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    selectedObject.transform.localScale += startScale * deltaMagnitudeDiff * scaleFactor * -0.01f;
                    selectedObject.transform.localScale = Vector3.Max(transform.localScale, startScale * scaleLimit / 10);
                    selectedObject.transform.localScale = Vector3.Min(transform.localScale, startScale * scaleLimit);*/
                }
                    //hit.collider.enabled = false;
                
            // Store both touches.
            

            
        }
       
    }
}

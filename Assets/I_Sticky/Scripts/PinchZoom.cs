using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float scaleFactor;        
    public int scaleLimit;
    Vector3 startScale;


    void Start()
    {
        startScale = transform.localScale;

    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            transform.localScale += startScale * deltaMagnitudeDiff * scaleFactor * -0.01f;
            transform.localScale = Vector3.Max(transform.localScale , startScale * scaleLimit / 10);
            transform.localScale = Vector3.Min(transform.localScale, startScale * scaleLimit);

            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

    private void OnMouseDown()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}

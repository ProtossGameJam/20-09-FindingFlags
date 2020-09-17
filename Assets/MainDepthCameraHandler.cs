using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDepthCameraHandler : MonoBehaviour
{
    Vector3 originalPoisition;
    // Start is called before the first frame update
    void Start()
    {
        originalPoisition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originalPoisition + (Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0))/1200;
    }
}

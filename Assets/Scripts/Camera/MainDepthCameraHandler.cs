using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDepthCameraHandler : MonoBehaviour
{
    [ReadOnly] [SerializeField] private Vector3 screenSize;
    [ReadOnly] [SerializeField] private Vector3 originalPosition;

    [SerializeField] private float sensitivity;
    
    private void Start()
    {
        originalPosition = transform.position;
        screenSize = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
    }

    private void Update()
    {
        transform.position = originalPosition + (Input.mousePosition - screenSize) / (sensitivity * 100.0f);
    }
}

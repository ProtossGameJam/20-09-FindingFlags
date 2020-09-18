using System;
using System.Collections;
using UnityEngine;

public class InteractModule : MonoBehaviour
{
    [SerializeField] private GameObject activeObject;

    protected virtual void Awake() {
        activeObject.SetActive(false);
    }

    public virtual void ActiveModuleObject() {
        StartCoroutine(TestActiveMethod());
    }

    private IEnumerator TestActiveMethod() {
        activeObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        activeObject.SetActive(false);
    }
}
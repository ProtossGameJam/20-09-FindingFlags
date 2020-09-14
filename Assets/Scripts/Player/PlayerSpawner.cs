using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PhotonView pView;

    [SerializeField] private Transform[] spawnPoint;

    private void Awake()
    {
        if (pView == null) {
            pView = PhotonView.Get(gameObject);
        }
    }
}

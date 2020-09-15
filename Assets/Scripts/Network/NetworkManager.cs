using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private int sendRate;
    [SerializeField] private int serializationRate;

    private void Reset()
    {
        sendRate = 60;
        serializationRate = 30;
    }

    private void Awake()
    {
        PhotonNetwork.SendRate = sendRate;
        PhotonNetwork.SerializationRate = serializationRate;
    }
}

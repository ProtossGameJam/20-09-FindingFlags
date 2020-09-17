using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private int sendRate;
    [SerializeField] private int serializationRate;

    private void Awake() {
        PhotonNetwork.SendRate = sendRate;
        PhotonNetwork.SerializationRate = serializationRate;
    }

    private void Reset() {
        sendRate = 60;
        serializationRate = 30;
    }
}
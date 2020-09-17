using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum FlagColor
{
    RED, ORANGE, YELLOW, GREEN, BLUE, NAVY, PURPLE, WHITE, BLACK
}

public class FlagManager : MonoBehaviourPun, IPunObservable
{
    [ReadOnly] [SerializeField] private List<FlagColor> currentFlag;
    [SerializeField] private int flagStoreSize;

    private void Awake()
    {
        currentFlag = new List<FlagColor>(flagStoreSize);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    public void GetFlag(FlagColor color, int count = 1)
    {
        if (currentFlag.Contains(color)) return;

        if (currentFlag.Count != currentFlag.Capacity) {
            currentFlag.Add(color);
            photonView.RPC("GetFlagAnnounce", RpcTarget.Others, PhotonNetwork.NickName, color, count);
        }
    }
    
    [PunRPC]
    public void GetFlagAnnounce(string nick, FlagColor color, int count)
    {
        print($"[DEBUG] RPC : GetFlagAnnounce() - Nickname : {nick}, Color : {color.ToString()}, Count : {count}개");
    }
}

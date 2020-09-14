using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RoomListUpdator : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent<List<RoomInfo>> roomUpdateCallback;

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //방 리스트가 업데이트 될 때
    {
        roomUpdateCallback.Invoke(roomList);
    }
}
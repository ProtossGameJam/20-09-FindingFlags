using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class RoomListUpdater : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent<List<RoomInfo>> roomUpdateCallback;

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        print("[DEBUG] Callback : OnRoomListUpdate()");
        roomUpdateCallback.Invoke(RoomDictionaryUpdate(roomList));
    }

    private List<RoomInfo> RoomDictionaryUpdate(List<RoomInfo> roomList) {
        print("[DEBUG] Execute : RoomDictionaryUpdate()");
        var tempList = new List<RoomInfo>();
        print($"[DEBUG] Room Count : {roomList.Count}");
        foreach (var room in roomList.Where(room => room.MaxPlayers != 0)) {
            print($"[DEBUG] Room Info : {room.ToStringFull()}"); // DEBUG
            tempList.Add(room);
        }

        return tempList;
    }
}
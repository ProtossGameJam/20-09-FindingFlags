using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RoomDictionary : Dictionary<string, RoomInfo> { }

public class RoomListUpdator : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent<RoomDictionary> roomUpdateCallback;

    private RoomDictionary roomDic;

    private void Awake()
    {
        roomDic = new RoomDictionary();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //방 리스트가 업데이트 될 때
    {
        print("[DEBUG] Callback : OnRoomListUpdate()");
        roomUpdateCallback.Invoke(RoomDictionaryUpdate(roomList));
    }

    private RoomDictionary RoomDictionaryUpdate(List<RoomInfo> roomList)
    {
        print("[DEBUG] Execute : RoomDictionaryUpdate()");
        roomDic.Clear();
        
        print($"[DEBUG] Room Count : {roomList.Count}");
        foreach (var room in roomList.Where(room => room.IsVisible && !room.RemovedFromList)) {
            DebugPrintRoomInfo(room); // DEBUG
            roomDic.Add(room.Name, room);
        }

        return roomDic;
    }

    private static void DebugPrintRoomInfo(RoomInfo info)
    {
        print($"[DEBUG] Room Info : {info.ToStringFull()}");
    }
}
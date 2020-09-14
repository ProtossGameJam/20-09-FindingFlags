using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using UnityEngine;

public class UIRoomList : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;

    [SerializeField] private Transform uiContentParent;
    [ReadOnly] [SerializeField] private List<GameObject> uiRoomList;

    [SerializeField] private bool hideRemovedRoom;

    public void SettingRoom(IEnumerable<RoomInfo> roomList)
    {
        DebugPrintRoomInfo(roomList);
        CleaningRoom();
        
        print("[DEBUG] Method : SettingRoom()");
        foreach (var room in roomList.Where(room => !room.RemovedFromList && !hideRemovedRoom)) {
            var obj = Instantiate(uiPrefab, uiContentParent);
            obj.GetComponent<UIRoom>().Setup(room);
            uiRoomList.Add(obj);
        }
    }

    private void CleaningRoom()
    {
        print("[DEBUG] Method : CleaningRoom()");
        foreach (var room in uiRoomList) {
            DestroyImmediate(room.gameObject);
        }
        uiRoomList.Clear();
    }

    private void DebugPrintRoomInfo(IEnumerable<RoomInfo> roomList)
    {
        foreach (var room in roomList) {
            print($"[DEBUG] Room Info / Name : {room.Name}, Full : {room.MaxPlayers}, Current : {room.PlayerCount}");
        }
    }
}
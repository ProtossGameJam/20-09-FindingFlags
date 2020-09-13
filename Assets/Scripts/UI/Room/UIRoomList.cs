using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class UIRoomList : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;

    [SerializeField] private Transform uiContentParent;
    [ReadOnly] [SerializeField] private List<UIRoom> uiRoomList;

    [SerializeField] private bool hideRemovedRoom;
    
    public void SettingRoom(IEnumerable<RoomInfo> roomList)
    {
        CleaningRoom();
        
        print("[DEBUG] Method : SettingRoom()");
        foreach (var room in roomList.Where(room => !room.RemovedFromList && !hideRemovedRoom)) {
            uiRoomList.Add(Instantiate(uiPrefab, uiContentParent).GetComponent<UIRoom>().Setup(room));
        }
    }

    private void CleaningRoom()
    {
        print("[DEBUG] Method : CleaningRoom()");
        foreach (var room in uiRoomList) {
            DestroyImmediate(room.gameObject);
        }
    }
}
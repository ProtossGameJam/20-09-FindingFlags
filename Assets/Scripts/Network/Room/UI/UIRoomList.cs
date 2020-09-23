using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class UIRoomList : MonoBehaviour {
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private Transform uiContentParent;

    private UIRoom[] _roomObjs;

    public void SettingRoomUI(List<RoomInfo> roomList) {
        print("[DEBUG] Execute : SettingRoomUI()");
        if (_roomObjs != null) {
            foreach (var room in _roomObjs) DestroyImmediate(room.gameObject);
        }

        _roomObjs = new UIRoom[roomList.Count];
        for (var i = 0; i < roomList.Count; i++)
            _roomObjs[i] = Instantiate(uiPrefab, uiContentParent).GetComponent<UIRoom>().Setup(roomList[i]);
    }
}
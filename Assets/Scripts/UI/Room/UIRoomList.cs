using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using UnityEngine;

public class UIRoomList : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private Transform uiContentParent;

    private UIRoom[] _roomObjs;

    public void SettingRoomUI(RoomDictionary dicObj)
    {
        print("[DEBUG] Execute : SettingRoomUI()");
        CleanRoomUI();
        _roomObjs = new UIRoom[dicObj.Count];
        var index = 0;
        foreach (var room in dicObj.Values) {
            _roomObjs[index++] = Instantiate(uiPrefab, uiContentParent).GetComponent<UIRoom>().Setup(room);
        }
    }

    private void CleanRoomUI()
    {
        print("[DEBUG] Execute : CleanRoomUI()");
        if (_roomObjs == null) return;
        for (var i = 0; i < _roomObjs.Length; i++) {
            DestroyImmediate(_roomObjs[i].gameObject);
        }
    }
}
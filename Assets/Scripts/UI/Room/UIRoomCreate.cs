using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class UIRoomCreate : MonoBehaviour
{
    [ReadOnly] [SerializeField] private string roomName;
    
    public void SetRoomName(string name)
    {
        roomName = name;
    }
    
    /// <summary>
    /// Check if Room's name is Empty
    /// </summary>
    /// <param name="name">used by room's name</param>
    public void CheckNameIsEmpty(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) {
            print("[DEBUG] No name has typed in field. Use default name or previous name.");
            name = Constants.DEFAULT_ROOM_NAME;
        }
        SetRoomName(name);
    }
    
    public void CreateRoom()
    {
        // Call: OnCreateRoom, OnJoinedRoom
        LobbyManager.CreateRoom(roomName);
    }
}
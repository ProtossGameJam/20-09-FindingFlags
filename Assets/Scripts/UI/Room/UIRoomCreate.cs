using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class UIRoomCreate : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomNameInputField;

    public void CreateRoom()
    {
        var roomName = roomNameInputField.text;
        if (string.IsNullOrWhiteSpace(roomName))
        {
            roomName = Constants.DEFAULT_ROOM_NAME;
            UIDebugText.Logging("Room name is empty. Use default name");
        }

        // Call: OnCreateRoom, OnJoinedRoom
        LobbyManager.Instance.CreateRoom(roomName);
    }
}
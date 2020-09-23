using TMPro;
using UnityEngine;

public class UIRoomCreate : MonoBehaviour {
    [SerializeField] private TMP_InputField inputField;

    [ReadOnly, SerializeField] private string roomName;

    public void SetRoomName(string name) => roomName = name;

    /// <summary> Check if Room's name is Empty </summary>
    /// <param name="name"> used by room's name </param>
    public void CheckNameIsEmpty(string name) {
        if (string.IsNullOrWhiteSpace(name)) {
            print("[DEBUG] No name has typed in field. Use default name or previous name.");
            SetRoomName(Constants.DEFAULT_ROOM_NAME);
            inputField.text = Constants.DEFAULT_ROOM_NAME;
        }
    }

    public void CreateRoom() =>
            // Call: OnCreateRoom, OnJoinedRoom
            LobbyManager.CreateRoom(roomName);
}
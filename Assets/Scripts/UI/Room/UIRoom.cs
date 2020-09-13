using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class UIRoom : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text roomName;

    [SerializeField] private TMP_Text roomCurrentCount;
    [SerializeField] private TMP_Text roomMaxCount;

    private RoomInfo _roomData;

    public UIRoom Setup(RoomInfo info)
    {
        _roomData = info;
        
        roomName.text = info.Name;

        roomCurrentCount.text = info.PlayerCount.ToString();
        roomMaxCount.text = info.MaxPlayers.ToString();

        return this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("[DEBUG] Class : UIRoom / Method : OnPointerClick()");
        LobbyManager.Instance.JoinRoom(_roomData);
    }
}
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class UIDebugRoomList : MonoBehaviour
{
    [ReadOnly] [SerializeField] private List<UIDebugPlayerText> playerList;

    [SerializeField] private GameObject debugPlayerText;
    
    public void DebugEnterPlayer(Player player)
    {
        Instantiate(debugPlayerText, transform).GetComponent<UIDebugPlayerText>().SetDebugText($"{player.NickName}({player.UserId})", true);
    }
    public void DebugLeftPlayer(Player player)
    {
        Instantiate(debugPlayerText, transform).GetComponent<UIDebugPlayerText>().SetDebugText($"{player.NickName}({player.UserId})", false);
    }
}

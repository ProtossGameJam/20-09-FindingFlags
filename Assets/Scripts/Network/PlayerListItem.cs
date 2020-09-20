using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player;

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //해당 플레이어가 이탈했을때.
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom() //내가 나갔을때.
    {
        Destroy(gameObject);
    }
}

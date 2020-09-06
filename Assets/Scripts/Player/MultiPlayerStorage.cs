using System.Collections.Generic;
using System.Linq;
using FlagGame;
using UnityEngine;

public class MultiPlayerStorage : MonoBehaviour
{
     [System.Serializable]
     public class PlayerSocket
     {
          public bool IsEmpty;
          public CharacterColor Color;
          public CharacterManager Manager;
     }
     
     [ReadOnly] [SerializeField] private List<PlayerSocket> players;

     public bool IsPlayerFull => players.All(player => !player.IsEmpty);

     public CharacterColor GetRemainColor
     {
          get {
               foreach (var player in players.Where(player => player.IsEmpty)) {
                    return player.Color;
               }
               Debug.LogError("[ERROR] 플레이어 목록이 Full 입니다. 하지만 FindEmptyColor에서 색상을 찾으려 하고 있습니다.");
               return CharacterColor.RED;
          }
     }

     private void Awake()
     {
          players = new List<PlayerSocket>();

          for (var i = 0; i < 4; i++) {
               players.Add(new PlayerSocket { IsEmpty = true, Manager = null });
          }

          players[0].Color = CharacterColor.RED;
          players[1].Color = CharacterColor.YELLOW;
          players[2].Color = CharacterColor.GREEN;
          players[3].Color = CharacterColor.BLUE;
     }

     public void AddPlayer(CharacterManager character)
     {
          foreach (var player in players.Where(player => player.IsEmpty)) {
               player.IsEmpty = false;
               player.Manager = character;
               break;
          }
     }
}
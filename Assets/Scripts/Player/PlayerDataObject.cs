using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player Data Create", order = 0)]
public class PlayerDataObject : ScriptableObject
{
    [SerializeField] private string playerName;

    public string PlayerName {
        get {
            if (string.IsNullOrWhiteSpace(playerName)) playerName = Constants.DEFAULT_PLAYER_NAME;
            return playerName;
        }
        set => playerName = value;
    }
}
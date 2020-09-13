using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Create PlayerData", order = 0)]
public class PlayerDataObject : ScriptableObject
{
    [SerializeField] private string playerName;

    public string PlayerName
    {
        get {
            if (string.IsNullOrWhiteSpace(playerName)) {
                playerName = Constants.DEFAULT_PLAYER_NAME;
            }
            return playerName;
        }
        set => playerName = value;
    }
}
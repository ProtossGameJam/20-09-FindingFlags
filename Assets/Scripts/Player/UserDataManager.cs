using UnityEngine;

public class UserDataManager : MonoSingleton<UserDataManager>
{
    [SerializeField] private PlayerDataObject playerData;

    protected override void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static string GetNickname(bool preventDuplicate = false)
    {
        if (preventDuplicate) {
            return Instance.playerData.PlayerName + Random.Range(0, 10000).ToString("0000");
        }
        return Instance.playerData.PlayerName;
    }

    public static void SetNickname(string nick)
    {
        Instance.playerData.PlayerName = nick;
    }
}
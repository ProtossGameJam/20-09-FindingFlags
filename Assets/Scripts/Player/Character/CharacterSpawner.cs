using FlagGame;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public enum CharacterColor
{
    RED, YELLOW, GREEN, BLUE
}

public class CharacterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject character;
        public Transform point;
    }

    [System.Serializable]
    public class CharacterDictionary : SerializableDictionaryBase<CharacterColor, SpawnInfo> { }

    [SerializeField] private CharacterDictionary characterDic;

    public CharacterManager SpawnCharacter(CharacterColor color)
    {
        return Instantiate(characterDic[color].character, characterDic[color].point.position, Quaternion.identity).GetComponent<CharacterManager>();
    }

    public CharacterManager SpawnCharacter(CharacterColor color, Vector3 position)
    {
        return Instantiate(characterDic[color].character, position, Quaternion.identity).GetComponent<CharacterManager>();
    }

    public SpawnInfo GetSpawnInfo(CharacterColor color)
    {
        if (!characterDic.ContainsKey(color))
            return null;
        return characterDic[color];
    }
}

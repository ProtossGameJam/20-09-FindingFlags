using UnityEngine;

namespace FlagGame
{
    [System.Serializable]
    public abstract class CharacterData
    {
        public CharacterColor color;
    }
    
    public class CharacterManager : MonoBehaviour
    {
        public CharacterData Data;
    }
}
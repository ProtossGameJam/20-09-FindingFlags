using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[System.Serializable]
public class GameObjectDictionary<T> : SerializableDictionaryBase<T, GameObject> where T : System.Enum
{
    public GameObject Instantiate(T type, Transform parent)
    {
        return Object.Instantiate(this[type], parent);
    }
    public GameObject Instantiate(T type, Vector3 position)
    {
        return Object.Instantiate(this[type], position, Quaternion.identity);
    }
    public GameObject Instantiate(T type, Vector3 position, Quaternion rotation, Transform parent)
    {
        return Object.Instantiate(this[type], position, rotation, parent);
    }
}
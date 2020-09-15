using Photon.Pun;
using UnityEngine;

/// <summary>
///     Mono singleton Class. Extend this class to make singleton component.
///     Example:
///     <code>
/// public class Foo : MonoSingleton<Foo>
/// </code>
///     . To get the instance of Foo class, use <code>Foo.instance</code>
///     Override <code>Init()</code> method instead of using <code>Awake()</code>
///     from this class.
/// </summary>
public abstract class PunSingleton<T> : MonoBehaviourPunCallbacks where T : PunSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get {
            // Instance requiered for the first time, we look for it
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (_instance == null) {
                    Debug.LogWarning("No instance of " + typeof(T) + ", a temporary one is created.");

                    IsTemporaryInstance = true;
                    _instance = new GameObject("Temp Instance of " + typeof(T), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (_instance == null) Debug.LogError("Problem during the creation of " + typeof(T));
                }
            }

            return _instance;
        }
    }

    public static bool IsTemporaryInstance { private set; get; }

    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    protected virtual void Awake()
    {
        if (_instance == null) {
            _instance = this as T;
        }
        else if (_instance != this) {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(this);
            return;
        }

        _instance.Initialize();
    }

    protected virtual void OnDestroy()
    {
        DestroyImmediate(gameObject);
    }

    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        _instance = null;
    }

    /// <summary>
    ///     This function is called when the instance is used the first time
    ///     Put all the initializations you need here, as you would do in Awake
    /// </summary>
    protected virtual void Initialize() { }
}
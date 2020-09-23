using UnityEngine;

public class EventTimer : MonoBehaviour {
    [ReadOnly, SerializeField] private float coolTime;
    [ReadOnly] public bool isRunning;

    public float CoolTime {
        set {
            if (coolTime + value < 0) {
                isRunning = false;
                coolTime = 0;
            }
            else {
                coolTime = value;
            }
        }
        get => coolTime;
    }

    public void StartTimer(float time) {
        CoolTime = time;
        isRunning = true;
    }
}
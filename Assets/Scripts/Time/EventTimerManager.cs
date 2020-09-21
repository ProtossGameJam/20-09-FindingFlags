using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventTimerManager : MonoBehaviour
{
    [ReadOnly] [SerializeField] private List<EventTimer> timerList;

    private void Awake() {
        timerList = new List<EventTimer>();
    }

    private void Update() {
        foreach (var timer in timerList.Where(timer => timer.isRunning)) {
            timer.CoolTime -= Time.deltaTime;
        }
    }

    public void RegisterTimer(EventTimer timer) {
        timerList.Add(timer);
    }
}
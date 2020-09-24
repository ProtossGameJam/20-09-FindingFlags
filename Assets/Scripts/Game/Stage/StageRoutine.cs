using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StageRoutine : MonoBehaviour {
    [SerializeField] private UIStageRoutine uiRoutine;

    [SerializeField] private UnityEvent<int> secondEvent;
    [SerializeField] private UnityEvent startEvent;

    [SerializeField] private int countdownTime;

    private Coroutine countRoutine;
    private WaitForSeconds waitOneSecond;

    private void Awake() {
        waitOneSecond = new WaitForSeconds(1.0f);
    }

    public void ReadyStage() {
        countRoutine = StartCoroutine(StartCountdown());
    }

    public void CancelStage() {
        StopCoroutine(countRoutine);
        secondEvent.Invoke(-1);
    }

    private IEnumerator StartCountdown() {
        for (var i = countdownTime; i >= 0; i--) {
            secondEvent.Invoke(i);
            yield return waitOneSecond;
        }

        secondEvent.Invoke(-1);
        startEvent.Invoke();
    }
}
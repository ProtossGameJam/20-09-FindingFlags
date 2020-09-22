using UnityEngine;
using UnityEngine.Events;

public class StageRoutine : MonoBehaviour
{
    [SerializeField] private UIStageRoutine uiRoutine;

    [SerializeField] private UnityEvent startEvent;

    public void ReadyStage() {
        uiRoutine.StartCountdown(5, startEvent);
    }

    public void CancelStage() {
        uiRoutine.StopCountdown();
    }
}
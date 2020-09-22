using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIStageRoutine : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;

    private WaitForSeconds waitOneSecond;

    private void Awake() {
        waitOneSecond = new WaitForSeconds(1.0f);
    }

    public void StartCountdown(int count, UnityEvent startAction) {
        StartCoroutine(UICountdown(count, startAction));
    }

    public void StopCountdown() {
        StopAllCoroutines();
        countdownText.text = "";
    }

    private IEnumerator UICountdown(int count, UnityEvent startAction) {
        for (var i = count - 1; i >= 0; i--) {
            countdownText.text = $"{i + 1}초 후에 게임이 시작됩니다!";
            yield return waitOneSecond;
        }

        countdownText.text = "";
        startAction.Invoke();
    }
}
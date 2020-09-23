using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIStageRoutine : MonoBehaviour {
    [SerializeField] private GameObject textObject;
    [SerializeField] private TMP_Text countdownText;

    public void WriteTextSecond(int sec) {
        if (sec > 0) {
            if (!textObject.activeSelf) {
                textObject.SetActive(true);
            }
            countdownText.text = $"{sec}초 후에 게임이 시작됩니다!";
        }
        else if (sec == 0) {
            countdownText.text = "게임 시작!";
        }
        else {
            textObject.SetActive(false);
        }
    }
}
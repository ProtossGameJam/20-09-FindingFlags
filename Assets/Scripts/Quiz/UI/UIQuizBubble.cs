using TMPro;
using UnityEngine;

public class UIQuizBubble : MonoBehaviour {
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text[] answerText;

    public void SetQuizUI(QuizData data) {
        descriptionText.text = data.desc;
        for (var i = 0; i < answerText.Length; i++) answerText[i].text = data.answer[i];
    }

    public void ActiveBubble(bool isActive) => gameObject.SetActive(isActive);
}
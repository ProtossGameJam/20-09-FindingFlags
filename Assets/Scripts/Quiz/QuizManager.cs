using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour {
    [SerializeField] private UIQuizBubble uiBubble;

    [SerializeField] private FlagManager flagManager;

    [SerializeField] private List<QuizData> quizData;
    [ReadOnly, SerializeField] private QuizData currentQuiz;

    private DialogueViewer callDialogueViewer;
    private bool inProgress;

    private void Awake() {
        if (flagManager == null) flagManager = FindObjectOfType<FlagManager>();
    }

    private void ResetQuizManager() {
        inProgress = false;
        currentQuiz = null;
        callDialogueViewer = null;
    }

    /// <summary>
    /// 새로운 퀴즈를 준비하고 시작하는 함수. DialogueManager 에서 Code를 감지하고 실행하게 됨.
    /// </summary>
    /// <param name="dialogue"> 호출한 Dialogue 오브젝트 </param>
    /// <param name="pos"> Quiz Bubble UI position </param>
    /// <param name="id"> Quiz ID </param>
    public void StartQuiz(DialogueViewer dialogue, int code) {
        Debug.Log("[DEBUG] Execute : StartQuiz()");
        if (!inProgress) {
            inProgress = true;

            callDialogueViewer = dialogue;
            var tempQuiz = quizData.Find(quiz => quiz.code == code);
            uiBubble.SetQuizUI(tempQuiz);
            currentQuiz = tempQuiz;
            dialogue.ActiveBubble(false);
            uiBubble.ActiveBubble(true);
        }
    }

    public void CancelQuiz() {
        callDialogueViewer.cooldownTime = 5.0f;

        uiBubble.ActiveBubble(false);

        callDialogueViewer.SetIndex(0);
        callDialogueViewer.EndDialogue();

        ResetQuizManager();
    }

    public void SelectAnswer(int index) {
        if (currentQuiz.correct == index) { // 정답
            callDialogueViewer.cooldownTime = -1.0f;
            Debug.Log(
                    $"[DEBUG] Execute : SelectAnswer() - Get Flag Color : {callDialogueViewer.transform.GetComponent<DefaultNPC>().ownFlag}"
            );
            flagManager.GetFlag(callDialogueViewer.transform.GetComponent<DefaultNPC>().ownFlag);
            // TODO: 정답이므로 깃발 증정
        }
        else { // 오답
            callDialogueViewer.cooldownTime = 30.0f;
        }

        uiBubble.ActiveBubble(false);

        callDialogueViewer.SetIndex($"s-{index + 1}");
        callDialogueViewer.PlayDialogue();

        ResetQuizManager();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoSingleton<QuizManager>
{
    [SerializeField] private UIQuizBubble uiBubble;

    [SerializeField] private List<QuizData> quizData;

    private DialogueViewer callDialogueViewer;
    private QuizData curQuizData;
    private bool inProgress;

    private void ResetQuizManager() {
        inProgress = false;
        curQuizData = null;
        callDialogueViewer.ResetDialogue();
        callDialogueViewer = null;
    }
    
    /// <summary>
    /// 새로운 퀴즈를 준비하고 시작하는 함수. DialogueManager 에서 Code를 감지하고 실행하게 됨.
    /// </summary>
    /// <param name="dialogue">호출한 Dialogue 오브젝트</param>
    /// <param name="pos">Quiz Bubble UI position</param>
    /// <param name="id">Quiz ID</param>
    public void StartQuiz(DialogueViewer dialogue, string code) {
        Debug.Log("[DEBUG] Execute : StartQuiz()");
        if (inProgress) return;
        inProgress = true;

        callDialogueViewer = dialogue;
        
        curQuizData = quizData.Find(quiz => string.Equals(quiz.code, code));
        uiBubble.SetQuizUI(curQuizData);
        uiBubble.ActiveBubble(true);
    }

    public void CancelQuiz() {
        callDialogueViewer.timer.StartTimer(5.0f);
        ResetQuizManager();
        uiBubble.ActiveBubble(false);
    }

    public void SelectAnswer(int index) {
        if (curQuizData.correct == index) {
            callDialogueViewer.isAnsweredDialogue = true;
            // TODO: 정답이므로 깃발 증정
        }
        else {
            callDialogueViewer.isAnsweredDialogue = false;
            callDialogueViewer.timer.StartTimer(30.0f);
        }
        callDialogueViewer.PrintEventSentence($"#s{index + 1}");
        ResetQuizManager();
        uiBubble.ActiveBubble(false);
    }
}
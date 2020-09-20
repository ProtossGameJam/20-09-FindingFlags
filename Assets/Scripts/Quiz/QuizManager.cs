using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Purpose: 퀴즈와 관련된 전반적인 것들을 관리해주는 클래스
 * Notice: 
 */
public class QuizManager : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private QuizObject obj_quizObj;    //퀴즈 스크립터블 오브젝트
    [SerializeField] private GameObject obj_pivot;      //퀴즈 오브젝트 컨테이너 
    [SerializeField] private QuizBubble quizBubble;

    [Header("UI")]
    [SerializeField] private TMP_Text ui_description;    //퀴즈 내용 
    [SerializeField] private TMP_Text ui_answerText1;    //버튼 1번
    [SerializeField] private TMP_Text ui_answerText2;    //버튼 2번
    [SerializeField] private TMP_Text ui_answerText3;    //버튼 3번

    private DialogueManager obj_currentDm;               //최근에 호출받은 DialogueManager



    /// <summary>
    /// 새로운 퀴즈를 준비하고 시작하는 함수. DialogueManager 에서 Code를 감지하고 실행하게 됨.
    /// </summary>
    /// <param name="from">호출한 오브젝트</param>
    /// <param name="position">퀴즈 UI 위치값</param>
    /// <param name="id">퀴즈 ID값</param>
    public string Quiz_StartQuiz(DialogueManager from, Vector3 position, int id)
    {
        QuizObject.QuizInfo newQuiz = obj_quizObj.GetQuizInfo(id);

        ui_description.text = newQuiz.question;
        //quizBubble.Write(newQuiz.question, 1f);

        ui_answerText1.text = newQuiz.choice1;
        ui_answerText2.text = newQuiz.choice2;
        ui_answerText3.text = newQuiz.choice3;

        obj_pivot.SetActive(true);
        transform.position = position;
        obj_currentDm = from;

        GameObject.Find("CM vcam1").GetComponent<CameraAssigner>().AssignTarget_UI(transform);
        return newQuiz.correct;
    }

    #region QUIZ


    /// <summary>
    /// 퀴즈를 풀지않고 X를 눌렀을 때 호출하는 함수.
    /// </summary>
    private void Quiz_Quit()
    {
        obj_currentDm.CoolDownTime = 5f;
    }


    #endregion


    #region UI

    /// <summary>
    /// 버튼 클릭 이벤트용 함수. 버튼 UI가 사용하게 될 것임.
    /// </summary>
    /// <param name="buttonId">클릭된 버튼의 아이디 값 (1~4)</param>
    public void UI_ButtonAnswer(int buttonId)
    {
        UI_OffQuizUI();
        GameObject.Find("CM vcam1").GetComponent<CameraAssigner>().AssignTarget_localPlayer();

        switch (buttonId) 
        {
            case 1: case 2: case 3:
                string str = string.Format("#s{0}", buttonId);
                obj_currentDm.printDialogueAfterQuizAnswer(str);
                break;

            case 4: 
                Quiz_Quit(); break;
        }
    }

    /// <summary>
    /// 퀴즈 UI 끄는 함수.
    /// </summary>
    public void UI_OffQuizUI()
    {
        obj_pivot.SetActive(false);
    }

    #endregion
}

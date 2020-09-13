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

    [Header("Variable")]
    [ReadOnly] [SerializeField] private int var_correctNumber;     //퀴즈의 정답 번호


    [Header("UI")]
    [SerializeField] private TMP_Text ui_description;    //퀴즈 내용 
    [SerializeField] private TMP_Text ui_answerText1;    //버튼 1번
    [SerializeField] private TMP_Text ui_answerText2;    //버튼 2번
    [SerializeField] private TMP_Text ui_answerText3;    //버튼 3번

    private DialogueManager obj_currentDm;               //최근에 호출받은 DialogueManager



    /* Purpose: 새로운 퀴즈를 준비하고 시작하는 함수
     * Variable: 호출한 오브젝트, 퀴즈 UI 위치값, 퀴즈 ID값.
     * Notice: DialogueManager 에서 Code를 감지하고 실행하게 됨.
     */
    public void Quiz_StartQuiz(DialogueManager from, Vector3 position, int id)
    {
        QuizObject.QuizInfo newQuiz = obj_quizObj.GetQuizInfo(id);

        ui_description.text = newQuiz.question;
        ui_answerText1.text = newQuiz.choice1;
        ui_answerText2.text = newQuiz.choice2;
        ui_answerText3.text = newQuiz.choice3;
        var_correctNumber = newQuiz.correct;

        obj_pivot.SetActive(true);
        transform.position = position;
        obj_currentDm = from;
    }

    /* Purpose: 퀴즈의 정답을 맞췄을때 호출하는 함수.
     * Variable: 
     * Notice: 
     */
    private void Quiz_Correct()
    {
        Debug.Log("정답!");
    }

    /* Purpose: 퀴즈의 오답을 선택했을때 호출하는 함수.
     * Variable: 
     * Notice: 
     */
    private void Quiz_Incorrect()
    {
        Debug.Log("오답!");
    }


    /* Purpose: 퀴즈를 풀지않고 X를 눌렀을 때 호출하는 함수.
     * Variable: 
     * Notice: 
     */
    private void Quiz_Quit()
    {
        obj_pivot.SetActive(false);
    }


    #region UI

    /* Purpose: 버튼 클릭 이벤트용 함수
     * Variable: 클릭된 버튼의 아이디 값 (1~4)
     * Notice: 버튼 UI가 사용하게 될 것임.
     */
    public void UI_ButtonAnswer(int buttonId)
    {
        switch(buttonId) 
        {
            case 1: case 2: case 3: 
                if(buttonId == var_correctNumber)
                {
                    Quiz_Correct();
                }
                else
                {
                    Quiz_Incorrect();
                }
                break;

            case 4: 
                Quiz_Quit(); break;
        }
    }

    #endregion
}

using System;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour, IInteractable
{
    [SerializeField] private TextBubble textBubble;

    [SerializeField] private DialogueObject dialogueObjs;

    [ReadOnly] [SerializeField] private int currentGroupIndex;
    [ReadOnly] [SerializeField] private int currentIndex;

    [SerializeField] private float typeSpeed;

    public void Interact()
    {
        PrintDialogue();
    }

    private void PrintDialogue()
    {
        var element = dialogueObjs.data[currentGroupIndex];
        if (textBubble.IsWriteOver) {
            if (currentIndex < element.sentence.Count) {
                textBubble.Write(element.sentence[currentIndex++].content, typeSpeed);
            }
            else if(element.sentence[currentIndex - 1].eventCode != "")
            {
                DoEventCode(element.sentence[currentIndex - 1].eventCode);
            }
            else {
                print("End Dialogue");
                // End group dialogue
            }
        }
        else {
            textBubble.writeElement.WriteAllAndDestroy();
        }
    }


    /* Purpose: 이벤트 코드를 판독하고 실행하는 함수.
     * Variable: 이벤트 코드
     * Notice: PrintDialogue() 에서 호출될 것임.
     */
    private void DoEventCode(string eventCode)
    {
        switch(eventCode) 
        {
            case "#q":
                textBubble.gameObject.SetActive(false); //SmokyOnion : TextBubble 에 대한 이해도가 부족하여 디버그용으로 넣은 코드.
                FindObjectOfType<QuizManager>().Quiz_StartQuiz(this, transform.GetChild(0).position, 0); //Smokyonion : 위치값을 수작업으로 찾고 있음. 변수로 등록할 여부도 고려..
                break;

            default: print("Error : 이벤트 코드가 이상합니다. (~DoEventCode())"); return;
        }
    }
}

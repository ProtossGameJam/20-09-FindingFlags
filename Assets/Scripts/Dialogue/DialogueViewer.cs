using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueViewer : InteractModule
{
    [SerializeField] private TextBubble textBubble;
    [SerializeField] private GameObject coolDownObject;

    [ReadOnly] [SerializeField] private List<DialogueData.SentenceElement> sentenceData;
    [ReadOnly] [SerializeField] private int curIndex = 0;
    [ReadOnly] [SerializeField] private string curEventCode = "";

    public EventTimer timer;

    [ReadOnly] public bool isAnsweredDialogue = false;

    private void Awake() {
        if (textBubble == null) {
            textBubble = GetComponentInChildren<TextBubble>();
        }
    }

    private void Start() {
        textBubble.BubbleEnabled = false;
    }

    public void SetDialogue(DialogueData data) {
        sentenceData = data.sentence;
    }

    public void ResetDialogue() {
        curIndex = 0;
        curEventCode = "";
    }

    public override void Interact(params object[] param) {
        if (!isAnsweredDialogue) {
            PrintDialogue();
        }
    }

    private void PrintDialogue() {
        if (timer.isRunning) return;
        
        if (!textBubble.BubbleEnabled) {
            textBubble.BubbleEnabled = true;
        }
        if (textBubble.IsEndWriteLine) {
            if (curEventCode != "") {
                ExecuteEvent(curEventCode);
            }
            else {
                if (curIndex < sentenceData.Count) {
                    curEventCode = sentenceData[curIndex].eventCode;
                    textBubble.Write(sentenceData[curIndex++].content, Constants.DEFAULT_DIALOGUE_TYPE_SPEED);
                }
                else {
                    print("End Dialogue");
                    //textBubble.BubbleEnabled = false;
                }
            }
        }
        else {
            textBubble.writeElement.WriteAllAndDestroy();
        }
    }

    private void ExecuteEvent(string code) {
        Debug.Log($"[DEBUG] Execute : ExecuteEvent() - Quiz Event");
        QuizManager.Instance.StartQuiz(this, code);
    }

    public void PrintEventSentence(string code) {
        var tempSentence = sentenceData.Find(line => string.Equals(line.eventCode, code));
        if (tempSentence != null) {
            textBubble.Write(tempSentence.content, Constants.DEFAULT_DIALOGUE_TYPE_SPEED);
            curIndex = sentenceData.Count;
        }
        else {
            Debug.LogError($"[DEBUG] Execute : PrintEventSentence() - The answer event code is invalid.");
        }
    }
}
using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public enum EventCategoryType
{
    Quiz
}

public class EventManager : MonoSingleton<EventManager>
{
    public enum EventType
    {
        None,
        Quiz,
        MoveIndex,
        ExitDialogue
    }

    [SerializeField] private EventCategoryDictionary eventCategoryDic;

    public void EventExecute(DialogueViewer dialogue, ref string code) {
        var parseResult = ParseEvent(code);
        code = null;
        switch (parseResult.type) {
            case EventType.Quiz:
                Debug.Log("[DEBUG] Execute : LateEventExecute() - Quiz Event");
                QuizManager.Instance.StartQuiz(dialogue, parseResult.code);
                break;
            case EventType.MoveIndex:
                Debug.Log("[DEBUG] Execute : LateEventExecute() - Move Index");
                break;
            case EventType.ExitDialogue:
                Debug.Log("[DEBUG] Execute : LateEventExecute() - Exit Dialogue");
                dialogue.EndDialogue();
                break;
            default:
                Debug.LogWarning("[WARNING] Execute : LateEventExecute() - Event is invalid");
                break;
        }
    }

    private static (EventType type, int code) ParseEvent(string eventCode) {
        var stringObj = eventCode.Split(',');
        foreach (var obj in stringObj) {
            Debug.Log($"* : {obj}");
            var retCode = obj.TrimStart('#');
            var indexCode = retCode.Contains("-") ? Convert.ToInt32(retCode.Split('-')[1]) : -1;
            if (obj.Contains("quiz")) {
                Console.WriteLine($"Quiz Code : <{retCode}-{indexCode}>");
                return (EventType.Quiz, indexCode);
            }

            if (obj.Contains("move")) {
                Console.WriteLine($"Move Index : <{retCode}-{indexCode}>");
                return (EventType.MoveIndex, indexCode);
            }

            if (obj.Contains("exit")) {
                Console.WriteLine($"Exit Dialogue : <{retCode}>");
                return (EventType.ExitDialogue, 0);
            }
        }

        return (EventType.None, -1);
    }

    public static string RemoveAnswerEventCode(string eventCode) {
        var stringObj = eventCode.Split(',');
        string returnCode = null;
        for (var i = 0; i < stringObj.Length; i++) {
            if (!stringObj[i].Contains("#s")) {
                returnCode += stringObj[i];
                if (i < stringObj.Length - 1) {
                    returnCode += ',';
                }
            }
        }

        return returnCode;
    }

    [Serializable]
    public class EventCategoryDictionary : SerializableDictionaryBase<EventCategoryType, string>
    { }
}
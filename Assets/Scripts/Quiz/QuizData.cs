using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Quiz/Quiz Data Create", order = 0)]
public class QuizData : ScriptableObject
{
    public int code;
    [Space(10)] [Multiline] public string desc;
    [Space(5)] public string[] answer;
    [Space(5)] public int correct;
}
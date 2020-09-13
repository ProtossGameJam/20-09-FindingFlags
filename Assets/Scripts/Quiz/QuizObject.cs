using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "Quiz/Quiz Element Create", order = 0)]
public class QuizObject : ScriptableObject
{
    [System.Serializable]
    public class QuizInfo
    {
        public int itemID;
        [Space(10), Multiline]
        public string question;
        [Space(5)]
        public string choice1;
        public string choice2;
        public string choice3;
        [Space(5)]
        public int correct;
    }

    [SerializeField]
    public List<QuizInfo> QuizList;
    public QuizInfo GetQuizInfo(int id)
    {
        return QuizList.Find(info => info.itemID == id);
    }
}
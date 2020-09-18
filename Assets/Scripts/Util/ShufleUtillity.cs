using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ShufleUtillity
{
    public static T[] GetShuffledArray<T>(Array array, int complexity = 1) {
        var tempArray = (T[]) array;
        for (var i = 0; i < complexity; i++) {
            for (var j = 0; j < tempArray.Length; j++) {
                var randomIndex = Random.Range(0, array.Length);
                var tempObj = tempArray[randomIndex];
                tempArray[randomIndex] = tempArray[j];
                tempArray[j] = tempObj;
            }
        }
        return tempArray;
    }
    
    public static List<T> GetShuffledList<T>(List<T> list, int complexity = 1) {
        for (var i = 0; i < complexity; i++) {
            for (var j = 0; j < list.Count; j++) {
                var randomIndex = Random.Range(0, list.Count);
                var tempObj = list[randomIndex];
                list[randomIndex] = list[j];
                list[j] = tempObj;
            }
        }
        return list;
    }
}
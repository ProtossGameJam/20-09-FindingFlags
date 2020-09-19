using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ShufleUtillity
{
    public static T[] GetShuffledArray<T>(T[] array, int complexity = 1) {
        for (var i = 0; i < complexity; i++) {
            for (var j = 0; j < array.Length; j++) {
                var randomIndex = Random.Range(0, array.Length);
                var tempObj = array[randomIndex];
                array[randomIndex] = array[j];
                array[j] = tempObj;
            }
        }

        return array;
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
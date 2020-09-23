using System;
using System.Collections.Generic;

public class ShuffleUtility <T> {
    public static T[] GetShuffledArray(T[] array, int complexity = 1) {
        var rand = new Random();
        for (var i = 0; i < complexity; i++) {
            for (var j = 0; j < array.Length; j++) {
                var randomIndex = rand.Next(array.Length);
                var tempObj = array[randomIndex];
                array[randomIndex] = array[j];
                array[j] = tempObj;
            }
        }

        return array;
    }

    public static List<T> GetShuffledList(List<T> list, int complexity = 1) {
        var rand = new Random();
        for (var i = 0; i < complexity; i++) {
            for (var j = 0; j < list.Count; j++) {
                var randomIndex = rand.Next(list.Count);
                var tempObj = list[randomIndex];
                list[randomIndex] = list[j];
                list[j] = tempObj;
            }
        }

        return list;
    }
}
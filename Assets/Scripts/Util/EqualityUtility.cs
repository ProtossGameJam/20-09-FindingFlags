using System.Collections.Generic;
using UnityEngine;

public static class EqualityUtility
{
    public static bool IsEqualColor(ref Color currentValue, Color newValue)
    {
        if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b &&
            currentValue.a == newValue.a)
            return true;

        currentValue = newValue;
        return false;
    }

    public static bool IsEqualStruct<T>(ref T currentValue, T newValue) where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
            return true;

        currentValue = newValue;
        return false;
    }

    public static bool IsEqualClass<T>(ref T currentValue, T newValue) where T : class
    {
        if (currentValue == null && newValue == null || currentValue != null && currentValue.Equals(newValue))
            return true;

        currentValue = newValue;
        return false;
    }
}
using System;
using Unity.Collections;

public static class NativeArrayExtensions
{
    public static int Find<T>(this NativeArray<T> array, T value) where T : struct, IEquatable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(value)) return i;
        }
        return -1;
    }

    public static bool EqualValues<T>(this NativeArray<T> array, NativeArray<T> other ) where T : struct, IEquatable<T>
    {
        var len = array.Length;
        if (len != other.Length) return false;
        for (int i = 0; i < len; i++)
        {
            if (!array[i].Equals(other[i]))
            {
                return false;
            }
        }
        return true;
    }

    public static void Sort<T>(this NativeList<T> list) where T: struct, IComparable<T>
    {
        list.AsArray().Sort();
    }
}

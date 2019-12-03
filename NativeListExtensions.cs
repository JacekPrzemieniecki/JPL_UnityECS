using System;
using Unity.Collections;
using UnityEngine.Assertions;

namespace JPL.ECS
{
    public static class NativeListExtensions
    {
        public static int Find<T>(this NativeList<T> list, T value) where T : struct, IEquatable<T>
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Equals(value)) return i;
            }
            return -1;
        }

        public static void FindAndRemoveSwapBack<T>(this NativeList<T> list, T value) where T : struct, IEquatable<T>
        {
            int idx = list.Find(value);
            if (idx != -1)
            {
                list.RemoveAtSwapBack(idx);
            }
        }

        public static void RemoveAt<T>(this NativeList<T> list, int idx) where T : struct, IEquatable<T>
        {
            var last = list.Length - 1;
            for (int i = idx; i < last; i++)
            {
                list[i] = list[i + 1];
            }
            list.Resize(last, NativeArrayOptions.UninitializedMemory);
        }

        public static void FindAndRemove<T>(this NativeList<T> list, T value) where T : struct, IEquatable<T>
        {
            int idx = list.Find(value);
            if (idx != -1)
            {
                list.RemoveAt(idx);
            }
        }

        public static bool EqualValues<T>(this NativeList<T> array, NativeArray<T> other) where T : struct, IEquatable<T>
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

        public static T Pop<T>(this NativeList<T> list) where T : struct, IEquatable<T>
        {
            Assert.IsTrue(list.Length > 0, "Tried to Pop() from an empty list");
            var last = list.Length - 1;
            var val = list[last];
            list.Resize(last, NativeArrayOptions.UninitializedMemory);
            return val;
        }

        public static void Sort<T>(this NativeList<T> list) where T : struct, IComparable<T>
        {
            list.AsArray().Sort();
        }
    }
}

using System;
using UnityEngine.Assertions;
using Unity.Entities;

namespace JPL.ECS
{
    public static class DynamicBufferExtensions
    {
        public static int Find<T>(this DynamicBuffer<T> buffer, T value) where T : struct, IBufferElementData, IEquatable<T>
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i].Equals(value)) return i;
            }
            return -1;
        }

        public static void FindAndRemove<T>(this DynamicBuffer<T> buffer, T value) where T : struct, IBufferElementData, IEquatable<T>
        {
            int idx = buffer.Find(value);
            if (idx != -1)
            {
                buffer.RemoveAt(idx);
            }
        }

        public static T Pop<T>(this DynamicBuffer<T> buffer) where T : struct, IBufferElementData
        {
            Assert.IsTrue(buffer.Length > 0, "Tried to Pop() from an empty buffer");
            var last = buffer.Length - 1;
            var result = buffer[last];
            buffer.RemoveAt(last);
            return result;
        }
    }
}

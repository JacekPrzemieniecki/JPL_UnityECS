using System.Diagnostics;
using Unity.Collections;
using Unity.Entities;

namespace JPL.ECS
{
    public abstract class SingleThreadSystem : ComponentSystem
    {
        protected DynamicBuffer<T> GetBuffer<T>(Entity entity) where T : struct, IBufferElementData
            => EntityManager.GetBuffer<T>(entity);
        protected T Get<T>(Entity entity) where T : struct, IComponentData
            => EntityManager.GetComponentData<T>(entity);
        protected T GetObject<T>(Entity entity)
            => EntityManager.GetComponentObject<T>(entity);

        protected void Set<T>(Entity entity, T value) where T : struct, IComponentData
            => EntityManager.SetComponentData(entity, value);

        protected DynamicBuffer<T> AddBuffer<T>(Entity entity) where T : struct, IBufferElementData
            => EntityManager.AddBuffer<T>(entity);
        protected void Add<T>(Entity entity, T value) where T : struct, IComponentData
            => EntityManager.AddComponentData(entity, value);
        protected void Add<T>(Entity entity) where T : struct, IComponentData
            => EntityManager.AddComponentData(entity, new T());
        protected void Add<T>(NativeArray<Entity> entities) where T : struct, IComponentData
        {
            int len = entities.Length;
            for (int i = 0; i < len; i++)
            {
                Add<T>(entities[i]);
            }
        }

        protected void Remove<T>(Entity entity) where T : struct, IComponentData
            => EntityManager.RemoveComponent<T>(entity);
        protected void Remove<T>(NativeArray<Entity> entities) where T : struct, IComponentData
            => EntityManager.RemoveComponent<T>(entities);

        protected bool Has<T>(Entity entity) where T : struct, IComponentData
            => EntityManager.HasComponent<T>(entity);


        [Conditional("UNITY_EDITOR")]
        protected void SetDebugName(Entity e, string name)
        {
#if UNITY_EDITOR
            EntityManager.SetName(e, name);
#endif
        }
    }
}

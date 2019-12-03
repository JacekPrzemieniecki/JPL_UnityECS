using System.Diagnostics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace JPL.ECS
{
    [AlwaysSynchronizeSystem]
    public abstract class SingleThreadSystem : JobComponentSystem
    {
        protected virtual void OnUpdate() { }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            OnUpdate();
            return default;
        }

        protected DynamicBuffer<T> GetBuffer<T>(Entity entity) where T : struct, IBufferElementData
            => EntityManager.GetBuffer<T>(entity);
        protected T Get<T>(Entity entity) where T : struct, IComponentData
            => EntityManager.GetComponentData<T>(entity);
        protected T GetObject<T>(Entity entity)
            => EntityManager.GetComponentObject<T>(entity);

        protected void Set<T>(Entity entity, T value) where T : struct, IComponentData
            => EntityManager.SetComponentData(entity, value);

        protected Entity CreateEntity()
            => EntityManager.CreateEntity();
        protected Entity CreateEntity<T>(T value) where T : struct, IComponentData
        {
            var e = EntityManager.CreateEntity(
                ComponentType.ReadWrite<T>());
            Set(e, value);
            return e;
        }
        protected Entity CreateEntity<T0, T1>(T0 value0, T1 value1)
            where T0 : struct, IComponentData
            where T1 : struct, IComponentData
        {
            var e = EntityManager.CreateEntity(
                ComponentType.ReadWrite<T0>(),
                ComponentType.ReadWrite<T1>());
            Set(e, value0);
            Set(e, value1);
            return e;
        }
        protected Entity CreateEntity<T0, T1, T2>(T0 value0, T1 value1, T2 value2)
            where T0 : struct, IComponentData
            where T1 : struct, IComponentData
            where T2 : struct, IComponentData
        {
            var e = EntityManager.CreateEntity(
                ComponentType.ReadWrite<T0>(),
                ComponentType.ReadWrite<T1>(),
                ComponentType.ReadWrite<T2>());
            Set(e, value0);
            Set(e, value1);
            Set(e, value2);
            return e;
        }

        protected NativeArray<Entity> Instantiate(NativeArray<Entity> prefabs, Allocator allocator)
        {
            var len = prefabs.Length;
            var result = new NativeArray<Entity>(len, allocator);
            for (int i = 0; i < len; i++)
            {
                result[i] = EntityManager.Instantiate(prefabs[i]);
            }
            return result;
        }

        protected void Destroy(Entity e)
            => EntityManager.DestroyEntity(e);

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

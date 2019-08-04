using Unity.Entities;
using Unity.Jobs;

namespace JPL.ECS
{
    public abstract class JobComponentSystemWithBarrier<TBarrier> : JobComponentSystem
            where TBarrier : EntityCommandBufferSystem
    {
        TBarrier _barrier;
        protected sealed override void OnCreate()
        {
            _barrier = World.GetOrCreateSystem<TBarrier>();
            OnCreate(_barrier);
        }
        protected virtual void OnCreate(TBarrier barrier) { }
        protected sealed override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var handle = OnUpdate(inputDeps, _barrier);
            _barrier.AddJobHandleForProducer(handle);
            return handle;
        }

        protected abstract JobHandle OnUpdate(JobHandle inputDeps, TBarrier barrier);
    }
}

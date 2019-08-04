using Unity.Entities;
using Unity.Jobs;

namespace JPL.ECS
{
    public abstract class JobComponentSystemWithBarrier<TBarrier> : JobComponentSystem
            where TBarrier : EntityCommandBufferSystem
    {
        TBarrier _barrier;
        protected sealed override void OnCreateManager()
        {
            _barrier = World.GetOrCreateSystem<TBarrier>();
            OnCreateManager(_barrier);
        }
        protected virtual void OnCreateManager(TBarrier barrier) { }
        protected sealed override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var handle = OnUpdate(inputDeps, _barrier);
            _barrier.AddJobHandleForProducer(handle);
            return handle;
        }

        protected abstract JobHandle OnUpdate(JobHandle inputDeps, TBarrier barrier);
    }
}

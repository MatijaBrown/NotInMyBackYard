using System;

namespace NIMBY.States
{
    public interface IState : IDisposable
    {

        public StateManager Manager { get; }

        void Init(StateManager manager);

        void Start();

        void Update(float delta);

        void Render();

        void Stop();

    }
}

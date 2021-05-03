using System;
using System.Collections.Generic;

namespace NIMBY.States
{
    public class StateManager : IDisposable
    {

        private readonly IDictionary<string, IState> _states = new Dictionary<string, IState>();
        private readonly Game _game;

        private string _current = null;

        public string CurrentState => _current;

        public Game Game => _game;

        public StateManager(Game game)
        {
            _game = game;
        }

        public void AddState(string name, IState state)
        {
            _states.Add(name, state);
            state.Init(this);

            if (_current == null)
            {
                SetState(name);
            }
        }

        public void SetState(string name)
        {
            if (_current != null)
                _states[_current].Stop();

            _current = name;
            _states[_current].Start();
        }

        public IState GetState(string name)
        {
            if (_states.ContainsKey(name))
            {
                return _states[name];
            }
            else
                return null;
        }

        public void Update(float delta)
        {
            _states[_current].Update(delta);
        }

        public void Render()
        {
            _states[_current].Render();
        }

        public void Dispose()
        {
            _states[_current].Stop();
            foreach (IState state in _states.Values)
            {
                state.Dispose();
            }
        }

    }
}

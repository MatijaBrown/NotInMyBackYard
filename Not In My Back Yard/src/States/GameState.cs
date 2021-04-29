using NIMBY.World;

namespace NIMBY.States
{
    public class GameState : IState
    {

        private StateManager _manager;
        private Level _level;

        public string LevelName { get; set; } = "level1";

        public Level Level => _level;

        public StateManager Manager => _manager;

        public void Init(StateManager manager)
        {
            _manager = manager;
        }

        public void Start()
        {
            _level = new Level(LevelName, this);

            _manager.Game.Camera.MaxXDistance = _level.PixelWidth;
            _manager.Game.Camera.MaxYDistance = _level.PixelHeight;
        }

        public void Update(float delta)
        {
            _level.Update();
        }

        public void Render()
        {
            _level.Render(_manager.Game.Camera);
        }

        public void Stop()
        {
            ResourceManager.Clear();
        }

        public void Dispose()
        {

        }

    }
}

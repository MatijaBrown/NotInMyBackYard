using NIMBY.Graphics;
using NIMBY.World;

namespace NIMBY.States
{
    public class GameState : IState
    {

        private StateManager _manager;
        private Level _level;

        private GameStateRenderer _renderer;

        public string LevelName { get; set; } = "level2";

        public Level Level => _level;

        public StateManager Manager => _manager;

        public GameStateRenderer Renderer => _renderer;

        public void Init(StateManager manager)
        {
            _manager = manager;
        }

        public void Start()
        {
            _level = new Level(LevelName, this);
            _manager.Game.Camera.MaxXDistance = _level.PixelWidth;
            _manager.Game.Camera.MaxYDistance = _level.PixelHeight;

            _renderer = new GameStateRenderer(this);
        }

        public void Update(float delta)
        {
            _level.Update();
        }

        public void Render()
        {
            _level.Render();
            _renderer.Render(_manager.Game.Camera);
        }

        public void Stop()
        {
            ResourceManager.Clear();

            _manager.Game.Camera.MaxXDistance = 0;
            _manager.Game.Camera.MaxYDistance = 0;
        }

        public void Dispose()
        {

        }

    }
}

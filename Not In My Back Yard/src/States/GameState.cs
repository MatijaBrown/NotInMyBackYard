using FontStash.NET;
using NIMBY.Graphics;
using NIMBY.Tiles;
using NIMBY.Ui;
using NIMBY.Utils;
using NIMBY.World;
using System;

namespace NIMBY.States
{
    public class GameState : IState
    {

        private StateManager _manager;
        private Level _level;

        private GameStateRenderer _renderer;

        private Button _resetButton, _finishButton;
        private Texture _reset, _finish, _star;

        private int _stars = 0;
        private bool _completed = false;

        public string LevelName { get; set; } = "level1";

        public Level Level => _level;

        public StateManager Manager => _manager;

        public GameStateRenderer Renderer => _renderer;

        public void Init(StateManager manager)
        {
            _manager = manager;
        }

        public void Start()
        {
            TileTexture.Init();
            _level = new Level(this);
            _manager.Game.Camera.MaxXDistance = _level.PixelWidth / 2.0f;
            _manager.Game.Camera.MaxYDistance = _level.PixelHeight / 2.0f;
            _manager.Game.Camera.CanZoom = true;

            _renderer = new GameStateRenderer(this);

            _finishButton = new Button(1150.0f, 50.0f, 100.0f, 100.0f, Finish);
            _resetButton = new Button(1150.0f, 175.0f, 100.0f, 100.0f, Reset);
            _reset = ResourceManager.LoadTexture("Reset_Button");
            _finish = ResourceManager.LoadTexture("Finish_Button");
            _star = ResourceManager.LoadTexture("Sun");
        }

        private void Finish()
        {
            if (_level.PlacedTurbines == _level.MaxTurbines && !_completed)
            {
                _manager.Game.Camera.MaxXDistance = 0;
                _manager.Game.Camera.MaxYDistance = 0;
                _manager.Game.Camera.CanZoom = false;
                _manager.Game.Camera.Zoom = Camera.MIN_ZOOM;

                _completed = true;
                _level.Update();

                _stars = (int)Math.Floor(5.0f * Math.Floor(_level.CurrentOutput) / _level.MaxOutput);
            } else if (_completed && _stars >= 3)
            {
                System.Console.WriteLine("Reset");
                // go to selection state
            }
        }

        private void Reset()
        {
            if (_completed)
            {
                _manager.Game.Camera.MaxXDistance = _level.PixelWidth / 2.0f;
                _manager.Game.Camera.MaxYDistance = _level.PixelHeight / 2.0f;
                _manager.Game.Camera.Zoom = Camera.MIN_ZOOM;
                _manager.Game.Camera.CanZoom = true;
                _completed = false;
            }

            _level.Reset();
        }

        public void Update(float delta)
        {
            _finishButton.Update();
            _resetButton.Update();

            if (!_completed)
            {
                if (!_finishButton.Hovering && !_resetButton.Hovering)
                    _level.Hovering = false;
                else
                    _level.Hovering = true;

                _level.Update();
            }
        }

        public void Render()
        {
            _level.Render();
            _renderer.Render(_manager.Game.Camera);

            _renderer.RenderTexturedQuad(_finish, _finishButton.X, _finishButton.Y + _finishButton.Height, _finishButton.Width, -_finishButton.Height);
            _renderer.RenderTexturedQuad(_reset, _resetButton.X, _resetButton.Y + _resetButton.Height, _resetButton.Width, -_resetButton.Height);

            if (_completed)
            {
                _renderer.PrepareLegacy();
                var fons = _manager.Game.Fons;

                fons.SetFont(fons.GetFontByName("stdfont"));
                fons.SetColour(0xFFFF0000);
                fons.SetSize(86.0f);
                fons.SetAlign((int)FonsAlign.Center | (int)FonsAlign.Middle);
                fons.DrawText(_manager.Game.Witdh / 2.0f + 156, _manager.Game.Height / 2.0f, "Producing: " + Math.Floor(_level.CurrentOutput).ToString() + "MJ / " + _level.MaxOutput.ToString() + "MJ");

                _renderer.EndLegacy();

                float size = 156;
                float x = 50.0f;
                float y = 250.0f;

                for (int i = 0; i < _stars; i++)
                {
                    _renderer.RenderTexturedQuad(_star, x, y + i * size, size, -size);
                }

            }
        }

        public void Stop()
        {
            Input.OnMouseReleased = null;
            Input.OnKeyReleased = null;
            ResourceManager.Clear();

            _manager.Game.Camera.MaxXDistance = 0;
            _manager.Game.Camera.MaxYDistance = 0;

            System.GC.Collect();

            _manager.Game.Camera.CanZoom = false;
        }

        public void Dispose()
        {

        }

    }
}

using FontStash.NET;
using FontStash.NET.GL.Legacy;
using NIMBY.Graphics;
using NIMBY.Ui;

namespace NIMBY.States
{
    public class MenuState : IState
    {

        private StateManager _manager;

        private MenuStateRenderer _renderer;
        private GLFons _glFons;
        private Fontstash _fons;

        private Texture _background;
        private Texture _buttonBackground;

        private Button _playButton, _settingsButton, _creditsButton, _quitButton;

        public StateManager Manager => _manager;

        public MenuStateRenderer Renderer => _renderer;

        public void Init(StateManager manager)
        {
            _manager = manager;
        }

        public void Start()
        {
            _renderer = new MenuStateRenderer(this);
            _background = ResourceManager.LoadTexture("title_everything");
            _buttonBackground = ResourceManager.LoadTexture("brushstroke_thing");

            _playButton = new Button(800, 300, 300, 75, () => System.Console.WriteLine("Play"));
            _settingsButton = new Button(850, 400, 300, 75, () => System.Console.WriteLine("Settings"));
            _creditsButton = new Button(900, 500, 300, 75, () => System.Console.WriteLine("Credits"));
            _quitButton = new Button(950, 600, 300, 75, () => System.Environment.Exit(0));

            _glFons = new GLFons(_manager.Game.LegacyGl);
            _fons = _glFons.Create(512, 512, (int)FonsFlags.ZeroTopleft);
            _fons.AddFont("stdfont", "./Assets/Fonts/DroidSerif-Regular.ttf");
        }

        public void Update(float delta)
        {
            _playButton.Update();
            _settingsButton.Update();
            _creditsButton.Update();
            _quitButton.Update();
        }

        public void Render()
        {
            var game = _manager.Game;

            _renderer.RenderTexturedQuad(_background, 0, game.Height, game.Witdh, -game.Height);

            _renderer.RenderTexturedQuad(_buttonBackground, _playButton.X, _playButton.Y + _playButton.Height, _playButton.Width, -_playButton.Height);
            _renderer.RenderTexturedQuad(_buttonBackground, _settingsButton.X, _settingsButton.Y + _settingsButton.Height, _settingsButton.Width, -_settingsButton.Height);
            _renderer.RenderTexturedQuad(_buttonBackground, _creditsButton.X, _creditsButton.Y + _creditsButton.Height, _creditsButton.Width, -_creditsButton.Height);
            _renderer.RenderTexturedQuad(_buttonBackground, _quitButton.X, _quitButton.Y + _quitButton.Height, _quitButton.Width, -_quitButton.Height);

            _renderer.PrepareLegacy();
            _fons.SetFont(_fons.GetFontByName("stdfont"));
            _fons.SetSize(36.0f);
            _fons.SetAlign((int)FonsAlign.Middle | (int)FonsAlign.Center);

            _fons.SetColour(_playButton.Hovering ? 0xFF00FFFF : 0xFFFFFFFF);
            _fons.DrawText(_playButton.X + _playButton.Width / 2.0f, _playButton.Y + _playButton.Height / 2.0f, "Play");
            _fons.SetColour(_settingsButton.Hovering ? 0xFF00FFFF : 0xFFFFFFFF);
            _fons.DrawText(_settingsButton.X + _settingsButton.Width / 2.0f, _settingsButton.Y + _settingsButton.Height / 2.0f, "Settings");
            _fons.SetColour(_creditsButton.Hovering ? 0xFF00FFFF : 0xFFFFFFFF);
            _fons.DrawText(_creditsButton.X + _creditsButton.Width / 2.0f, _creditsButton.Y + _creditsButton.Height / 2.0f, "Credits");
            _fons.SetColour(_quitButton.Hovering ? 0xFF00FFFF : 0xFFFFFFFF);
            _fons.DrawText(_quitButton.X + _quitButton.Width / 2.0f, _quitButton.Y + _quitButton.Height / 2.0f, "Quit");

            _renderer.EndLegacy();
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

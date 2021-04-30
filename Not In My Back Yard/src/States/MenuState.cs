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

            _playButton = new Button(700.0f, 500.0f, 300.0f, 200.0f, () => System.Console.WriteLine("Play"));
            _settingsButton = new Button(750.0f, 425.0f, 300.0f, 200.0f, () => System.Console.WriteLine("Settings"));
            _creditsButton = new Button(800.0f, 325.0f, 300.0f, 200.0f, () => System.Console.WriteLine("Credits"));
            _quitButton = new Button(850.0f, 250.0f, 300.0f, 200.0f, () => System.Console.WriteLine("Quit"));

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

            _renderer.RenderTexturedQuad(_background, 0, 0, game.Witdh, game.Height);

            _renderer.RenderTexturedQuad(_buttonBackground, _playButton.X, _playButton.Y, _playButton.Width, _playButton.Height);
            _renderer.RenderTexturedQuad(_buttonBackground, _settingsButton.X, _settingsButton.Y, _settingsButton.Width, _settingsButton.Height);
            _renderer.RenderTexturedQuad(_buttonBackground, _creditsButton.X, _creditsButton.Y, _creditsButton.Width, _creditsButton.Height);
            _renderer.RenderTexturedQuad(_buttonBackground, _quitButton.X, _quitButton.Y, _quitButton.Width, _quitButton.Height);

            game.LegacyGl.Enable(Silk.NET.OpenGL.Legacy.GLEnum.Blend);
            game.LegacyGl.BlendFunc(Silk.NET.OpenGL.Legacy.GLEnum.SrcAlpha, Silk.NET.OpenGL.Legacy.GLEnum.OneMinusSrcAlpha);
            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.GLEnum.Texture2D);
            game.LegacyGl.MatrixMode(Silk.NET.OpenGL.Legacy.GLEnum.Projection);
            game.LegacyGl.LoadIdentity();
            game.LegacyGl.Ortho(0, game.Witdh, game.Height, 0, -1, 1);

            game.LegacyGl.MatrixMode(Silk.NET.OpenGL.Legacy.GLEnum.Modelview);
            game.LegacyGl.LoadIdentity();
            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.GLEnum.DepthTest);
            game.LegacyGl.Color4(255, 255, 255, 255);
            game.LegacyGl.Enable(Silk.NET.OpenGL.Legacy.GLEnum.Blend);
            game.LegacyGl.BlendFunc(Silk.NET.OpenGL.Legacy.GLEnum.SrcAlpha, Silk.NET.OpenGL.Legacy.GLEnum.OneMinusSrcAlpha);
            game.LegacyGl.Enable(Silk.NET.OpenGL.Legacy.GLEnum.CullFace);

            _fons.SetFont(_fons.GetFontByName("stdfont"));
            _fons.SetColour(0xFF0000FF);
            _fons.DrawText(100.0f, 200.0f, "I am the walrus!");

            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.EnableCap.Blend);
            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.EnableCap.CullFace);
            game.LegacyGl.Flush();
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

using FontStash.NET;
using FontStash.NET.GL.Legacy;
using LevelEditor.Interface;
using Silk.NET.GLFW;
using Silk.NET.OpenGL.Legacy;
using System;
using Wind_Thing.Resources;

namespace Wind_Thing
{
    public unsafe class Game : IDisposable
    {

        private readonly int _width, _height;
        private readonly string _title;

        private readonly Glfw _glfw;
        private WindowHandle* _window;
        private GL _gl;

        private World.World _world;
        private Ui _ui;

        private GLFons _glFons;
        private Fontstash _fons;

        public Fontstash Fons => _fons;

        public World.World World => _world;

        public Ui Ui => _ui;

        public Game(int width, int height, string title)
        {
            _width = width;
            _height = height;
            _title = title;

            _glfw = Glfw.GetApi();
            _glfw.Init();
        }

        private void Init()
        {
            _gl = GL.GetApi(new GlfwContext(_glfw, _window));
            Assets.Init(_gl);
            Input.Init(_glfw, _window);

            _glFons = new GLFons(_gl);
            _fons = _glFons.Create(512, 512, (int)FonsFlags.ZeroTopleft);
            _fons.AddFont("stdfont", "./fonts/DroidSerif-Regular.ttf");

            _world = new World.World(this);
            _ui = new Ui(this);
        }

        private void Update()
        {
            _ui.Update();
            _world.Update();
        }

        private void Render()
        {
            _gl.Viewport(0, 0, (uint)_width, (uint)_height);
            _gl.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            _gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            _gl.MatrixMode(MatrixMode.Projection);
            _gl.LoadIdentity();
            _gl.Ortho(0, _width, _height, 0, -1, 1);

            _gl.MatrixMode(MatrixMode.Modelview);
            _gl.LoadIdentity();

            _world.Render(_gl);
            _ui.Render(_gl);

            _gl.Flush();
            _glfw.SwapBuffers(_window);
            _glfw.PollEvents();
        }

        private void Close()
        {
            Assets.Dispose();
            _glFons.Dispose();
        }

        public void Run()
        {
            _glfw.WindowHint(WindowHintBool.Resizable, false);
            _window = _glfw.CreateWindow(_width, _height, _title, null, null);
            _glfw.MakeContextCurrent(_window);
            Init();
            while (!_glfw.WindowShouldClose(_window))
            {
                Update();
                Render();
            }
            Close();
            _glfw.Terminate();
        }

        public void Dispose()
        {

        }

    }
}

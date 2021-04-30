using NIMBY.States;
using NIMBY.Utils;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using System;

namespace NIMBY
{
    public unsafe class Game : IDisposable
    {

        private readonly string _title;
        private WindowHandle* _window;
        private uint _width, _height;

        private readonly StateManager _stateManager;

        private GL _gl;
        private Silk.NET.OpenGL.Legacy.GL _legacyGl;
        private Glfw _glfw;

        private Camera _camera;

        public uint Witdh => _width;

        public uint Height => _height;

        public GL Gl => _gl;

        public Silk.NET.OpenGL.Legacy.GL LegacyGl => _legacyGl;

        public Glfw Glfw => _glfw;

        public Camera Camera => _camera;

        public Game(uint width, uint height, string title)
        {
            _width = width;
            _height = height;
            _title = title;

            _stateManager = new StateManager(this);
            _glfw = Glfw.GetApi();
        }

        private void Init()
        {
            var context = new GlfwContext(_glfw, _window);
            _gl = GL.GetApi(context);
            _legacyGl = Silk.NET.OpenGL.Legacy.GL.GetApi(context);

            ResourceManager.Init(_gl);
            Input.Init(_glfw, _window);

            _camera = new Camera(0.0f, 0.0f, 0.0f, 0.0f);

            _stateManager.AddState("Game", new GameState());
            _stateManager.AddState("Main Menu", new MenuState());
            _stateManager.SetState("Main Menu");
        }

        private void Update(double d)
        {
            _camera.Update((float)d);
            _stateManager.Update((float)d);

            Input.Update();
        }

        private void Render()
        {
            _gl.Viewport(0, 0, _width, _height);
            _gl.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            _gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            _stateManager.Render();
        }

        private void Close()
        {
            _stateManager.Dispose();
        }

        public void Dispose()
        {

        }

        public void Run()
        {
            _glfw.Init();
            _glfw.WindowHint(WindowHintBool.Resizable, false);
            _window = _glfw.CreateWindow((int)_width, (int)_height, _title, null, null);

            _glfw.MakeContextCurrent(_window);

            Init();

            while (!_glfw.WindowShouldClose(_window))
            {
                Update(0);
                Render();
                _glfw.SwapBuffers(_window);
                _glfw.PollEvents();
            }

            _glfw.Terminate();

        }
    }
}

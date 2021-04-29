using NIMBY.States;
using NIMBY.Utils;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;

namespace NIMBY
{
    public class Game : IDisposable
    {

        private readonly string _title;
        private IWindow _window;
        private uint _width, _height;

        private readonly StateManager _stateManager;

        private GL _gl;
        private Camera _camera;

        public uint Witdh => _width;

        public uint Height => _height;

        public GL Gl => _gl;

        public Camera Camera => _camera;

        public Game(uint width, uint height, string title)
        {
            _width = width;
            _height = height;
            _title = title;

            _stateManager = new StateManager(this);
        }

        private void Init()
        {
            _window.Center();
            _gl = GL.GetApi(_window);

            ResourceManager.Init(_gl);
            Input.Init(_window.CreateInput());

            _camera = new Camera(0.0f, 0.0f, 0.0f, 0.0f);

            _stateManager.AddState("GameState", new GameState());
        }

        private void Update(double d)
        {
            _width = (uint)_window.Size.X;
            _height = (uint)_window.Size.Y;

            _camera.Update((float)d);
            _stateManager.Update((float)d);

            Input.Update();
        }

        private void Render(double _)
        {
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
            var ops = WindowOptions.Default;
            ops.Size = new Vector2D<int>((int)_width, (int)_height);
            ops.Title = _title;
            ops.FramesPerSecond = 0;
            ops.UpdatesPerSecond = 60;
            ops.VSync = true;
            ops.WindowBorder = WindowBorder.Fixed;
            _window = Window.Create(ops);
            _window.Load += Init;
            _window.Update += Update;
            _window.Render += Render;
            _window.Closing += Close;
            _window.Run();
        }

    }
}

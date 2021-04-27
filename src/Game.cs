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

        private GL _gl;

        public uint Witdh => _width;

        public uint Height => _height;

        public GL Gl => _gl;

        public Game(uint width, uint height, string title)
        {
            _width = width;
            _height = height;
            _title = title;
        }

        private void Init()
        {
            _window.Center();
        }

        private void Update(double d)
        {
            _width = (uint)_window.Size.X;
            _height = (uint)_window.Size.Y;

            _gl = GL.GetApi(_window);
            ResourceManager.Init(_gl);
        }

        private void Render(double _)
        {
            
        }

        private void Close()
        {
            ResourceManager.Clear();
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
            _window = Window.Create(ops);
            _window.Load += Init;
            _window.Update += Update;
            _window.Render += Render;
            _window.Closing += Close;
            _window.Run();
        }

    }
}

using System;

namespace NIMBY.Ui
{
    public class Button
    {

        private readonly float _x, _y;
        private readonly float _width, _height;

        private bool _hovering;
        private Action _onClick;

        public float X => _x;

        public float Y => _y;

        public float Width => _width;

        public float Height => _height;

        public bool Hovering => _hovering;

        public Button(float x, float y, float width, float height, Action onClick)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _onClick = onClick;
        }

        public void Update()
        {
            if (Input.MouseX > _x && Input.MouseY > _y && Input.MouseX < _x + _width && Input.MouseY < _y + _height)
            {
                _hovering = true;
            }
            else
            {
                _hovering = false;
            }

            if (_hovering && Input.WasMouseButtonReleased(Silk.NET.Input.MouseButton.Left))
            {
                _onClick.Invoke();
            }
        }

    }
}

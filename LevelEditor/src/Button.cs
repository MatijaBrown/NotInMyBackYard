using FontStash.NET;
using System;

namespace Wind_Thing
{
    public class Button : IDisposable
    {

        private readonly float _x, _y;
        private readonly float _width, _height;
        private readonly Action _onClick;
        private readonly string _text;

        private bool _hovering;

        public bool Enabled { get; set; }

        public Button(string text, float x, float y, float width, float height, Action onClick)
        {
            _text = text;
            _x = x;
            _y = y;
            _width = width;
            _height = height;

            _onClick = onClick;
            Input.OnLeftDown += Click;

            _hovering = false;
        }

        private void Click(float _, float __)
        {
            if (_hovering && Enabled)
                _onClick.Invoke();
            Enabled = false;
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
        }

        public void Render(Fontstash fons)
        {
            if (_hovering)
                fons.SetColour(0xFF00FFFF);
            else
                fons.SetColour(0xFF000000);

            fons.SetFont(fons.GetFontByName("stdfont"));
            fons.SetSize(18.0f);
            fons.DrawText(_x + 2.0f, _y + _height, _text);
        }

        public void Dispose()
        {
            Input.OnLeftDown -= Click;
        }

    }
}

using FontStash.NET;
using Silk.NET.GLFW;
using Silk.NET.OpenGL.Legacy;
using System;

namespace Wind_Thing
{
    public class TextInput
    {

        private readonly float _x, _y;
        private readonly float _width, _height;

        private readonly int _fontSize;
        private readonly int _maxLength;
        private readonly string _font;

        private readonly Fontstash _fons;
        private readonly string _name;

        private readonly Action _onChange;

        private bool _selected = false;
        private string _text = "0";
        private bool _changed = false;

        public string Text => _text;

        public TextInput(float x, float y, float width, float height, int fontSize, int maxLength, string name, string font, Fontstash fons, Action onChange)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _fontSize = fontSize;
            _maxLength = maxLength;
            _font = font;
            _fons = fons;
            _name = name;
            _onChange = onChange;

            Input.OnLeftDown += Click;
            Input.OnChar += Write;
            Input.OnKeyDown += Key;
        }

        private void Key(Keys key)
        {
            if (key == Keys.Backspace && _text.Length > 0 && _selected)
            {
                _text = _text.Remove(_text.Length - 1);
            }
        }

        private void Write(string letter)
        {
            if (_selected && (_text.Length < _maxLength || _maxLength == -1))
            {
                _text += letter;
                _changed = true;
            }
        }

        private void Click(float x, float y)
        {
            if (x > _x && y > _y && x < _x + _width && y < _y + _height)
            {
                _selected = true;
            }
            else
            {
                if (_selected)
                {
                    _selected = false;
                    if (_changed)
                    {
                        _changed = false;
                        _onChange.Invoke();
                    }
                }
            }
        }

        public void Render(GL gl)
        {
            gl.Begin(PrimitiveType.Lines);
            if (_selected)
                gl.Color4(0.0f, 1.0f, 0.0f, 1.0f);
            else
                gl.Color4(0.0f, 0.0f, 0.0f, 1.0f);
            gl.Vertex2(_x, _y);
            gl.Vertex2(_x + _width, _y);
            gl.Vertex2(_x, _y);
            gl.Vertex2(_x , _y + _height);
            gl.Vertex2(_x + _width, _y);
            gl.Vertex2(_x + _width, _y + _height);
            gl.Vertex2(_x, _y + _height);
            gl.Vertex2(_x + _width, _y + _height);
            gl.End();

            _fons.SetFont(_fons.GetFontByName(_font));
            _fons.SetSize((float)_fontSize);
            _fons.SetColour(0xFF000000);
            _fons.DrawText(_x + 1.0f, _y + _height - 3.0f, _name + ": " + _text);
        }

    }
}

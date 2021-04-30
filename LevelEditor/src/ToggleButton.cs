using FontStash.NET;
using Wind_Thing;

namespace LevelEditor
{
    public class ToggleButton
    {

        private readonly float _x, _y;
        private readonly float _width, _height;
        private readonly string _text, _activeText;

        private bool _hovering;
        private bool _active;

        public bool Active => _active;

        public ToggleButton(string text, string activeText, float x, float y, float width, float height)
        {
            _text = text;
            _activeText = activeText;
            _x = x;
            _y = y;
            _width = width;
            _height = height;

            Input.OnLeftDown += Click;

            _hovering = false;
        }

        private void Click(float _, float __)
        {
            if (_hovering)
                _active = !_active;
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
            if (_active)
                fons.SetColour(0xFF00FFFF);
            else
                fons.SetColour(0xFF000000);

            fons.SetFont(fons.GetFontByName("stdfont"));
            fons.SetSize(18.0f);
            fons.DrawText(_x + 2.0f, _y + _height, _active ? _activeText : _text);
        }

    }
}

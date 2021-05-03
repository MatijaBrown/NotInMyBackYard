using FontStash.NET;
using NIMBY.Graphics;
using NIMBY.States;
using NIMBY.World;
using System.Numerics;

namespace NIMBY.Ui
{
    public class LevelSelectButton
    {

        private readonly Button _button;
        private readonly Vector3 _colour;
        private readonly LevelData _data;
        private readonly LevelSelectorState _state;

        public bool Hovering => _button.Hovering;

        public LevelSelectButton(float x, float y, Vector3 colour, LevelData data, LevelSelectorState state)
        {
            _colour = colour;
            _data = data;
            _state = state;
            _button = new Button(x, y, 200.0f, 200.0f, () =>
            {
                GameState gameState = (GameState)_state.Manager.GetState("Game");
                gameState.LevelMeta = _data;
                _state.Manager.SetState("Game");
            });
        }

        public void Update()
        {
            _button.Update();
        }

        public void Render(LevelSelectorStateRenderer renderer, Fontstash fons)
        {
            renderer.RenderQuad(_button.X, _button.Y, _button.Width, _button.Height, new Vector4(_colour, Hovering ? 1.0f : 0.75f));

            float size = 32;
            float x = _button.X + size / 1.5f;
            float y = _button.Y + size * 1.5f;

            for (int i = 0; i < _data.bestRating; i++)
            {
                renderer.RenderTexturedQuad(_state.Star, x + i * size, y, size, -size);
            }

            renderer.PrepareLegacy();
            fons.SetColour(0xFFFFFFFF);
            fons.SetFont(fons.GetFontByName("stdfont"));
            fons.SetSize(24.0f);
            fons.SetAlign((int)FonsAlign.Center | (int)FonsAlign.Middle);
            fons.DrawText(_button.X + _button.Width / 2.0f, _button.Y + _button.Height / 2.0f, _data.name);
            fons.SetSize(16.0f);
            fons.DrawText(_button.X + _button.Width / 2.0f, _button.Y + _button.Height / 2.0f + 35.0f, "Last Time: " + _data.bestTime.ToString());
            renderer.EndLegacy();
        }

    }
}

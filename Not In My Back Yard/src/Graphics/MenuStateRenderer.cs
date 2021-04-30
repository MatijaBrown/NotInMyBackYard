using NIMBY.Graphics.Renderers;
using NIMBY.States;
using Silk.NET.OpenGL;

namespace NIMBY.Graphics
{
    public class MenuStateRenderer
    {

        private readonly MenuState _state;
        private readonly GL _gl;

        private readonly TextureQuadRenderer _texturedQuadRenderer;

        public GL Gl => _gl;

        public MenuState State => _state;

        public MenuStateRenderer(MenuState state)
        {
            _state = state;
            _gl = state.Manager.Game.Gl;

            _texturedQuadRenderer = new TextureQuadRenderer(this);
        }

        public void RenderTexturedQuad(Texture texture, float x, float y, float width, float height)
        {
            _texturedQuadRenderer.Render(texture, x, y, width, height);
        }

    }
}

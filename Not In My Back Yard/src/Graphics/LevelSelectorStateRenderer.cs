using NIMBY.Graphics.Renderers;
using NIMBY.States;
using Silk.NET.OpenGL;
using System.Numerics;

namespace NIMBY.Graphics
{
    public class LevelSelectorStateRenderer
    {

        private readonly LevelSelectorState _state;
        private readonly GL _gl;

        private readonly TextureQuadRenderer _texturedQuadRenderer;
        private readonly FillRenderer _fillRenderer;

        public GL Gl => _gl;

        public LevelSelectorState State => _state;

        public LevelSelectorStateRenderer(LevelSelectorState state)
        {
            _state = state;
            _gl = state.Manager.Game.Gl;

            _texturedQuadRenderer = new TextureQuadRenderer(_state.Manager.Game, _gl);
            _fillRenderer = new FillRenderer(_state.Manager.Game, _gl);
        }

        public void RenderTexturedQuad(NIMBY.Graphics.Texture texture, float x, float y, float width, float height)
        {
            _texturedQuadRenderer.Render(texture, x, y, width, height);
        }

        public void RenderQuad(float x, float y, float width, float height, Vector4 colour)
        {
            _fillRenderer.Render(x, y, width, height, colour);
        }

        public void PrepareLegacy()
        {
            var game = _state.Manager.Game;

            game.LegacyGl.Enable(Silk.NET.OpenGL.Legacy.GLEnum.Blend);
            game.LegacyGl.BlendFunc(Silk.NET.OpenGL.Legacy.GLEnum.SrcAlpha, Silk.NET.OpenGL.Legacy.GLEnum.OneMinusSrcAlpha);
            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.GLEnum.Texture2D);
            game.LegacyGl.MatrixMode(Silk.NET.OpenGL.Legacy.GLEnum.Projection);
            game.LegacyGl.LoadIdentity();
            game.LegacyGl.Ortho(0, game.Witdh, game.Height, 0, -1, 1);

            game.LegacyGl.MatrixMode(Silk.NET.OpenGL.Legacy.GLEnum.Modelview);
            game.LegacyGl.LoadIdentity();
            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.GLEnum.DepthTest);
            game.LegacyGl.Color4(255, 255, 255, 255);
            game.LegacyGl.Enable(Silk.NET.OpenGL.Legacy.GLEnum.Blend);
            game.LegacyGl.BlendFunc(Silk.NET.OpenGL.Legacy.GLEnum.SrcAlpha, Silk.NET.OpenGL.Legacy.GLEnum.OneMinusSrcAlpha);
            game.LegacyGl.Enable(Silk.NET.OpenGL.Legacy.GLEnum.CullFace);
        }

        public void EndLegacy()
        {
            var game = _state.Manager.Game;

            game.LegacyGl.MatrixMode(Silk.NET.OpenGL.Legacy.MatrixMode.Modelview);
            game.LegacyGl.LoadIdentity();
            game.LegacyGl.MatrixMode(Silk.NET.OpenGL.Legacy.MatrixMode.Projection);
            game.LegacyGl.LoadIdentity();

            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.EnableCap.Blend);
            game.LegacyGl.Disable(Silk.NET.OpenGL.Legacy.EnableCap.CullFace);
            game.LegacyGl.Flush();
        }

    }
}

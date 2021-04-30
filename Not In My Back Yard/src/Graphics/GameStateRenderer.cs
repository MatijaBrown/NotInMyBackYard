using NIMBY.Graphics.Renderers;
using NIMBY.States;
using NIMBY.Tiles;
using NIMBY.Utils;
using Silk.NET.OpenGL;
using System.Collections.Generic;

namespace NIMBY.Graphics
{
    public class GameStateRenderer
    {

        private readonly GameState _state;
        private readonly GL _gl;

        private readonly IList<Tile> _tiles = new List<Tile>();
        private readonly TileRenderer _tileRenderer;
        private readonly TextureQuadRenderer _texturedQuadRenderer;

        public GL Gl => _gl;

        public GameState State => _state;

        public GameStateRenderer(GameState state)
        {
            _state = state;
            _gl = state.Manager.Game.Gl;

            _tileRenderer = new TileRenderer(this);
            _texturedQuadRenderer = new TextureQuadRenderer(_state.Manager.Game, _gl);
        }

        public void RenderTile(Tile tile)
        {
            _tiles.Add(tile);
        }

        public void RenderTexturedQuad(Texture texture, float x, float y, float width, float height)
        {
            _texturedQuadRenderer.Render(texture, x, y, width, height);
        }

        public void Render(Camera camera)
        {
            foreach (Tile tile in _tiles)
            {
                _tileRenderer.Render(tile, camera);
            }
            _tiles.Clear();
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

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

        public GL Gl => _gl;

        public GameState State => _state;

        public GameStateRenderer(GameState state)
        {
            _state = state;
            _gl = state.Manager.Game.Gl;

            _tileRenderer = new TileRenderer(this);
        }

        public void RenderTile(Tile tile)
        {
            _tiles.Add(tile);
        }

        public void Render(Camera camera)
        {
            foreach (Tile tile in _tiles)
            {
                _tileRenderer.Render(tile, camera);
            }
            _tiles.Clear();
        }

    }
}

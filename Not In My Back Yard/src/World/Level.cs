using NIMBY.States;
using NIMBY.Tiles;
using NIMBY.Utils;
using System;
using System.IO;

namespace NIMBY.World
{
    public class Level : IDisposable
    {

        private Tile[ , ] _tiles;
        private uint _worldWidth, _worldHeight;
        private uint _maxTurbines;

        private readonly GameState _state;
        private readonly Graphics.Renderers.TileRenderer _renderer;

        public uint WorldWidth => _worldWidth;

        public float PixelWidth => _worldWidth * Tile.SIZE;

        public uint WorldHeight => _worldHeight;

        public float PixelHeight => _worldHeight * Tile.SIZE;

        public uint MaxTurbines => _maxTurbines;

        public GameState State => _state;

        public uint PlacedTurbines { get; set; } = 0;

        public Level(string levelFile, GameState state)
        {
            _state = state;

            LoadLevel(levelFile);
            _renderer = new Graphics.Renderers.TileRenderer(_state);
        }

        public void Update()
        {
            float xOff = -(float)(_worldWidth * Tile.SIZE / 2.0f);
            float yOff = (float)(_worldHeight * Tile.SIZE / 2.0f);

            foreach (Tile tile in _tiles)
            {
                tile.Update(xOff, yOff);
            }

            if (Input.WasMouseButtonReleased(Silk.NET.Input.MouseButton.Left))
            {
                foreach (Tile tile in _tiles)
                {
                    tile.Click(Input.MouseX, Input.MouseY, xOff, yOff);
                }
            }

        }

        public void Render(Camera camera)
        {
            foreach (Tile tile in _tiles)
            {
                _renderer.Render(tile, camera);
            }
        }

        private void LoadLevel(string file)
        {
            string[] lines = File.ReadAllLines("./Assets/Levels/" + file + ".lvl");

            // Load Metadata
            string[] meta = lines[0].Split(' ');
            _worldWidth = uint.Parse(meta[0]);
            _worldHeight = uint.Parse(meta[1]);
            _maxTurbines = uint.Parse(meta[2]);
            _tiles = new Tile[_worldHeight, _worldWidth];

            // Load Tiles
            for (int i = 1; i <= _worldHeight; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < _worldWidth; j++)
                {
                    uint id = uint.Parse(line[j]);
                    _tiles[i - 1, j] = new Tile((uint)j, (uint)i - 1, (TileType)id, this);
                }
            }

            // Load wind powers
            for (int i = 1; i <= _worldHeight; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < _worldWidth; j++)
                {
                    int power = int.Parse(line[j]);
                    _tiles[i - 1, j].WindPower = power;
                }
            }

            // TODO: Load writing

        }

        public void Dispose()
        {

        }

    }
}

using NIMBY.States;
using NIMBY.Tiles;
using Silk.NET.GLFW;
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

        private float _maxOutput;

        public uint WorldWidth => _worldWidth;

        public float PixelWidth => _worldWidth * Tile.SIZE;

        public uint WorldHeight => _worldHeight;

        public float PixelHeight => _worldHeight * Tile.SIZE;

        public uint MaxTurbines => _maxTurbines;

        public GameState State => _state;

        public uint PlacedTurbines { get; set; } = 0;

        public bool Hovering { get; set; } = true;

        public float MaxOutput => _maxOutput;

        public float CurrentOutput { get; set; }

        public Level(GameState state)
        {
            _state = state;

            LoadLevel(_state.LevelMeta.name);
            Input.OnMouseReleased += Click;
        }

        private void Click(MouseButton button)
        {
            if (button != MouseButton.Left || Input.Dragging || Hovering)
                return;

            foreach (Tile tile in _tiles)
            {
                tile.Click(Input.MouseX, Input.MouseY);
            }
        }

        public void Update()
        {
            CurrentOutput = 0;

            float xOff = -(float)(_worldWidth * Tile.SIZE / 2.0f);
            float yOff = (float)(_worldHeight * Tile.SIZE / 2.0f);

            foreach (Tile tile in _tiles)
            {
                tile.Update(xOff, yOff);
            }
        }

        public void Render()
        {
            foreach (Tile tile in _tiles)
            {
                tile.Render();
            }
        }

        public void Reset()
        {
            GC.Collect();

            LoadLevel(_state.LevelMeta.name);
            PlacedTurbines = 0;
        }

        private void LoadLevel(string file)
        {
            string[] lines = File.ReadAllLines("./Assets/Levels/" + file + ".lvl");

            // Load Metadata
            string[] meta = lines[0].Split(' ');
            _worldWidth = uint.Parse(meta[0]);
            _worldHeight = uint.Parse(meta[1]);
            _maxTurbines = uint.Parse(meta[2]);
            _maxOutput = float.Parse(meta[3]);
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
            for (int i = 1 + (int)_worldHeight; i < 2 * _worldHeight + 1; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < _worldWidth; j++)
                {
                    int power = int.Parse(line[j]);
                    _tiles[i - 1 - _worldHeight, j].WindPower = power;
                }
            }

        }

        public Tile GetTile(uint x, uint y)
        {
            if (x < 0 || y < 0 || x >= _worldWidth || y >= _worldHeight)
            {
                return null;
            }

            return _tiles[y, x];
        }

        public void Dispose()
        {

        }

    }
}

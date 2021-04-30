using LevelEditor.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Wind_Thing.World
{
    public class World
    {

        private readonly Game _game;

        private int _width, _height;
        private Tile[ , ] _tiles;

        public Game Game => _game;

        public int Width => _width;

        public int Height => _height;

        public World(Game game)
        {
            _game = game;
            Reset(1, 1);
        }

        public void Update()
        {
            foreach (Tile tile in _tiles)
            {
                tile.Update();
            }
        }

        public void Render(Silk.NET.OpenGL.Legacy.GL gl)
        {
            gl.Color4(1.0f, 1.0f, 1.0f, 1.0f);
            foreach (Tile tile in _tiles)
            {
                tile.Render(gl);
            }
        }

        public void Reset(int width, int height)
        {
            if (width > 15 || height > 15)
                return;

            _width = width;
            _height = height;
            _tiles = new Tile[_height, _width];
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _tiles[i, j] = new Tile(j * Tile.SIZE, i * Tile.SIZE, TileType.Plains, 0, this);
                }
            }

        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
            {
                return null;
            }
            else
            {
                return _tiles[x, y];
            }
        }

        public void Export(Ui ui)
        {
            int width = _width;
            int height = _height;
            int maxTurbines = ui.MaxTurbines;

            List<string> tiles = new();
            List<string> windPower = new();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tiles.Add(((int)_tiles[i, j].TileType).ToString());
                    tiles.Add(" ");
                    windPower.Add(_tiles[i, j].WindPower.ToString());
                    windPower.Add(" ");
                }
                tiles.RemoveAt(tiles.Count - 1);
                windPower.RemoveAt(windPower.Count - 1);
                tiles.Add(Environment.NewLine);
                windPower.Add(Environment.NewLine);
            }

            windPower.RemoveAt(windPower.Count - 1);

            string result = width.ToString() + " " + height.ToString() + " " + maxTurbines.ToString() + Environment.NewLine;
            foreach (string s in tiles)
            {
                result += s;
            }
            foreach (string wp in windPower)
            {
                result += wp;
            }

            var writer = File.CreateText(ui.OutputFont + ".lvl");
            writer.Write(result);
            writer.Close();

        }

        public static Vector2 ToTileCoords(float x, float y)
        {
            return new Vector2(MathF.Floor(x / Tile.SIZE), MathF.Floor(y / Tile.SIZE));
        }

    }
}

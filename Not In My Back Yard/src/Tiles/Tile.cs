using NIMBY.Utils;
using NIMBY.World;
using System;

namespace NIMBY.Tiles
{
    public class Tile
    {

        public const float SIZE = 64;

        public static readonly float[] VERTICES = new float[]
        {
            0, 0,
            0, SIZE,
            SIZE, SIZE,
            0, 0,
            SIZE, SIZE,
            SIZE, 0
        };

        private readonly uint _tileX, _tileY;
        private readonly TileType _type;
        private readonly Level _level;

        private int _windPower;
        private bool _turbined;

        private float _drawX, _drawY;

        public uint TileX => _tileX;

        public uint TileY => _tileY;

        public TileType TileType => _type;

        public int WindPower { get => _windPower; set => _windPower = value; }

        public bool Turbined => _turbined;

        public float DrawX => _drawX;

        public float DrawY => _drawY;

        public Tile(uint tileX, uint tileY, TileType type, Level level)
        {
            _tileX = tileX;
            _tileY = tileY;
            _type = type;
            _level = level;

            _turbined = false;
        }

        private bool CanHaveTurbine()
        {
            return _type == TileType.Grass;
        }

        private void Turbine()
        {
            if (CanHaveTurbine() && _level.PlacedTurbines < _level.MaxTurbines)
            {
                _level.PlacedTurbines++;
                _turbined = true;
            }
        }

        private void Deturbine()
        {
            if (_turbined)
            {
                _level.PlacedTurbines--;
                _turbined = false;
            }
        }

        public void Click(float x, float y)
        {
            var game = _level.Game;

            x -= game.Witdh / 2.0f;
            x += game.Camera.XOffset;
            y -= game.Height / 2.0f;
            y *= -1.0f;
            y += game.Camera.YOffset;

            if (x > _drawX && y > _drawY && x < _drawX + SIZE && y < _drawY + SIZE)
            {
                if (_turbined)
                    Deturbine();
                else
                    Turbine();
            }
        }

        public void Update(float xOffset, float yOffset)
        {
            _drawX = xOffset + (float)(_tileX * SIZE);
            _drawY = yOffset - (float)(_tileY * SIZE);
        }

    }
}

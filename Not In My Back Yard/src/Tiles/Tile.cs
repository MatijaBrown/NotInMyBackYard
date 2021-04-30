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
        private float _production;

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
            x -= _level.State.Manager.Game.Witdh / 2.0f;
            y -= _level.State.Manager.Game.Height / 2.0f;

            float dx = _drawX * _level.State.Manager.Game.Camera.Scale - _level.State.Manager.Game.Camera.Position.X;
            float dy = -_drawY * _level.State.Manager.Game.Camera.Scale + _level.State.Manager.Game.Camera.Position.Y;
            float s = SIZE * _level.State.Manager.Game.Camera.Scale;

            if (x > dx && y < dy && x < dx + s && y > dy - s)
            {
                if (_turbined)
                    Deturbine();
                else
                    Turbine();
            }

        }

        private void ApplyInfluence(Tile t)
        {
            if (t == null)
                return;

            _production += (float)t._windPower / 2.0f;
            if (t._type == TileType.Mountain)
            {
                _production -= (float)t._windPower / 2.0f;
                _production += (float)t._windPower * 2.0f;
            }
        }

        private void ApplyPenalties(Tile t)
        {
            if (t == null)
                return;

            if (t.TileType == TileType.Forest)
            {
                _production *= 0.9f;
            } else if (t.TileType == TileType.Building && !NeighboursHouse())
            {
                _production = 0.0f;
            }

            if (t._turbined)
            {
                _production *= 0.75f;
            }

            _production = Math.Max(0, _production);
        }

        private bool NeighboursHouse()
        {
            var top = _level.GetTile(_tileX, _tileY + 1);
            var bottom = _level.GetTile(_tileX, _tileY - 1);
            var left = _level.GetTile(_tileX - 1, _tileY);
            var right = _level.GetTile(_tileX + 1, _tileY);

            if (top != null && top.TileType == TileType.House)
                return true;
            if (bottom != null && bottom.TileType == TileType.House)
                return true;
            if (left != null && left.TileType == TileType.House)
                return true;
            if (right != null && right.TileType == TileType.House)
                return true;

            return false;
        }

        private void ComputeWindValue()
        {
            _production = _windPower;

            // Straight
            ApplyInfluence(_level.GetTile(_tileX, _tileY - 1));
            ApplyInfluence(_level.GetTile(_tileX, _tileY + 1));
            ApplyInfluence(_level.GetTile(_tileX - 1, _tileY));
            ApplyInfluence(_level.GetTile(_tileX + 1, _tileY));
            ApplyPenalties(_level.GetTile(_tileX, _tileY - 1));
            ApplyPenalties(_level.GetTile(_tileX, _tileY + 1));
            ApplyPenalties(_level.GetTile(_tileX - 1, _tileY));
            ApplyPenalties(_level.GetTile(_tileX + 1, _tileY));

            if (NeighboursHouse())
                _production *= -1;
        }

        public void Update(float xOffset, float yOffset)
        {
            if (_turbined)
            {
            ComputeWindValue();
            _level.CurrentOutput += _production;
            }

            _drawX = xOffset + (float)(_tileX * SIZE);
            _drawY = yOffset - (float)(_tileY * SIZE);
        }

        public void Render()
        {
            _level.State.Renderer.RenderTile(this);
        }

    }
}

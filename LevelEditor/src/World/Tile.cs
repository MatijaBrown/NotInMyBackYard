using Silk.NET.OpenGL.Legacy;
using System;
using Wind_Thing.Resources;

namespace Wind_Thing.World
{

    public enum TileType
    {
        Plains = 0,
        Water = 1,
        House = 2,
        Mountain = 3,
        Forest = 4,
        Building = 5
    }

    public class Tile : IDisposable
    {

        public const float SIZE = 64;

        public static readonly float[] TEX_COORDS =
        {
            0.0f, 1.0f,
            0.0f, 0.0f,
            1.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 0.0f,
            1.0f, 1.0f
        };

        private TileType _type;
        private readonly World _world;

        private readonly float _x, _y;
        private int _windPower;

        public int WindPower { get => _windPower; set => _windPower = value; }

        public TileType TileType { get => _type; set => _type = value; }

        public Tile(float x, float y, TileType type, int windPower, World world)
        {
            _x = x;
            _y = y;
            _type = type;
            _windPower = windPower;
            _world = world;

            Input.OnLeftDown += OnLeftDown;
        }

        private void OnLeftDown(float x, float y)
        {
            if (x > _x && y > _y && x < _x + SIZE && y < _y + SIZE)
            {
                if (!_world.Game.Ui.TilePowerSelector.Active)
                    _type = _world.Game.Ui.CurrentSelected;
                else
                    _windPower = _world.Game.Ui.CurrentWindPower;
            }
        }

        public void Update()
        {

        }

        public unsafe void Render(GL gl)
        {
            var texture = GetTexture(_type);

            texture.Bind();
            gl.AlphaFunc(AlphaFunction.Greater, 0.5f);
            gl.Enable(EnableCap.AlphaTest);
            gl.Enable(EnableCap.Texture2D);
            gl.EnableClientState(EnableCap.VertexArray);
            gl.EnableClientState(EnableCap.TextureCoordArray);

            fixed (float* tc = TEX_COORDS)
            {
                gl.TexCoordPointer(2, GLEnum.Float, sizeof(float) * 2, tc);
            }

            float[] verts =
            {
                _x, _y,
                _x, _y + SIZE,
                _x + SIZE, _y + SIZE,
                _x, _y,
                _x + SIZE, _y + SIZE,
                _x + SIZE, _y
            };

            fixed (float* v = verts)
            {
                gl.VertexPointer(2, GLEnum.Float, sizeof(float) * 2, v);
            }

            gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
            texture.Unbind();

            if (_windPower != 0)
            {
                Resources.Texture windNumber = GetWindNumber(_windPower);
                windNumber.Bind();
                gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
                windNumber.Unbind();
            }

            gl.Disable(EnableCap.Texture2D);
            gl.DisableClientState(EnableCap.VertexArray);
            gl.DisableClientState(EnableCap.TextureCoordArray);
        }

        public static Resources.Texture GetTexture(TileType type)
        {
            return type switch
            {
                TileType.Plains => Assets.GrassTexture,
                TileType.Water => Assets.WaterTexture,
                TileType.Mountain => Assets.MountainTexture,
                TileType.House => Assets.HouseTexture,
                TileType.Forest => Assets.ForestTexture,
                TileType.Building => Assets.Building,
                _ => Assets.GrassTexture,
            };
        }

        public static Resources.Texture GetWindNumber(int windPower)
        {
            return windPower switch
            {
                0 => Assets.Zero,
                1 => Assets.One,
                2 => Assets.Two,
                3 => Assets.Three,
                4 => Assets.Four,
                5 => Assets.Five,
                6 => Assets.Six,
                7 => Assets.Seven,
                8 => Assets.Eight,
                9 => Assets.Nine,
                _ => throw new NotImplementedException()
            };
        }

        public void Dispose()
        {
            Input.OnLeftDown -= OnLeftDown;
        }

    }
}

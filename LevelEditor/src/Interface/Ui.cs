using FontStash.NET;
using Silk.NET.GLFW;
using Silk.NET.OpenGL.Legacy;
using Wind_Thing;
using Wind_Thing.World;

namespace LevelEditor.Interface
{
    public class Ui
    {

        private readonly Game _game;

        private readonly TextInput _widthInput, _heightInput, _outputName, _maxTurbinesInput, _fiveStarEnergyInput;
        private readonly ToggleButton _tilePowerSelector;
        private readonly Fontstash _fons;

        private readonly Button _grass, _water, _building, _forest, _house, _mountain;
        private readonly Button _zero, _one, _two, _three, _four, _five, _six, _seven, _eight, _nine;
        private readonly Button _exportButton;

        private TileType _currentSelected = TileType.Plains;
        private int _currentWindPower = 0;

        private string _outputFont = null;
        private int _maxTurbines;
        private int _fiveStarEnergy = 0;

        public TileType CurrentSelected => _currentSelected;

        public int CurrentWindPower => _currentWindPower;

        public ToggleButton TilePowerSelector => _tilePowerSelector;

        public string OutputFont => _outputFont;

        public int MaxTurbines => _maxTurbines;

        public int FiveStarEnergy => _fiveStarEnergy;

        public Ui(Game game)
        {
            _game = game;
            _fons = _game.Fons;

            _widthInput = new TextInput(1000.0f, 5.0f, 200.0f, 25.0f, 24, 2, "Width", "stdfont", _fons, () => _game.World.Reset(int.Parse(_widthInput.Text), _game.World.Height));
            _heightInput = new TextInput(1000.0f, 40.0f, 200.0f, 25.0f, 24, 2, "Height", "stdfont", _fons, () => _game.World.Reset(_game.World.Width, int.Parse(_heightInput.Text)));
            _outputName = new TextInput(1000.0f, 550.0f, 240.0f, 25.0f, 24, -1, "Name", "stdfont", _fons, () => _outputFont = _outputName.Text);
            _maxTurbinesInput = new TextInput(1000.0f, 75.0f, 240.0f, 25.0f, 24, -1, "Turbines", "stdfont", _fons, () => _maxTurbines = int.Parse(_maxTurbinesInput.Text));
            _fiveStarEnergyInput = new TextInput(1000.0f, 600.0f, 200.0f, 25.0f, 24, -1, "Max E", "stdfont", _fons, () => _fiveStarEnergy = int.Parse(_fiveStarEnergyInput.Text));

            _grass = new Button("Grass", 1000.0f, 200.0f, 50.0f, 30.0f, () => _currentSelected = TileType.Plains);
            _water = new Button("Water", 1000.0f, 230.0f, 50.0f, 30.0f, () => _currentSelected = TileType.Water);
            _building = new Button("Building", 1000.0f, 260.0f, 50.0f, 30.0f, () => _currentSelected = TileType.Building);
            _forest = new Button("Forest", 1000.0f, 290.0f, 50.0f, 30.0f, () => _currentSelected = TileType.Forest);
            _house = new Button("House", 1000.0f, 320.0f, 50.0f, 30.0f, () => _currentSelected = TileType.House);
            _mountain = new Button("Mountain", 1000.0f, 350.0f, 50.0f, 30.0f, () => _currentSelected = TileType.Mountain);

            _zero = new Button("Zero", 1150.0f, 200.0f, 50.0f, 30.0f, () => _currentWindPower = 0);
            _one = new Button("One", 1150.0f, 230.0f, 50.0f, 30.0f, () => _currentWindPower = 1);
            _two = new Button("Two", 1150.0f, 260.0f, 50.0f, 30.0f, () => _currentWindPower = 2);
            _three = new Button("Three", 1150.0f, 290.0f, 50.0f, 30.0f, () => _currentWindPower = 3);
            _four = new Button("Four", 1150.0f, 320.0f, 50.0f, 30.0f, () => _currentWindPower = 4);
            _five = new Button("Five", 1150.0f, 350.0f, 50.0f, 30.0f, () => _currentWindPower = 5);
            _six = new Button("Six", 1150.0f, 380.0f, 50.0f, 30.0f, () => _currentWindPower = 6);
            _seven = new Button("Seven", 1150.0f, 410.0f, 50.0f, 30.0f, () => _currentWindPower = 7);
            _eight = new Button("Eight", 1150.0f, 440.0f, 50.0f, 30.0f, () => _currentWindPower = 8);
            _nine = new Button("Nine", 1150.0f, 470.0f, 50.0f, 30.0f, () => _currentWindPower = 9);

            _exportButton = new Button("Export", 1000.0f, 500.0f, 240.0f, 25.0f, () => _game.World.Export(this));

            _tilePowerSelector = new ToggleButton("Tiles", "Wind", 1000.0f, 450.0f, 50.0f, 30.0f);
        }

        public void Update()
        {
            if (!_tilePowerSelector.Active)
            {
                _grass.Enabled = true;
                _water.Enabled = true;
                _building.Enabled = true;
                _forest.Enabled = true;
                _house.Enabled = true;
                _mountain.Enabled = true;
            }
            else
            {
                _zero.Enabled = true;
                _one.Enabled = true;
                _two.Enabled = true;
                _three.Enabled = true;
                _four.Enabled = true;
                _five.Enabled = true;
                _six.Enabled = true;
                _seven.Enabled = true;
                _eight.Enabled = true;
                _nine.Enabled = true;
            }

            _grass.Update();
            _water.Update();
            _building.Update();
            _forest.Update();
            _house.Update();
            _mountain.Update();

            _zero.Update();
            _one.Update();
            _two.Update();
            _three.Update();
            _four.Update();
            _five.Update();
            _six.Update();
            _seven.Update();
            _eight.Update();
            _nine.Update();

            _tilePowerSelector.Update();

            _exportButton.Enabled = true;
            _exportButton.Update();
        }

        public void Render(GL gl)
        {
            gl.Begin(PrimitiveType.Triangles);
            gl.Color4(0.2f, 0.6f, 0.6f, 1.0f);
            gl.Vertex2(960.0f, 0.0f);
            gl.Vertex2(960.0f, 960.0f);
            gl.Vertex2(1280.0f, 960.0f);
            gl.Vertex2(960.0f, 0.0f);
            gl.Vertex2(1280.0f, 960.0f);
            gl.Vertex2(1280.0f, 0.0f);
            gl.End();

            _widthInput.Render(gl);
            _heightInput.Render(gl);
            _outputName.Render(gl);
            _maxTurbinesInput.Render(gl);
            _fiveStarEnergyInput.Render(gl);

            gl.Color4(1.0f, 1.0f, 1.0f, 1.0f);
            RenderPreviewImage(gl);

            _grass.Render(_fons);
            _water.Render(_fons);
            _building.Render(_fons);
            _forest.Render(_fons);
            _house.Render(_fons);
            _mountain.Render(_fons);

            _zero.Render(_fons);
            _one.Render(_fons);
            _two.Render(_fons);
            _three.Render(_fons);
            _four.Render(_fons);
            _five.Render(_fons);
            _six.Render(_fons);
            _seven.Render(_fons);
            _eight.Render(_fons);
            _nine.Render(_fons);

            _tilePowerSelector.Render(_fons);

            _exportButton.Render(_fons);
        }

        private unsafe void RenderPreviewImage(GL gl)
        {
            var texture = Tile.GetTexture(_currentSelected);

            gl.AlphaFunc(AlphaFunction.Greater, 0.5f);
            gl.Enable(EnableCap.AlphaTest);
            gl.Enable(EnableCap.Texture2D);
            gl.EnableClientState(EnableCap.VertexArray);
            gl.EnableClientState(EnableCap.TextureCoordArray);

            texture.Bind();
            fixed (float* tc = Tile.TEX_COORDS)
            {
                gl.TexCoordPointer(2, GLEnum.Float, sizeof(float) * 2, tc);
            }

            float x = 1000.0f;
            float y = 100.0f;

            float[] verts =
            {
                x, y,
                x, y + Tile.SIZE,
                x + Tile.SIZE, y + Tile.SIZE,
                x, y,
                x + Tile.SIZE, y + Tile.SIZE,
                x + Tile.SIZE, y
            };

            fixed (float* v = verts)
            {
                gl.VertexPointer(2, GLEnum.Float, sizeof(float) * 2, v);
            }

            gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
            texture.Unbind();

            texture = Tile.GetWindNumber(_currentWindPower);

            texture.Bind();
            x = 1150.0f;
            y = 100.0f;

            verts = new float[]
            {
                x, y,
                x, y + Tile.SIZE,
                x + Tile.SIZE, y + Tile.SIZE,
                x, y,
                x + Tile.SIZE, y + Tile.SIZE,
                x + Tile.SIZE, y
            };

            fixed (float* v = verts)
            {
                gl.VertexPointer(2, GLEnum.Float, sizeof(float) * 2, v);
            }

            gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
            texture.Unbind();

            gl.Disable(EnableCap.Texture2D);
            gl.DisableClientState(EnableCap.VertexArray);
            gl.DisableClientState(EnableCap.TextureCoordArray);
        }

    }
}

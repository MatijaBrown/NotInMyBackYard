using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIMBY.Graphics.Renderers
{
    public class TileRenderer
    {

        private readonly Shader _shader;
        private readonly Texture _texture;
        private readonly GL _gl;

        public TileRenderer(GL gl)
        {
            _gl = gl;
            _shader = ResourceManager.LoadShader("tileVertexShader", "tileFragmentShader");
            _texture = ResourceManager.LoadTexture("houseTile_lightsOn");
        }

        private void TileRenderData()
        {

        }

        public void Render(string type, float x, float y, float width, float hight)
        {

        }

    }
}

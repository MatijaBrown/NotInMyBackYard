using Silk.NET.OpenGL;
using System.Numerics;

namespace NIMBY.Graphics.Renderers
{
    public class FillRenderer
    {

        private readonly GL _gl;
        private readonly Shader _shader;
        private readonly Game _game;

        private readonly VAO _vao;

        public FillRenderer(Game game, GL gl)
        {
            _game = game;
            _gl = gl;

            _shader = ResourceManager.LoadShader("fillVertexShader", "fillFragmentShader");
            _vao = ResourceManager.CreateVao();
        }

        public void Render(float x, float y, float width, float height, Vector4 colour)
        {
            float[] verts =
            {
                x, y,
                x, y + height,
                x + width, y + height,
                x, y,
                x + width, y + height,
                x + width, y
            };
            _vao.StoreData(verts, 0, 2, GLEnum.Float);

            _gl.Enable(EnableCap.Blend);
            _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _shader.Start();
            _shader.LoadMatrix("transformation", Matrix4x4.Identity);
            _shader.LoadVector("viewSize", new Vector2(_game.Witdh, _game.Height));
            _shader.LoadVector("colour", colour);

            _vao.Bind();
            _gl.EnableVertexAttribArray(0);

            _gl.DrawArrays(GLEnum.Triangles, 0, 6);

            _gl.DisableVertexAttribArray(0);
            _vao.Unbind();

            _shader.Stop();

            _gl.Disable(EnableCap.Blend);
        }

    }
}

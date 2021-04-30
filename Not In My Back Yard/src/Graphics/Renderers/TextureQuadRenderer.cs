using NIMBY.Utils;
using Silk.NET.OpenGL;
using System.Numerics;

namespace NIMBY.Graphics.Renderers
{
    public class TextureQuadRenderer
    {

        private static readonly float[] TEX_COORDS =
        {
            0, 1,
            0, 0,
            1, 0,
            0, 1,
            1, 0,
            1, 1
        };

        private readonly GL _gl;
        private readonly Shader _shader;
        private readonly MenuStateRenderer _master;

        private readonly VAO _vao;

        public TextureQuadRenderer(MenuStateRenderer master)
        {
            _master = master;
            _gl = _master.Gl;

            _shader = ResourceManager.LoadShader("texturedQuadVertexShader", "texturedQuadFragmentShader");
            _vao = ResourceManager.CreateVao();
            _vao.StoreData(TEX_COORDS, 1, 2, GLEnum.Float);
        }

        public void Render(Texture texture, float x, float y, float width, float height)
        {
            var game = _master.State.Manager.Game;

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
            _shader.LoadVector("viewSize", new Vector2(game.Witdh, game.Height));

            _vao.Bind();
            _gl.EnableVertexAttribArray(0);
            _gl.EnableVertexAttribArray(1);

            texture.Bind();

            _gl.DrawArrays(GLEnum.Triangles, 0, 6);

            texture.Unbind();

            _gl.DisableVertexAttribArray(0);
            _gl.DisableVertexAttribArray(1);
            _vao.Unbind();

            _shader.Stop();

            _gl.Disable(EnableCap.Blend);
        }

    }
}

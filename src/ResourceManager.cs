using Silk.NET.OpenGL;
using System.Collections.Generic;

using Shader = NIMBY.Graphics.Shader;
using Texture = NIMBY.Graphics.Texture;

namespace NIMBY
{
    public static class ResourceManager
    {

        private static readonly IList<Shader> shaders = new List<Shader>();
        private static readonly IList<Texture> textures = new List<Texture>();

        private static GL gl;

        public static void Init(GL gl)
        {
            ResourceManager.gl = gl;
        }

        public static Shader LoadShader(string vertexFile, string fragmentFile, string geometryFile = null)
        {
            Shader shader = new(vertexFile, fragmentFile, gl, geometryFile);
            shaders.Add(shader);
            return shader;
        }

        public static Texture LoadTexture(string textureFile)
        {
            Texture texture = new(textureFile, gl);
            textures.Add(texture);
            return texture;
        }

        public static void Clear()
        {
            foreach (Shader shader in shaders)
            {
                shader.Dispose();
            }
            foreach (Texture texture in textures)
            {
                texture.Dispose();
            }
        }

    }
}

using Silk.NET.OpenGL;
using StbImageSharp;
using System;
using System.IO;

namespace NIMBY.Graphics
{
    public class Texture : IDisposable
    {

        private readonly uint _textureID;
        private readonly uint _width, _height;

        private readonly GL _gl;

        public uint Width => _width;

        public uint Height => _height;

        public Texture(string image, GL gl)
        {
            _gl = gl;
            _textureID = _gl.GenTexture();

            FileStream stream = File.OpenRead("./Assets/Textures/" + image + ".png");
            ImageResult img = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            _width = (uint)img.Width;
            _height = (uint)img.Height;
            Load(img.Data);
            stream.Close();
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            _gl.ActiveTexture(unit);
            _gl.BindTexture(TextureTarget.Texture2D, _textureID);
        }

        public void Unbind()
        {
            _gl.BindTexture(TextureTarget.Texture2D, 0);
        }

        private unsafe void Load(byte[] data)
        {
            Bind();
            _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            _gl.PixelStore(PixelStoreParameter.UnpackRowLength, _width);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipPixels, 0);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipRows, 0);

            fixed (byte* pixels = data)
            {
                _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, _width, _height, 0, GLEnum.Rgba, GLEnum.UnsignedByte, pixels);
            }

            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            _gl.PixelStore(PixelStoreParameter.UnpackRowLength, 0);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipPixels, 0);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipRows, 0);

            Unbind();
        }

        public void Dispose()
        {
            Unbind();
            _gl.DeleteTexture(_textureID);
        }

    }
}

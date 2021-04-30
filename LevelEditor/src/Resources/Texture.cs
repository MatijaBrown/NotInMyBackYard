using Silk.NET.OpenGL.Legacy;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Runtime.InteropServices;

namespace Wind_Thing.Resources
{
    public class Texture : IDisposable
    {

        private readonly uint _textureID;
        private readonly GL _gl;

        public Texture(string file, GL gl)
        {
            _gl = gl;
            _textureID = _gl.GenTexture();
            Load(file);
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            _gl.ActiveTexture(unit);
            _gl.BindTexture(TextureTarget.Texture2D, _textureID);
        }

        private unsafe void Load(string file)
        {
            Bind();
            Image<Rgba32> image = (Image<Rgba32>)Image.Load("./sprites/" + file + ".png");
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            _gl.PixelStore(PixelStoreParameter.UnpackRowLength, image.Width);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipPixels, 0);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipRows, 0);

            fixed (void* pixels = &MemoryMarshal.GetReference(image.GetPixelRowSpan(0)))
            {
                _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, (uint)image.Width, (uint)image.Height, 0, GLEnum.Rgba, GLEnum.UnsignedByte, pixels);
            }
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);

            _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            _gl.PixelStore(PixelStoreParameter.UnpackRowLength, 0);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipPixels, 0);
            _gl.PixelStore(PixelStoreParameter.UnpackSkipRows, 0);

            Unbind();
            image.Dispose();
        }

        public void Unbind()
        {
            _gl.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            Unbind();
            _gl.DeleteTexture(_textureID);
        }

    }
}

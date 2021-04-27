using System.Collections.Generic;

namespace NIMBY.Graphics
{
    public class TextureAtlas
    {

        private readonly IDictionary<string, float[]> _coordinateMap = new Dictionary<string, float[]>();
        private readonly Texture _source;

        public IDictionary<string, float[]> CoordinateMap => _coordinateMap;

        public Texture Source => _source;

        public TextureAtlas(Texture source)
        {
            _source = source;
        }

        public void Bind(Silk.NET.OpenGL.TextureUnit unit = Silk.NET.OpenGL.TextureUnit.Texture0)
        {
            _source.Bind(unit);
        }

        public void Unbind()
        {
            _source.Unbind();
        }

        public void AddSubImage(string name, uint sourceX, uint sourceY, uint width, uint height)
        {
            float x = (float)sourceX / (float)_source.Width;
            float y = (float)sourceY / (float)_source.Height;
            float w = (float)width / (float)_source.Width;
            float h = (float)height / (float)_source.Height;
            float[] coords = new float[]
            {
                x, y + h,
                x, y,
                x + w, y,
                x, y + h,
                x + w, y,
                x + w, y + h
            };
            _coordinateMap.Add(name, coords);
        }

        public float[] GetSubImage(string name)
        {
            return _coordinateMap[name];
        }

    }
}

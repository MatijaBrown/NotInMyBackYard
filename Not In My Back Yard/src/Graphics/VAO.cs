using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;

namespace NIMBY.Graphics
{
    public class VAO : IDisposable
    {

        private readonly IList<uint> _vbos = new List<uint>();

        private readonly uint _vaoID;
        private readonly GL _gl;

        public VAO(GL gl)
        {
            _gl = gl;
            _vaoID = _gl.GenVertexArray();
        }

        public void Bind()
        {
            _gl.BindVertexArray(_vaoID);
        }

        public void Unbind()
        {
            _gl.BindVertexArray(0);
        }

        public unsafe void StoreData<T>(T[] data, uint slot, int dimensions, GLEnum dataType)
            where T : unmanaged
        {
            Bind();
            uint vboID = _gl.GenBuffer();
            _vbos.Add(vboID);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, vboID);
            fixed (T* d = data)
            {
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (uint)(sizeof(T) * data.Length), d, BufferUsageARB.StaticDraw);
            }
            _gl.VertexAttribPointer(slot, dimensions, dataType, false, (uint)(dimensions * sizeof(T)), null);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            Unbind();
        }

        public void Dispose()
        {
            Unbind();
            foreach (uint vbo in _vbos)
            {
                _gl.DeleteBuffer(vbo);
            }
            _gl.DeleteVertexArray(_vaoID);
        }

    }
}

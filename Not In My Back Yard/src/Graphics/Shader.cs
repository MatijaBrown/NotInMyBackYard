using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace NIMBY.Graphics
{
    public class Shader : IDisposable
    {

        private readonly uint _vertexShaderID;
        private readonly int _geometryShaderID;
        private readonly uint _fragmentShaderID;
        private readonly uint _programmeID;

        private readonly IDictionary<string, int> _uniformLocations = new Dictionary<string, int>();

        private readonly GL _gl;

        public Shader(string vertexShader, string fragmentShader, GL gl, string geometryShader = null)
        {
            _gl = gl;
            _vertexShaderID = LoadShader(vertexShader, ShaderType.VertexShader, _gl);
            if (geometryShader != null)
                _geometryShaderID = (int)LoadShader(geometryShader, ShaderType.GeometryShader, _gl);
            else
                _geometryShaderID = -1;
            _fragmentShaderID = LoadShader(fragmentShader, ShaderType.FragmentShader, _gl);
            _programmeID = _gl.CreateProgram();
            _gl.AttachShader(_programmeID, _vertexShaderID);
            if (_geometryShaderID != -1)
                _gl.AttachShader(_programmeID, (uint)_geometryShaderID);
            _gl.AttachShader(_programmeID, _fragmentShaderID);
            _gl.LinkProgram(_programmeID);
            ValidateProgramme(_programmeID, gl);
        }

        public void Start()
        {
            _gl.UseProgram(_programmeID);
        }

        public void Stop()
        {
            _gl.UseProgram(0);
        }

        private int GetLocation(string name)
        {
            if (!_uniformLocations.ContainsKey(name))
            {
                _uniformLocations.Add(name, _gl.GetUniformLocation(_programmeID, name));
            }
            return _uniformLocations[name];
        }

        public void LoadFloat(string name, float value)
        {
            _gl.Uniform1(GetLocation(name), value);
        }

        public void LoadInt(string name, int value)
        {
            _gl.Uniform1(GetLocation(name), value);
        }

        public void LoadBool(string name, bool value)
        {
            LoadInt(name, value ? 1 : 0);
        }

        public void LoadVector(string name, Vector2 value)
        {
            _gl.Uniform2(GetLocation(name), value);
        }

        public void LoadVector(string name, Vector3 value)
        {
            _gl.Uniform3(GetLocation(name), value);
        }

        public void LoadVector(string name, Vector4 value)
        {
            _gl.Uniform4(GetLocation(name), value);
        }

        public unsafe void LoadMatrix(string name, Matrix4x4 value)
        {
            _gl.UniformMatrix4(GetLocation(name), 1, false, (float*)&value);
        }

        public void Dispose()
        {
            Stop();
            _gl.DetachShader(_programmeID, _vertexShaderID);
            if (_geometryShaderID != -1)
                _gl.DetachShader(_programmeID, (uint)_geometryShaderID);
            _gl.DetachShader(_programmeID, _fragmentShaderID);
            _gl.DeleteShader(_vertexShaderID);
            if (_geometryShaderID != -1)
                _gl.DeleteShader((uint)_geometryShaderID);
            _gl.DeleteShader(_fragmentShaderID);
            _gl.DeleteProgram(_programmeID);
        }

        private static void ValidateProgramme(uint programmeID, GL gl)
        {
            gl.ValidateProgram(programmeID);
            string infoLog = gl.GetProgramInfoLog(programmeID);
            if (!string.IsNullOrEmpty(infoLog))
            {
                Console.Error.WriteLine("\n" + "| ERROR: SHADER: Programme validation error: " + infoLog + "\n");
            }
        }

        private static void CompileShader(uint shader, ShaderType type, GL gl)
        {
            gl.CompileShader(shader);
            string infoLog = gl.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(infoLog))
            {
                Console.Error.WriteLine("\n" + "| ERROR::SHADER: Compilation error: Type: " + type + "\n" + infoLog + "\n");
            }
        }

        private static uint LoadShader(string sourceFile, ShaderType type, GL gl)
        {
            string source = File.ReadAllText("./Assets/Shaders/" + sourceFile + ".glsl");
            uint shaderID = gl.CreateShader(type);
            gl.ShaderSource(shaderID, source);
            gl.CompileShader(shaderID);
            CompileShader(shaderID, type, gl);
            return shaderID;
        }

    }
}

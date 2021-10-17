using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace EsquemaBasico
{
    public class Shader
    {
        public int Handle { get; set; }

        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }
            
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, VertexShaderSource);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, FragmentShaderSource);
            
            GL.CompileShader(vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(vertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(fragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);
            
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);
            
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }
        
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public void Use()
        {
            GL.UseProgram(Handle);
        }
    }
}
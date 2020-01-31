using System;
using System.Drawing;


// GlmNet can be found at https://github.com/dwmkerr/glmnet
using GlmNet;

using static OpenGLLayerExample.OGLProvider;

namespace OpenGLLayerExample
{
    public static class UniformSender
    {
        public static void SendColorAsUniformVariable(uint shader, Color fill, string name)
        {
            float[] argb = { (float)fill.R / 255.0f, (float)fill.G / 255.0f, (float)fill.B / 255.0f };
            int location = GL.GetUniformLocation(shader, name);
            if (location < 0)
                Console.WriteLine("Uniform <" + name + "> not found.");
            GL.Uniform3(location, argb[0], argb[1], argb[2]);
        }

        public static void SendVec3AsUniformVariable(uint shader, float[] value, string name)
        {
            int location = GL.GetUniformLocation(shader, name);
            if (location < 0)
                Console.WriteLine("Uniform <" + name + "> not found.");
            GL.Uniform3(location, value[0], value[1], value[2]);
        }

        public static void SendMVPAsUniformVariable(uint shader, mat4 model, mat4 view, mat4 projection)
        {
            int location = GL.GetUniformLocation(shader, "model");
            if (location < 0)
                Console.WriteLine("Uniform <model> not found.");
            GL.UniformMatrix4(location, 1, false, model.to_array());

            location = GL.GetUniformLocation(shader, "view");
            if (location < 0)
                Console.WriteLine("Uniform <view> not found.");
            GL.UniformMatrix4(location, 1, false, view.to_array());

            location = GL.GetUniformLocation(shader, "projection");
            if (location < 0)
                Console.WriteLine("Uniform <projection> not found.");
            GL.UniformMatrix4(location, 1, false, projection.to_array());
        }

        public static void SendUniformSample2D(uint shader, string name)
        {
            int location = GL.GetUniformLocation(shader, name);
            if (location < 0)
                Console.WriteLine("Uniform <" + name + "> not found.");

            GL.Uniform1(location, 0);
        }
    }
}
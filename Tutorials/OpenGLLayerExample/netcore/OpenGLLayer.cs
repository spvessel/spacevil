using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;

// GlmNet can be found at https://github.com/dwmkerr/glmnet
using GlmNet;

// SharpGL can be found at https://github.com/dwmkerr/sharpgl
using static SharpGL.OpenGL;
using static OpenGLLayerExample.OGLProvider;

namespace OpenGLLayerExample
{
    // implement IDraggable to recieve drag events
    public class OpenGLLayer : Prototype, IOpenGLLayer, IDraggable
    {
        private static int _count = 0;
        public OpenGLLayer()
        {
            SetItemName(this.GetType().Name + _count++);

            // attr
            SetBackground(75, 75, 75);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
        }

        public override void InitElements()
        {
            // assign events

            // rotate cube
            EventKeyPress += (sender, args) =>
            {
                if (args.Key < KeyCode.Right || args.Key > KeyCode.Up)
                    return;

                Rotate(args.Key);
            };
            EventMousePress += (sender, args) =>
            {
                _ptr.SetPosition(args.Position.GetX(), args.Position.GetY());
            };
            EventMouseDrag += (sender, args) =>
            {
                float xRot = (float)(args.Position.GetX() - _ptr.GetX()) / 2;
                _model = glm.rotate(_model, glm.radians(xRot), new vec3(0.0f, 1.0f, 0.0f));

                float yRot = (float)(args.Position.GetY() - _ptr.GetY()) / 2;
                _model = glm.rotate(_model, glm.radians(yRot), new vec3(1.0f, 0.0f, 0.0f));

                _ptr.SetPosition(args.Position.GetX(), args.Position.GetY());
            };

            // zoom scene
            EventScrollUp += (sender, args) =>
            {
                _zCamera -= 0.2f;
                if (_zCamera < 2)
                    _zCamera = 2;
                SetCameraLookAt(_xCamera, _yCamera, _zCamera);
            };
            EventScrollDown += (sender, args) =>
            {
                _zCamera += 0.2f;
                SetCameraLookAt(_xCamera, _yCamera, _zCamera);
            };
        }

        private Pointer _ptr = new Pointer();
        // shaders
        private uint _shaderCommon = 0;
        private uint _shaderLamp = 0;
        private uint _shaderTexture = 0;

        // resources
        private uint[] _VBO;
        private uint[] _cubeVAO;
        private uint[] _lightVAO;
        private uint[] _texture;
        private uint[] _buffers;
        private uint[] _FBO;
        private uint[] _depthrenderbuffer;
        private int _VBOlenght = 0;

        // cube color
        private Color _color;

        // matrices
        private mat4 _projection;
        private mat4 _view;
        private mat4 _model;
        // view attr
        private float _xCamera = 0;
        private float _yCamera = 0;
        private float _zCamera = 3;
        private float _aspectRatio = 1;

        // IOpenGLLayer init flag
        private bool _isInit = false;

        public void Free()
        {
            _isInit = false;
            GL.DeleteProgram(_shaderCommon);
            GL.DeleteProgram(_shaderLamp);
            GL.DeleteProgram(_shaderTexture);
            GL.DeleteBuffers(1, _VBO);
            GL.DeleteVertexArrays(1, _cubeVAO);
            GL.DeleteVertexArrays(1, _lightVAO);
        }

        public void Initialize()
        {
            // configure OpenGL
            GL.Disable(GL_CULL_FACE);

            // creating shaders
            _shaderCommon = CreateShaderProgram(OpenGLResources.GetCommonVertexShaderCode(),
                    OpenGLResources.GetCommonFragmentShaderCode());

            _shaderLamp = CreateShaderProgram(OpenGLResources.GetLampVertexShaderCode(),
                    OpenGLResources.GetLampFragmentShaderCode());

            _shaderTexture = CreateShaderProgram(OpenGLResources.GetTextureVertexShaderCode(),
                    OpenGLResources.GetTextureFragmentShaderCode());

            // gen buffers for cube
            GenBuffers(OpenGLResources.Get3DCubeVertex());
            _color = Color.FromArgb(10, 162, 232);

            // preparing MVP matrices
            _projection = glm.perspective(glm.radians(45f), _aspectRatio, 0.1f, 100.0f);
            _model = new mat4(1f);
            SetCameraLookAt(_xCamera, _yCamera, _zCamera);

            // set init flag
            _isInit = true;
        }

        public bool IsInitialized()
        {
            return _isInit;
        }

        public void Draw()
        {
            // it is example with using a second FBO
            // gen FBO
            GenTexturedFBO();
            // set scene viewport according to items size
            GL.Viewport(0, 0, GetWidth(), GetHeight());
            // crear color and depth bits
            GL.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

            ///////////////
            // draw cube with light
            GL.UseProgram(_shaderCommon);
            UniformSender.SendColorAsUniformVariable(_shaderCommon, Color.White, "lightColor");
            UniformSender.SendVec3AsUniformVariable(_shaderCommon, new float[] { 1.2f, 1.0f, 2.0f }, "lightPos");
            UniformSender.SendMVPAsUniformVariable(_shaderCommon, _model, _view, _projection);
            BindCubeBuffer();
            UniformSender.SendColorAsUniformVariable(_shaderCommon, _color, "objectColor");
            GL.DrawArrays(GL_TRIANGLES, 0, _VBOlenght);

            // optionally: draw edges
            UniformSender.SendColorAsUniformVariable(_shaderCommon, Color.Black, "objectColor");
            GL.DrawArrays(GL_LINE_STRIP, 0, _VBOlenght);

            // using light of lamp
            GL.UseProgram(_shaderLamp);
            BindLampBuffer();
            GL.DrawArrays(GL_TRIANGLES, 0, _VBOlenght);
            /////////////////

            // unbind second FBO and restore viewport to current window
            UnbindFBO();
            RenderService.SetGLLayerViewport(GetHandler(), this);

            // draw FBO texture
            GL.UseProgram(_shaderTexture);
            GenTextureBuffers();
            BindTexture();

            UniformSender.SendUniformSample2D(_shaderTexture, "tex");
            GL.DrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, IntPtr.Zero);

            // delete resources
            GL.DeleteFramebuffersEXT(1, _FBO);
            GL.DeleteRenderbuffersEXT(1, _depthrenderbuffer);
            GL.DeleteTextures(1, _texture);
            GL.DeleteBuffers(2, _buffers);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
        }

        public void Refresh()
        {
            if (!_isInit)
                return;

            _projection = glm.perspective(glm.radians(45f), _aspectRatio, 0.1f, 100.0f);
        }

        public void RestoreView()
        {
            _xCamera = 0;
            _yCamera = 0;
            _zCamera = 3;
            _projection = glm.perspective(glm.radians(45f), _aspectRatio, 0.1f, 100.0f);
            _model = new mat4(1f);
            SetCameraLookAt(_xCamera, _yCamera, _zCamera);
        }

        public void SetZoom(float value)
        {
            _zCamera = value;
            SetCameraLookAt(_xCamera, _yCamera, _zCamera);
        }

        public void Resize()
        {
            _aspectRatio = (float)GetWidth() / GetHeight();
            Refresh();
        }

        public override void SetWidth(int width)
        {
            base.SetWidth(width);
            Resize();
        }

        public override void SetHeight(int height)
        {
            base.SetHeight(height);
            Resize();
        }

        private void SetCameraLookAt(float x, float y, float z)
        {
            _view = glm.lookAt(new vec3(x, y, z), new vec3(0, 0, 0), new vec3(0, 1, 0));
        }

        public void Rotate(KeyCode code)
        {
            if (code == KeyCode.Left)
            {
                _model = glm.rotate(_model, glm.radians(1), new vec3(0.0f, 1.0f, 0.0f));
            }
            if (code == KeyCode.Right)
            {
                _model = glm.rotate(_model, glm.radians(-1), new vec3(0.0f, 1.0f, 0.0f));
            }
            if (code == KeyCode.Up)
            {
                _model = glm.rotate(_model, glm.radians(1), new vec3(1.0f, 0.0f, 0.0f));
            }
            if (code == KeyCode.Down)
            {
                _model = glm.rotate(_model, glm.radians(-1), new vec3(1.0f, 0.0f, 0.0f));
            }
        }

        private uint CreateShaderProgram(string vertexCode, string fragmentCode)
        {
            uint vertex = GL.CreateShader(GL_VERTEX_SHADER);
            GL.ShaderSource(vertex, vertexCode);
            GL.CompileShader(vertex);

            uint fragment = GL.CreateShader(GL_FRAGMENT_SHADER);
            GL.ShaderSource(fragment, fragmentCode);
            GL.CompileShader(fragment);

            uint shader = GL.CreateProgram();
            GL.AttachShader(shader, vertex);
            GL.AttachShader(shader, fragment);
            GL.LinkProgram(shader);

            GL.DetachShader(shader, vertex);
            GL.DetachShader(shader, fragment);
            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);

            return shader;
        }

        private void GenBuffers(List<float[]> vertices)
        {
            _VBOlenght = vertices.Count;
            float[] vboData = new float[vertices.Count * 6];

            for (int i = 0; i < vertices.Count; i++)
            {
                int index = i * 6;
                vboData[index + 0] = vertices[i][0];
                vboData[index + 1] = vertices[i][1];
                vboData[index + 2] = vertices[i][2];

                vboData[index + 3] = vertices[i][3];
                vboData[index + 4] = vertices[i][4];
                vboData[index + 5] = vertices[i][5];
            }

            _cubeVAO = new uint[1];
            GL.GenVertexArrays(1, _cubeVAO);
            GL.BindVertexArray(_cubeVAO[0]);

            _VBO = new uint[1];
            GL.GenBuffers(1, _VBO);

            GL.BindBuffer(GL_ARRAY_BUFFER, _VBO[0]);
            GL.BufferData(GL_ARRAY_BUFFER, vboData, GL_STATIC_DRAW);

            // position attribute
            GL.VertexAttribPointer(0, 3, GL_FLOAT, false, 6 * sizeof(float), IntPtr.Zero);
            GL.EnableVertexAttribArray(0);
            // normal attribute
            GL.VertexAttribPointer(1, 3, GL_FLOAT, false, 6 * sizeof(float), IntPtr.Zero + (3 * sizeof(float)));
            GL.EnableVertexAttribArray(1);

            _lightVAO = new uint[1];
            GL.GenVertexArrays(1, _lightVAO);
            GL.BindVertexArray(_lightVAO[0]);

            GL.BindBuffer(GL_ARRAY_BUFFER, _VBO[0]);
            GL.VertexAttribPointer(0, 3, GL_FLOAT, false, 6 * sizeof(float), IntPtr.Zero);
            GL.EnableVertexAttribArray(0);
        }

        private void GenTextureBuffers()
        {
            float[] vboData = new float[]
            {
                //X    Y   U     V
                -1f,  1f, 0f, 0f, 1f, //x0
                -1f, -1f, 0f, 0f, 0f, //x1
                 1f, -1f, 0f, 1f, 0f, //x2
                 1f,  1f, 0f, 1f, 1f, //x3
            };

            int[] iboData = new int[]
            {
                0, 1, 2,
                2, 3, 0,
            };

            _buffers = new uint[2];
            GL.GenBuffers(2, _buffers);

            GL.BindBuffer(GL_ARRAY_BUFFER, _buffers[0]);
            GL.BufferData(GL_ARRAY_BUFFER, vboData, GL_STATIC_DRAW);
            GL.VertexAttribPointer(0, 3, GL_FLOAT, false, 5 * 4, IntPtr.Zero);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, _buffers[1]);

            IntPtr ptr = Marshal.AllocHGlobal(iboData.Length * sizeof(float));
            Marshal.Copy(iboData, 0, ptr, iboData.Length);
            GL.BufferData(GL_ELEMENT_ARRAY_BUFFER, iboData.Length * 4, ptr, GL_STATIC_DRAW);
            Marshal.FreeHGlobal(ptr);

            GL.VertexAttribPointer(1, 2, GL_FLOAT, true, 5 * 4, IntPtr.Zero + (3 * 4));
            GL.EnableVertexAttribArray(1);
        }

        private void GenTexturedFBO()
        {
            //fbo
            _FBO = new uint[1];
            GL.GenFramebuffersEXT(1, _FBO);
            GL.BindFramebufferEXT(GL_FRAMEBUFFER_EXT, _FBO[0]);

            //texture
            _texture = new uint[1];
            GL.GenTextures(1, _texture);
            GL.BindTexture(GL_TEXTURE_2D, _texture[0]);
            GL.TexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, GetWidth(), GetHeight(), 0, GL_RGBA, GL_UNSIGNED_BYTE, IntPtr.Zero);
            GL.TexParameterI(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, new uint[] { GL_NEAREST });
            GL.TexParameterI(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, new uint[] { GL_NEAREST });

            GL.FramebufferTexture2DEXT(GL_FRAMEBUFFER_EXT, GL_COLOR_ATTACHMENT0_EXT, GL_TEXTURE_2D, _texture[0], 0);

            _depthrenderbuffer = new uint[1];
            GL.GenRenderbuffersEXT(1, _depthrenderbuffer);
            GL.BindRenderbufferEXT(GL_RENDERBUFFER_EXT, _depthrenderbuffer[0]);
            GL.RenderbufferStorageEXT(GL_RENDERBUFFER_EXT, GL_DEPTH_COMPONENT, GetWidth(), GetHeight());
            GL.BindRenderbufferEXT(GL_RENDERBUFFER_EXT, 0);

            GL.FramebufferRenderbufferEXT(GL_FRAMEBUFFER_EXT, GL_DEPTH_ATTACHMENT_EXT, GL_RENDERBUFFER_EXT,
                    _depthrenderbuffer[0]);

            if (GL.CheckFramebufferStatusEXT(GL_FRAMEBUFFER_EXT) != GL_FRAMEBUFFER_COMPLETE_EXT)
                Console.WriteLine("fbo fail!");
        }

        private void BindCubeBuffer()
        {
            GL.BindVertexArray(_cubeVAO[0]);
        }

        private void BindLampBuffer()
        {
            GL.BindVertexArray(_lightVAO[0]);
        }

        private void BindTexture()
        {
            GL.BindTexture(GL_TEXTURE_2D, _texture[0]);
        }

        private void UnbindFBO()
        {
            GL.BindFramebufferEXT(GL_FRAMEBUFFER_EXT, 0);
        }
    }
}
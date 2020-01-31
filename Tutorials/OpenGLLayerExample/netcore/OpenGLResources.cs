using System.Collections.Generic;

namespace OpenGLLayerExample
{
    public static class OpenGLResources
    {
        public static string GetCommonVertexShaderCode()
        {
            return
                "#version 330 core\n" +

                "layout(location = 0) in vec3 vPosition;\n" +
                "layout(location = 1) in vec3 vNormal;\n" +

                "out vec3 FragPos;\n" +
                "out vec3 Normal;\n" +

                "uniform mat4 model;\n" +
                "uniform mat4 view;\n" +
                "uniform mat4 projection;\n" +

                "void main()\n" +
                "{\n" +
                    "FragPos = vec3(model * vec4(vPosition, 1.0));\n" +
                    "Normal = vNormal;\n" +
                    "gl_Position = projection * view * vec4(FragPos, 1.0);\n" +
                "}\n";
        }

        public static string GetCommonFragmentShaderCode()
        {
            return
                "#version 330 core\n" +

                "in vec3 FragPos;\n" +
                "in vec3 Normal;\n" +

                "uniform vec3 lightPos;\n" +
                "uniform vec3 lightColor;\n" +
                "uniform vec3 objectColor;\n" +

                "out vec4 color;\n" +

                "void main()\n" +
                "{\n" +
                    "float ambientStrength = 0.1;\n" +
                    "vec3 ambient = ambientStrength * lightColor;\n" +
                    "vec3 norm = normalize(Normal);\n" +
                    "vec3 lightDir = normalize(lightPos - FragPos);\n" +
                    "float diff = max(dot(norm, lightDir), 0.0);\n" +
                    "vec3 diffuse = diff * lightColor;\n" +
                    "vec3 result = (ambient + diffuse) * objectColor;\n" +
                    "color = vec4(result, 1.0);\n" +
                "}\n";
        }

        public static string GetLampVertexShaderCode()
        {
            return
                "#version 330 core\n" +

                "layout(location = 0) in vec3 vPosition;\n" +

                "void main()\n" +
                "{\n" +
                    "gl_Position = vec4(vPosition, 1.0f);\n" +
                "}\n";
        }

        public static string GetLampFragmentShaderCode()
        {
            return
                "#version 330 core\n" +

                "out vec4 color;\n" +

                "void main()\n" +
                "{\n" +
                    "color = vec4(1.0);\n" +
                "}\n";
        }

        public static string GetTextureVertexShaderCode()
        {
            return
                "#version 330 core\n" +

                "layout (location = 0) in vec3 vert;\n" +
                "layout (location = 1) in vec2 verTexCoord;\n" +

                "out vec2 fragTexCoord;\n" +

                "void main()\n" +
                "{\n" +
                    "fragTexCoord = verTexCoord;\n" +
                    "gl_Position = vec4(vert, 1.0f);\n" +
                "}\n";
        }

        public static string GetTextureFragmentShaderCode()
        {
            return
                "#version 330 core\n" +

                "uniform sampler2D tex;\n" +

                "in vec2 fragTexCoord;\n" +
                "out vec4 color;\n" +
                
                "void main()\n" +
                "{\n" +
                    "color = texture(tex, fragTexCoord);\n" +
                "}\n";
        }

        public static List<float[]> Get3DCubeVertex()
        {
            return new List<float[]> {
                new float[] {-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f},
                new float[] { 0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f},
                new float[] { 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f},
                new float[] { 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f},
                new float[] {-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f},
                new float[] {-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f},

                new float[] {-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f},
                new float[] { 0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f},
                new float[] { 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f},
                new float[] { 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f},
                new float[] {-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f},
                new float[] {-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f},

                new float[] {-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f},
                new float[] {-0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f},
                new float[] {-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f},
                new float[] {-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f},
                new float[] {-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f},
                new float[] {-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f},

                new float[] { 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f},
                new float[] { 0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f},
                new float[] { 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f},
                new float[] { 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f},
                new float[] { 0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f},
                new float[] { 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f},

                new float[] {-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f},
                new float[] { 0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f},
                new float[] { 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f},
                new float[] { 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f},
                new float[] {-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f},
                new float[] {-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f},

                new float[] {-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f},
                new float[] { 0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f},
                new float[] { 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f},
                new float[] { 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f},
                new float[] {-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f},
                new float[] {-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f}
            };
        }
    }
}
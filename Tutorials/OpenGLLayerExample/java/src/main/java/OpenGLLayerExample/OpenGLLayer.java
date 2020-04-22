package OpenGLLayerExample;

import java.util.List;
import java.awt.*;

import static org.lwjgl.opengl.GL30.glBindVertexArray;
import static org.lwjgl.opengl.GL30.glGenVertexArrays;
import static org.lwjgl.opengl.GL30.glDeleteVertexArrays;
import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.opengl.GL15.*;
import static org.lwjgl.opengl.GL20.*;
import static org.lwjgl.opengl.EXTFramebufferObject.*;
import static org.lwjgl.opengl.GL15.glGenBuffers;

//GLM can be found at https://mvnrepository.com/artifact/io.github.java-graphics/glm/1.0.1
import glm.mat._4.Mat4;
import glm.vec._3.Vec3;

import com.spvessel.spacevil.Prototype;
import com.spvessel.spacevil.Flags.KeyCode;
import com.spvessel.spacevil.Flags.SizePolicy;
import com.spvessel.spacevil.Common.*;
import com.spvessel.spacevil.Core.*;

// implement InterfaceDraggable to recieve drag events
public class OpenGLLayer extends Prototype implements InterfaceOpenGLLayer, InterfaceDraggable {

    private static int _count = 0;

    public OpenGLLayer() {
        setItemName(this.getClass().getSimpleName() + _count++);

        // attr
        setBackground(75, 75, 75);
        setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
    }

    @Override
    public void initElements() {
        // assign events

        // rotate cube
        eventKeyPress.add((sender, args) -> {
            if (args.key.getValue() < KeyCode.RIGHT.getValue() || args.key.getValue() > KeyCode.UP.getValue())
                return;

            rotate(args.key);
        });
        eventMousePress.add((sender, args) -> {
            _ptr.setPosition(args.position.getX(), args.position.getY());
        });
        eventMouseDrag.add((sender, args) -> {
            float xRot = (float) (args.position.getX() - _ptr.getX()) / 2;
            _model = _model.rotate(radians(xRot), new Vec3(0.0f, 1.0f, 0.0f));

            float yRot = (float) (args.position.getY() - _ptr.getY()) / 2;
            _model = _model.rotate(radians(yRot), new Vec3(1.0f, 0.0f, 0.0f));

            _ptr.setPosition(args.position.getX(), args.position.getY());
        });

        // zoom scene
        eventScrollUp.add((sender, args) -> {
            _zCamera -= 0.2f;
            if (_zCamera < 2)
                _zCamera = 2;
            setCameraLookAt(_xCamera, _yCamera, _zCamera);
        });
        eventScrollDown.add((sender, args) -> {
            _zCamera += 0.2f;
            setCameraLookAt(_xCamera, _yCamera, _zCamera);
        });
    }

    private Position _ptr = new Position();

    // shaders
    private int _shaderCommon = 0;
    private int _shaderLamp = 0;
    private int _shaderTexture = 0;

    // resources
    private int _VBO;
    private int _cubeVAO;
    private int _lightVAO;
    private int _texture;
    private int[] _buffers = new int[2];
    private int _FBO;
    private int _depthrenderbuffer;
    private int _VBOlenght = 0;

    // cube color
    private Color _color;

    // matrices
    private Mat4 _projection;
    private Mat4 _view;
    private Mat4 _model;

    // view attr
    private float _aspectRatio = 1.0f;
    private float _xCamera = 0;
    private float _yCamera = 0;
    private float _zCamera = 3;

    // InterfaceOpenGLLayer init flag
    private boolean _isInit = false;

    @Override
    public void free() {
        _isInit = false;
        glDeleteProgram(_shaderCommon);
        glDeleteProgram(_shaderLamp);
        glDeleteProgram(_shaderTexture);
        glDeleteBuffers(_VBO);
        glDeleteVertexArrays(_cubeVAO);
        glDeleteVertexArrays(_lightVAO);
    }

    @Override
    public void initialize() {
        // configure OpenGL
        glDisable(GL_CULL_FACE);

        // creating shaders
        _shaderCommon = createShaderProgram(OpenGLResources.getCommonVertexShaderCode(),
                OpenGLResources.getCommonFragmentShaderCode());

        _shaderLamp = createShaderProgram(OpenGLResources.getLampVertexShaderCode(),
                OpenGLResources.getLampFragmentShaderCode());

        _shaderTexture = createShaderProgram(OpenGLResources.getTextureVertexShaderCode(),
                OpenGLResources.getTextureFragmentShaderCode());

        // gen buffers for cube
        genBuffers(OpenGLResources.get3DCubeVertex());
        _color = new Color(10, 162, 232);

        // preparing MVP matrices
        _projection = new Mat4().perspective(radians(45f), _aspectRatio, 0.1f, 100.0f);
        _model = new Mat4().identity();
        setCameraLookAt(_xCamera, _yCamera, _zCamera);

        // set init flag
        _isInit = true;
    }

    @Override
    public boolean isInitialized() {
        return _isInit;
    }

    @Override
    public void draw() {
        // it is example with using a second FBO
        // gen FBO
        genTexturedFBO();
        // set scene viewport according to items size
        glViewport(0, 0, getWidth(), getHeight());
        // crear color and depth bits
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        /////////////////
        // draw cube with light
        glUseProgram(_shaderCommon);
        UniformSender.sendColorAsUniformVariable(_shaderCommon, Color.white, "lightColor");
        UniformSender.sendVec3AsUniformVariable(_shaderCommon, new float[] { 1.2f, 1.0f, 2.0f }, "lightPos");
        UniformSender.sendMVPAsUniformVariable(_shaderCommon, _model, _view, _projection);
        bindCubeBuffer();
        UniformSender.sendColorAsUniformVariable(_shaderCommon, _color, "objectColor");
        glDrawArrays(GL_TRIANGLES, 0, _VBOlenght);

        // optionally: draw black edges
        UniformSender.sendColorAsUniformVariable(_shaderCommon, Color.black, "objectColor");
        glDrawArrays(GL_LINE_STRIP, 0, _VBOlenght);

        // using light of lamp
        glUseProgram(_shaderLamp);
        bindLampBuffer();
        glDrawArrays(GL_TRIANGLES, 0, _VBOlenght);
        /////////////////

        // unbind second FBO and restore viewport to current window
        unbindFBO();
        RenderService.setGLLayerViewport(getHandler(), this);

        // draw FBO texture
        glUseProgram(_shaderTexture);
        genTextureBuffers();
        bindTexture();

        UniformSender.sendUniformSample2D(_shaderTexture, "tex");
        glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);

        // delete resources
        glDeleteFramebuffersEXT(_FBO);
        glDeleteRenderbuffersEXT(_depthrenderbuffer);
        glDeleteTextures(_texture);
        glDeleteBuffers(_buffers[0]);
        glDeleteBuffers(_buffers[1]);
        glDisableVertexAttribArray(0);
        glDisableVertexAttribArray(1);
    }

    public void refresh() {
        if (!_isInit)
            return;

        _projection = new Mat4().perspective(radians(45f), _aspectRatio, 0.1f, 100.0f);
    }

    public void restoreView() {
        _xCamera = 0;
        _yCamera = 0;
        _zCamera = 3;
        _projection = new Mat4().perspective(radians(45f), _aspectRatio, 0.1f, 100.0f);
        _model = new Mat4().identity();
        setCameraLookAt(_xCamera, _yCamera, _zCamera);
    }

    public void setZoom(float value) {
        _zCamera = value;
        setCameraLookAt(_xCamera, _yCamera, _zCamera);
    }

    public void resize() {
        _aspectRatio = (float) getWidth() / getHeight();
        refresh();
    }

    @Override
    public void setWidth(int width) {
        super.setWidth(width);
        resize();
    }

    @Override
    public void setHeight(int height) {
        super.setHeight(height);
        resize();
    }

    private void setCameraLookAt(float x, float y, float z) {
        _view = new Mat4().lookAt(new Vec3(x, y, z), new Vec3(0f, 0f, 0f), new Vec3(0f, 1f, 0f));
    }

    public void rotate(KeyCode code) {
        if (code == KeyCode.LEFT) {
            _model = _model.rotate(radians(1), new Vec3(0.0f, 1.0f, 0.0f));
        }
        if (code == KeyCode.RIGHT) {
            _model = _model.rotate(radians(-1), new Vec3(0.0f, 1.0f, 0.0f));
        }
        if (code == KeyCode.UP) {
            _model = _model.rotate(radians(1), new Vec3(1.0f, 0.0f, 0.0f));
        }
        if (code == KeyCode.DOWN) {
            _model = _model.rotate(radians(-1), new Vec3(1.0f, 0.0f, 0.0f));
        }
    }

    private float radians(float value) {
        return value * (float) Math.PI / 180f;
    }

    private int createShaderProgram(String vertexCode, String fragmentCode) {
        int vertex = glCreateShader(GL_VERTEX_SHADER);
        glShaderSource(vertex, vertexCode);
        glCompileShader(vertex);

        int fragment = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fragment, fragmentCode);
        glCompileShader(fragment);

        int shader = glCreateProgram();
        glAttachShader(shader, vertex);
        glAttachShader(shader, fragment);
        glLinkProgram(shader);

        glDetachShader(shader, vertex);
        glDetachShader(shader, fragment);
        glDeleteShader(vertex);
        glDeleteShader(fragment);

        return shader;
    }

    private void genBuffers(List<float[]> vertices) {
        _VBOlenght = vertices.size();
        float[] vboData = new float[vertices.size() * 6];

        for (int i = 0; i < vertices.size(); i++) {
            int index = i * 6;
            vboData[index + 0] = vertices.get(i)[0];
            vboData[index + 1] = vertices.get(i)[1];
            vboData[index + 2] = vertices.get(i)[2];
            vboData[index + 3] = vertices.get(i)[3];
            vboData[index + 4] = vertices.get(i)[4];
            vboData[index + 5] = vertices.get(i)[5];
        }

        _cubeVAO = glGenVertexArrays();
        _VBO = glGenBuffers();

        glBindBuffer(GL_ARRAY_BUFFER, _VBO);
        glBufferData(GL_ARRAY_BUFFER, vboData, GL_STATIC_DRAW);

        glBindVertexArray(_cubeVAO);

        // position attribute
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 6 * 4, 0);
        glEnableVertexAttribArray(0);
        // normal attribute
        glVertexAttribPointer(1, 3, GL_FLOAT, false, 6 * 4, 0 + (3 * 4));
        glEnableVertexAttribArray(1);

        _lightVAO = glGenVertexArrays();
        glBindVertexArray(_lightVAO);

        glBindBuffer(GL_ARRAY_BUFFER, _VBO);
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 6 * 4, 0);
        glEnableVertexAttribArray(0);
    }

    private void genTextureBuffers() {
        float[] vboData = new float[] {
                //X     Y      U      V
                -1f, 1f, 0f, 0f, 1f, //x0
                -1f, -1f, 0f, 0f, 0f, //x1
                1f, -1f, 0f, 1f, 0f, //x2
                1f, 1f, 0f, 1f, 1f, //x3
        };

        int[] iboData = new int[] { 0, 1, 2, 2, 3, 0, };

        _buffers[0] = glGenBuffers();
        glBindBuffer(GL_ARRAY_BUFFER, _buffers[0]);
        glBufferData(GL_ARRAY_BUFFER, vboData, GL_STATIC_DRAW);
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 5 * 4, 0);
        glEnableVertexAttribArray(0);

        _buffers[1] = glGenBuffers();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _buffers[1]);
        glBufferData(GL_ELEMENT_ARRAY_BUFFER, iboData, GL_STATIC_DRAW);
        glVertexAttribPointer(1, 2, GL_FLOAT, true, 5 * 4, (3 * 4));
        glEnableVertexAttribArray(1);
    }

    private void genTexturedFBO() {
        //fbo
        _FBO = glGenFramebuffersEXT();
        glBindFramebufferEXT(GL_FRAMEBUFFER_EXT, _FBO);

        //texture
        _texture = glGenTextures();
        glBindTexture(GL_TEXTURE_2D, _texture);
        glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, getWidth(), getHeight(), 0, GL_RGBA, GL_UNSIGNED_BYTE, 0);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

        glFramebufferTexture2DEXT(GL_FRAMEBUFFER_EXT, GL_COLOR_ATTACHMENT0_EXT, GL_TEXTURE_2D, _texture, 0);

        _depthrenderbuffer = glGenRenderbuffersEXT();
        glBindRenderbufferEXT(GL_RENDERBUFFER_EXT, _depthrenderbuffer);
        glRenderbufferStorageEXT(GL_RENDERBUFFER_EXT, GL_DEPTH_COMPONENT, getWidth(), getHeight());
        glBindRenderbufferEXT(GL_RENDERBUFFER_EXT, 0);

        glFramebufferRenderbufferEXT(GL_FRAMEBUFFER_EXT, GL_DEPTH_ATTACHMENT_EXT, GL_RENDERBUFFER_EXT,
                _depthrenderbuffer);

        if (glCheckFramebufferStatusEXT(GL_FRAMEBUFFER_EXT) != GL_FRAMEBUFFER_COMPLETE_EXT)
            System.out.println("fbo fail!");
    }

    private void bindCubeBuffer() {
        glBindVertexArray(_cubeVAO);
    }

    private void bindLampBuffer() {
        glBindVertexArray(_lightVAO);
    }

    private void bindTexture() {
        glBindTexture(GL_TEXTURE_2D, _texture);
    }

    private void unbindFBO() {
        glBindFramebufferEXT(GL_FRAMEBUFFER_EXT, 0);
    }
}
package OpenGLLayerExample;

import java.awt.Color;
import java.nio.FloatBuffer;

import org.lwjgl.BufferUtils;

import static org.lwjgl.opengl.GL20.*;

//GLM can be found at https://mvnrepository.com/artifact/io.github.java-graphics/glm/1.0.1
import glm.mat._4.Mat4;

public final class UniformSender {

    private UniformSender() {
    }

    public static void sendColorAsUniformVariable(int shader, Color fill, String name) {
        float[] argb = { (float) fill.getRed() / 255.0f, (float) fill.getGreen() / 255.0f,
                (float) fill.getBlue() / 255.0f };

        int location = glGetUniformLocation(shader, name);
        if (location < 0)
            System.out.println("Uniform <" + name + "> not found.");

        glUniform3f(location, argb[0], argb[1], argb[2]);
    }

    public static void sendVec3AsUniformVariable(int shader, float[] value, String name) {
        int location = glGetUniformLocation(shader, name);
        if (location < 0)
            System.out.println("Uniform <" + name + "> not found.");
        glUniform3f(location, value[0], value[1], value[2]);
    }

    public static void sendMVPAsUniformVariable(int shader, Mat4 model, Mat4 view, Mat4 projection) {
        int location = glGetUniformLocation(shader, "model");
        if (location < 0)
            System.out.println("Uniform <model> not found.");
        glUniformMatrix4fv(location, false, toFloatBuffer(model));

        location = glGetUniformLocation(shader, "view");
        if (location < 0)
            System.out.println("Uniform <view> not found.");
        glUniformMatrix4fv(location, false, toFloatBuffer(view));

        location = glGetUniformLocation(shader, "projection");
        if (location < 0)
            System.out.println("Uniform <projection> not found.");
        glUniformMatrix4fv(location, false, toFloatBuffer(projection));
    }

    public static void sendUniformSample2D(int shader, String name) {
        int location = glGetUniformLocation(shader, name);
        if (location < 0)
            System.out.println("Uniform <" + name + "> not found.");

        glUniform1i(location, 0);
    }

    private static FloatBuffer toFloatBuffer(Mat4 mat) {
        FloatBuffer bb = BufferUtils.createFloatBuffer(4 * 4);
        float[] array = mat.toFa_();
        bb.put(array);
        bb.rewind();
        return bb;
    }
}

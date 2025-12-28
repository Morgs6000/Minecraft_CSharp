using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Window : GameWindow
{
    private Shader shader;
    private Texture texture;
    private Mesh mesh;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        string vertexPath = "res/shaders/default/vertex.glsl";
        string fragmentPath = "res/shaders/default/fragment.glsl";
        shader = new Shader(vertexPath, fragmentPath);

        string texturePath = "res/textures/container.jpg";
        texture = new Texture(texturePath);

        mesh = new Mesh();
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 1.0f);

        mesh.Begin();

        mesh.Tex(0.0f, 1.0f);
        mesh.Vertex(-0.5f, -0.5f, 0.0f);
        mesh.Tex(1.0f, 1.0f);
        mesh.Vertex( 0.5f, -0.5f, 0.0f);
        mesh.Tex(1.0f, 0.0f);
        mesh.Vertex( 0.5f,  0.5f, 0.0f);
        mesh.Tex(0.0f, 0.0f);
        mesh.Vertex(-0.5f,  0.5f, 0.0f);

        mesh.End();

        // GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();

        Matrix4 model = Matrix4.Identity;
        model *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), MathHelper.DegreesToRadians(-55.0f));
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -3.0f));
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), (float)ClientSize.X / (float)ClientSize.Y, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);

        texture.Bind();

        mesh.Draw(shader);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }
}

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Camera
{
    private Vector3 Position;
    private Vector3 Front;
    private Vector3 Up;

    private float MovementSpeed;
    
    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    public Camera()
    {
        Position = new Vector3(0.0f, 0.0f, 3.0f);
        Front    = new Vector3(0.0f, 0.0f, -1.0f);
        Up       = new Vector3(0.0f, 1.0f, 0.0f);

        MovementSpeed = 2.5f;
    }

    public void Update(GameWindow gameWindow)
    {
        KeyboardState keyboardState = gameWindow.KeyboardState;

        ProcessTime();
        ProcessKeyboard(keyboardState);
    }

    private void ProcessTime()
    {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;
    }
    
    private void ProcessKeyboard(KeyboardState keyboardState)
    {
        float speed = MovementSpeed * deltaTime;

        if (keyboardState.IsKeyDown(Keys.W))
        {
            Position += speed * Vector3.Normalize(new Vector3(Front.X, 0.0f, Front.Z));
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            Position -= speed * Vector3.Normalize(new Vector3(Front.X, 0.0f, Front.Z));
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            Position -= speed * Vector3.Normalize(Vector3.Cross(Front, Up));
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            Position += speed * Vector3.Normalize(Vector3.Cross(Front, Up));
        }
        if (keyboardState.IsKeyDown(Keys.Space))
        {
            Position += speed * Up;
        }
        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            Position -= speed * Up;
        }
    }

    public Matrix4 LookAt()
    {
        Vector3 eye = Position;
        Vector3 target = Position + Front;
        Vector3 up = Up;

        return Matrix4.LookAt(eye, target, up);
    }
    
    public Matrix4 CreatePerspectiveFieldOfView(GameWindow gameWindow)
    {
        float fovy = MathHelper.DegreesToRadians(70.0f);
        float aspect = (float)gameWindow.ClientSize.X / (float)gameWindow.ClientSize.Y;
        float depthNear = 0.05f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }
}

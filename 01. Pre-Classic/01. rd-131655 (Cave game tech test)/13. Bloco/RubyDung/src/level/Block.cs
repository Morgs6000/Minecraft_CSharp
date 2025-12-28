namespace RubyDung;

public class Block
{
    public static Block block = new Block();

    public void Load(Mesh mesh)
    {
        float x0 = -0.5f;
        float y0 = -0.5f;
        float z0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;
        float z1 = 0.5f;

        float u0 = 0.0f;
        float v0 = 0.0f;

        float u1 = 1.0f;
        float v1 = 1.0f;

        mesh.Tex(u0, v1);
        mesh.Vertex(x0, y0, z0);
        mesh.Tex(u1, v1);
        mesh.Vertex(x0, y0, z1);
        mesh.Tex(u1, v0);
        mesh.Vertex(x0, y1, z1);
        mesh.Tex(u0, v0);
        mesh.Vertex(x0, y1, z0);

        mesh.Tex(u0, v1);
        mesh.Vertex(x1, y0, z1);
        mesh.Tex(u1, v1);
        mesh.Vertex(x1, y0, z0);
        mesh.Tex(u1, v0);
        mesh.Vertex(x1, y1, z0);
        mesh.Tex(u0, v0);
        mesh.Vertex(x1, y1, z1);

        mesh.Tex(u0, v1);
        mesh.Vertex(x0, y0, z0);
        mesh.Tex(u1, v1);
        mesh.Vertex(x1, y0, z0);
        mesh.Tex(u1, v0);
        mesh.Vertex(x1, y0, z1);
        mesh.Tex(u0, v0);
        mesh.Vertex(x0, y0, z1);

        mesh.Tex(u0, v1);
        mesh.Vertex(x0, y1, z1);
        mesh.Tex(u1, v1);
        mesh.Vertex(x1, y1, z1);
        mesh.Tex(u1, v0);
        mesh.Vertex(x1, y1, z0);
        mesh.Tex(u0, v0);
        mesh.Vertex(x0, y1, z0);

        mesh.Tex(u0, v1);
        mesh.Vertex(x1, y0, z0);
        mesh.Tex(u1, v1);
        mesh.Vertex(x0, y0, z0);
        mesh.Tex(u1, v0);
        mesh.Vertex(x0, y1, z0);
        mesh.Tex(u0, v0);
        mesh.Vertex(x1, y1, z0);

        mesh.Tex(u0, v1);
        mesh.Vertex(x0, y0, z1);
        mesh.Tex(u1, v1);
        mesh.Vertex(x1, y0, z1);
        mesh.Tex(u1, v0);
        mesh.Vertex(x1, y1, z1);
        mesh.Tex(u0, v0);
        mesh.Vertex(x0, y1, z1);
    }
}

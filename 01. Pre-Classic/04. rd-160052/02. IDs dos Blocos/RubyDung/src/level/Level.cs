using System.IO.Compression;

namespace RubyDung;

public class Level
{
    public readonly int width;
    public readonly int height;
    public readonly int depth;

    private byte[,,] blocks;

    public Level(int w, int h, int d)
    {
        width = w;
        height = h;
        depth = d;

        blocks = new byte[w, h, d];

        bool mapLoaded = Load();

        if (!mapLoaded)
        {
            GenerateMap();
        }
    }
    
    private void GenerateMap()
    {
        int w = width;
        int h = height;
        int d = depth;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                for (int z = 0; z < d; z++)
                {
                    int id = 0;

                    int dh = h * 2 / 3;

                    if (y == dh)
                    {
                        id = Block.grass.id;
                    }
                    if (y < dh)
                    {
                        id = Block.dirt.id;
                    }
                    if (y <= dh - 5)
                    {
                        id = Block.rock.id;
                    }                    

                    blocks[x, y, z] = (byte)id;
                }
            }
        }
    }

    public bool Load()
    {
        try
        {
            string file = "level.dat";
            string filePath = $"save/{file}";
            string directoryPath = Path.GetDirectoryName(filePath);

            // Cria o diretório se não existir
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Debug.Log($"Diretório criado: {directoryPath}");
            }
            if (!File.Exists(filePath))
            {
                Debug.LogWarning("Arquivo 'level.dat' não encontrado. Criando novo nível.");

                return false;
            }
            
            using(FileStream fs = new FileStream(filePath, FileMode.Open))
            using(GZipStream gzip = new GZipStream(fs, CompressionMode.Decompress))
            using (BinaryReader dis = new BinaryReader(gzip))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            blocks[x, y, z] = dis.ReadByte();
                        }
                    }
                }

                dis.Close();

                Debug.LogSuccess("Nível carregado com sucesso!");

                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao carregar nível: {e.Message}");
            Debug.LogError(e.StackTrace);
            // throw;

            return false;
        }
    }

    public void Save()
    {
        try
        {
            string file = "level.dat";
            string filePath = $"save/{file}";
            string directoryPath = Path.GetDirectoryName(filePath);

            // Cria o diretório se não existir
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Debug.Log($"Diretório criado: {directoryPath}");
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (GZipStream gzip = new GZipStream(fs, CompressionMode.Compress))
            using (BinaryWriter dos = new BinaryWriter(gzip))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            dos.Write(blocks[x, y, z]);
                        }
                    }
                }

                dos.Close();

                Debug.LogSuccess("Nível salvo com sucesso!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao salvar nível: {e.Message}");
            Debug.LogError(e.StackTrace);
            throw;
        }
    }

    public List<AABB> GetCubes(AABB other)
    {
        List<AABB> cubes = new List<AABB>();

        int x0 = (int)other.x0;
        int y0 = (int)other.y0;
        int z0 = (int)other.z0;

        int x1 = (int)(other.x1 + 1.0F);
        int y1 = (int)(other.y1 + 1.0F);
        int z1 = (int)(other.z1 + 1.0F);

        if (x0 < 0)
        {
            x0 = 0;
        }
        if (y0 < 0)
        {
            y0 = 0;
        }
        if (z0 < 0)
        {
            z0 = 0;
        }

        if (x1 > this.width)
        {
            x1 = this.width;
        }
        if (y1 > this.height)
        {
            y1 = this.height;
        }
        if (z1 > this.depth)
        {
            z1 = this.depth;
        }

        for (int x = x0; x < x1; ++x)
        {
            for (int y = y0; y < y1; ++y)
            {
                for (int z = z0; z < z1; ++z)
                {
                    Block block = Block.blocks[GetBlock(x, y, z)];

                    if (block != null)
                    {
                        cubes.Add(block.GetAABB(x, y, z));
                    }
                }
            }
        }

        return cubes;
    }
    
    public void SetBlock(int x, int y, int z, int type)
    {
        if (x >= 0 && x < width &&
            y >= 0 && y < height &&
            z >= 0 && z < depth)
        {
            blocks[x, y, z] = (byte)type;
        }
    }

    public int GetBlock(int x, int y, int z)
    {
        if (x >= 0 && x < width &&
            y >= 0 && y < height &&
            z >= 0 && z < depth)
        {
            return blocks[x, y, z];
        }
        
        return 0;
    }

    public bool IsSolidBlock(int x, int y, int z)
    {
        Block block = Block.blocks[GetBlock(x, y, z)];

        return block == null ? false : block.IsSolid();
    }
}

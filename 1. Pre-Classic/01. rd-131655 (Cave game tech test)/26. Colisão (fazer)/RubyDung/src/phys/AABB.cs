namespace RubyDung.phys;

/* Axis-Aligned Bounding Box */

public class AABB
{
    public float x0;
    public float y0;
    public float z0;

    public float x1;
    public float y1;
    public float z1;

    public AABB(float x0, float y0, float z0, float x1, float y1, float z1)
    {
        this.x0 = x0;
        this.y0 = y0;
        this.z0 = z0;

        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
    }
    
    public bool Intersects(AABB other)
    {
        return this.x0 < other.x1 && this.x1 > other.x0 &&
               this.y0 < other.y1 && this.y1 > other.y0 &&
               this.z0 < other.z1 && this.z1 > other.z0;
    }
    
    public float ClipXCollide(AABB other)
    {
        if (other.y1 > this.y0 && other.y0 < this.y1 && other.z1 > this.z0 && other.z0 < this.z1)
        {
            if (this.x1 > other.x0 && this.x1 <= other.x1)
            {
                // Colisão pelo lado direito deste AABB com o lado esquerdo do outro
                return other.x0 - (this.x1 - this.x0);
            }
            
            if (this.x0 < other.x1 && this.x0 >= other.x0)
            {
                // Colisão pelo lado esquerdo deste AABB com o lado direito do outro
                return other.x1;
            }
        }
        
        return this.x0;
    }
    
    public float ClipYCollide(AABB other)
    {
        if (other.x1 > this.x0 && other.x0 < this.x1 && other.z1 > this.z0 && other.z0 < this.z1)
        {
            if (this.y1 > other.y0 && this.y1 <= other.y1)
            {
                // Colisão pela parte de baixo deste AABB com a parte de cima do outro
                return other.y0 - (this.y1 - this.y0);
            }
            
            if (this.y0 < other.y1 && this.y0 >= other.y0)
            {
                // Colisão pela parte de cima deste AABB com a parte de baixo do outro
                return other.y1;
            }
        }
        
        return this.y0;
    }
    
    public float ClipZCollide(AABB other)
    {
        if (other.x1 > this.x0 && other.x0 < this.x1 && other.y1 > this.y0 && other.y0 < this.y1)
        {
            if (this.z1 > other.z0 && this.z1 <= other.z1)
            {
                // Colisão pela frente deste AABB com a parte de trás do outro
                return other.z0 - (this.z1 - this.z0);
            }
            
            if (this.z0 < other.z1 && this.z0 >= other.z0)
            {
                // Colisão por trás deste AABB com a frente do outro
                return other.z1;
            }
        }
        
        return this.z0;
    }
}

namespace U3DSpace.Primitives
{
    public class Shader
    {
        #region Constructors

        public Shader()
        {
            Name = string.Empty;
            Material = string.Empty;
            Texture = string.Empty;
        }

        public Shader(string name, string material, string texture)
        {
            Name = name;
            Material = material;
            Texture = texture;
        }

        #endregion Constructors

        #region Properties

        /// <summary>Use for identify this Shader</summary>
        public string Name { get; internal set; }

        /// <summary>Name of the Material from U3DDocument</summary>
        public string Material { get; internal set; }

        /// <summary>Name of the Texture from U3DDocument</summary>
        public string Texture { get; internal set; }

        #endregion Properties

        #region Methods

        public static bool operator !=(Shader a, Shader b)
        {
            return !(a == b);
        }

        public static bool operator ==(Shader a, Shader b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Shader shader)
            {
                if (Name == shader.Name &&
                    Material == shader.Material &&
                    Texture == shader.Texture)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Material.GetHashCode() * 3) + (Texture.GetHashCode() * 5) + (Name.GetHashCode() * 7);
        }

        #endregion Methods
    }
}
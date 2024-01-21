namespace Universal3d.Core.Primitives.MeshPrimitives;

public class Corner
{
    #region Constructors

    public Corner()
    {
        Position = -1;
        Normal = -1;
        TextureCoordinate = -1;
    }

    public Corner(int position, int normal, int textureCoordinate)
    {
        Position = position;
        Normal = normal;
        TextureCoordinate = textureCoordinate;
    }

    public Corner(Corner corner)
    {
        Position = corner.Position;
        Normal = corner.Normal;
        TextureCoordinate = corner.TextureCoordinate;
    }

    #endregion Constructors

    #region Properties

    /// <summary>Index of position</summary>
    public int Position { get; internal set; }

    /// <summary>Index of normal</summary>
    public int Normal { get; internal set; }

    /// <summary>Index of texture coordinate</summary>
    public int TextureCoordinate { get; internal set; }

    #endregion Properties

    #region Methods

    public static bool operator !=(Corner a, Corner b)
    {
        return !(a == b);
    }

    public static bool operator ==(Corner a, Corner b)
    {
        return a.Equals(b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Corner corner)
        {
            if (Position == corner.Position &&
                Normal == corner.Normal &&
                TextureCoordinate == corner.TextureCoordinate)
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
        return (Position * 3) + (Normal * 5) + (TextureCoordinate * 7);
    }

    #endregion Methods
}
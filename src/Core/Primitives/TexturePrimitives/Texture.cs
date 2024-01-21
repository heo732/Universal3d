using System;

namespace Universal3d.Core.Primitives.TexturePrimitives;
public class Texture
{
    #region Constructors

    public Texture()
    {
        Name = string.Empty;
        ImageFormat = ImageFormat.Invalid;
        Image = new byte[0];
    }

    public Texture(string name, ImageFormat imageFormat, byte[] image)
    {
        Name = name;
        ImageFormat = imageFormat;
        Image = new byte[image.Length];
        Array.Copy(image, Image, image.Length);
    }

    #endregion Constructors

    #region Properties

    /// <summary>Use for identify this Texture</summary>
    public string Name { get; internal set; }

    public ImageFormat ImageFormat { get; internal set; }

    /// <summary>Byte data that represent image</summary>
    public byte[] Image { get; internal set; }

    #endregion Properties

    #region Methods

    public static bool operator !=(Texture a, Texture b)
    {
        return !(a == b);
    }

    public static bool operator ==(Texture a, Texture b)
    {
        return a.Equals(b);
    }

    public override int GetHashCode()
    {
        return
            (Name.GetHashCode() * 3) +
            ((byte)ImageFormat * 5) +
            (Image.GetHashCode() * 7);
    }

    public override bool Equals(object obj)
    {
        if (obj is Texture texture)
        {
            if (Name == texture.Name &&
                ImageFormat == texture.ImageFormat &&
                Image.Equals(texture.Image))
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

    #endregion Methods
}

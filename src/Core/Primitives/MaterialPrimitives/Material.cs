using System;

namespace Universal3d.Core.Primitives.MaterialPrimitives;

public class Material
{
    #region Constructors

    public Material()
    {
        Name = string.Empty;
        Ambient = new Color();
        Diffuse = new Color();
        Specular = new Color();
        Emissive = new Color();
        Reflectivity = 0.0f;
        Opacity = 1.0f;
    }

    public Material(string name, Color ambient, Color diffuse, Color specular, Color emissive,
        float reflectivity, float opacity)
    {
        Name = name;
        Ambient = new Color(ambient);
        Diffuse = new Color(diffuse);
        Specular = new Color(specular);
        Emissive = new Color(emissive);
        Reflectivity = reflectivity;
        Opacity = opacity;
    }

    #endregion Constructors

    #region Properties

    public Color Ambient { get; internal set; }

    public Color Diffuse { get; internal set; }

    public Color Emissive { get; internal set; }

    public Color Specular { get; internal set; }

    /// <summary>Use for identify this Material</summary>
    public string Name { get; internal set; }

    public float Opacity { get; internal set; }
    public float Reflectivity { get; internal set; }

    #endregion Properties

    #region Methods

    public static bool operator !=(Material a, Material b)
    {
        return !(a == b);
    }

    public static bool operator ==(Material a, Material b)
    {
        return a.Equals(b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Material material)
        {
            if (Name == material.Name &&
                Ambient == material.Ambient &&
                Diffuse == material.Diffuse &&
                Specular == material.Specular &&
                Emissive == material.Emissive &&
                (Math.Abs(Reflectivity - material.Reflectivity) < 0.00005f) &&
                (Math.Abs(Opacity - material.Opacity) < 0.00005f))
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
        return (int)(
            (Name.GetHashCode() * 3) +
            (Ambient.GetHashCode() * 5) +
            (Diffuse.GetHashCode() * 7) +
            (Specular.GetHashCode() * 11) +
            (Emissive.GetHashCode() * 13) +
            (Reflectivity * 17) +
            (Opacity * 19));
    }

    #endregion Methods
}
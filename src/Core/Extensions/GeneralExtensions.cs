using System.Text;
using Universal3d.Core.Enums;

namespace Universal3d.Core.Extensions;
public static class GeneralExtensions
{
    public static Encoding ToSystemEncoding(this U3dCharacterEncoding encoding) => encoding switch
    {
        U3dCharacterEncoding.Utf8 => Encoding.UTF8,
        _ => Encoding.UTF8,
    };
}

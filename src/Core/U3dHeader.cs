using System.Collections.Generic;
using Universal3d.Core.Enums;

namespace Universal3d.Core;
public class U3dHeader
{
    public int Version { get; set; }
    public U3dProfileIdentifier ProfileIdentifier { get; set; } = U3dProfileIdentifier.NoCompressionMode;
    public uint DeclarationSize { get; set; }
    public ulong FileSize { get; set; }
    public U3dCharacterEncoding CharacterEncoding { get; set; }
    public List<U3dMetaItem> Meta { get; } = [];
}

using System.Collections.Generic;
using Universal3d.Core.Enums;

namespace Universal3d.Core;
public class U3dMetaItem
{
    public U3dMetaItemAttributes Attributes { get; set; }
    public string Key { get; set; }
    public string StringValue { get; set; }
    public List<byte> BinaryValue { get; } = [];
}

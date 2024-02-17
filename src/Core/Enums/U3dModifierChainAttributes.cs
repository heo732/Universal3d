using System;

namespace Universal3d.Core.Enums;
[Flags]
internal enum U3dModifierChainAttributes : uint
{
    BoundingSpherePresent = 0x00000001,
    AxisAlignedBoundingBoxPresent = 0x00000002,
}

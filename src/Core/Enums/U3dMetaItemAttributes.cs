using System;

namespace Universal3d.Core.Enums;
[Flags]
public enum U3dMetaItemAttributes : uint
{
    /// <summary>indicates the Value is formatted as a String</summary>
    String = 0x00000000,
    /// <summary>indicates the Value is formatted as a binary sequence</summary>
    BinarySequence = 0x00000001,
    /// <summary>indicates the Value is HIDDEN and should not be displayed by the viewer</summary>
    Hidden = 0x00000002,
    /// <summary>indicates that this meta data should be used when double-clicked</summary>
    UseOnDoubleClick = 0x00000010,
    /// <summary>indicates the Value should be displayed by the viewer in a right-click menu</summary>
    DisplayValue = 0x00000020,
    /// <summary>indicates the Key should be displayed by the viewer in a right-click menu</summary>
    DisplayKey = 0x00000040,
    /// <summary>indicates the Value is an ACTION and should executed by the viewer</summary>
    ActionValue = 0x00000100,
    /// <summary>indicates the Value is a FILE and should opened by the viewer</summary>
    FileValue = 0x00000200,
    /// <summary>indicates the Value is MIME DATA and should opened by the viewer</summary>
    MimeValue = 0x00000400,
}

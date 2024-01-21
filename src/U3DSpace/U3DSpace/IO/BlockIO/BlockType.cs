﻿namespace U3DSpace.IO.BlockIO
{
    public enum BlockType : uint
    {
        Invalid = 0x00000000,
        Header = 0x00443355,
        ModifierChain = 0xFFFFFF14,
        GroupNode = 0xFFFFFF21,
        ModelNode = 0xFFFFFF22,
        ShadingModifier = 0xFFFFFF45,
        MeshDeclaration = 0xFFFFFF31,
        MeshContinuation = 0xFFFFFF3B,
        LitTextureShader = 0xFFFFFF53,
        MaterialResource = 0xFFFFFF54,
        TextureDeclaration = 0xFFFFFF55,
        TextureContinuation = 0xFFFFFF5C
    }
}
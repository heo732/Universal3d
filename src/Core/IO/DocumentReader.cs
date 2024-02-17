using System.Collections.Generic;
using System.IO;
using System.Linq;
using Universal3d.Core.Enums;
using Universal3d.Core.IO.BlockIO;
using Universal3d.Core.Primitives.NodePrimitives;

namespace Universal3d.Core.IO;
internal class DocumentReader
{
    private readonly U3dDocument _doc;
    private readonly BinaryReader _reader;
    private readonly List<BlockReader> _topBlocks = [];

    private DocumentReader(U3dDocument doc, BinaryReader reader)
    {
        _doc = doc;
        _reader = reader;
    }

    #region PublicMethods

    public static U3dDocument Read(Stream stream, bool leaveOpen)
    {
        var doc = new U3dDocument();
        using var streamReader = new BinaryReader(stream, doc.TextEncoding, leaveOpen);
        var docReader = new DocumentReader(doc, streamReader);
        docReader.Parse();
        return doc;
    }

    #endregion PublicMethods

    #region PrivateMethods

    private void Parse()
    {
        ParseTopBlocks();
        ParseHeaderBlock();
        //ParseNodes();
        //ParseDeclarationsOfMeshes();
        //ParseShaders();
        //ParseMaterials();
        //ParseDeclarationsOfTextures();
        //ParseContinuationsOfMeshes();
        //ParseContinuationsOfTextures();
    }

    private void ParseTopBlocks()
    {
        _topBlocks.Clear();
        BlockReader currentBlock;

        do
        {
            currentBlock = BlockReader.Parse(_reader);
            if (currentBlock is not null) _topBlocks.Add(currentBlock);
        }
        while (_reader.BaseStream.Position < _reader.BaseStream.Length);
    }

    private void ParseHeaderBlock()
    {
        var r = _topBlocks.Single(x => x.Block.Type == BlockType.Header);

        if (r.TryReadI32(out var version)) _doc.Header.Version = version;
        if (r.TryReadU32(out var profileIdentifier)) _doc.Header.ProfileIdentifier = (U3dProfileIdentifier)profileIdentifier;
        if (r.TryReadU32(out var declarationSize)) _doc.Header.DeclarationSize = declarationSize;
        if (r.TryReadU64(out var fileSize)) _doc.Header.FileSize = fileSize;
        if (r.TryReadU32(out var characterEncoding)) _doc.Header.CharacterEncoding = (U3dCharacterEncoding)characterEncoding;
        
        _doc.Header.Meta.Clear();
        _doc.Header.Meta.AddRange(r.ReadMeta(_doc.TextEncoding));
    }

    private void ParseNodes()
    {
        var chainBlocks = _topBlocks.Where(x => x.Block.Type == BlockType.ModifierChain).ToList();

        foreach (var chainBlock in chainBlocks)
        {
            if (!chainBlock.TryReadString(_doc.TextEncoding, out var chainName))
                continue;

            if (!chainBlock.TryReadU32(out var chainType) || chainType != (uint)U3dModifierChainType.Node)
                continue;

            var node = new Node { Name = chainName };

            chainBlock.TryReadU32(out var chainAttributes);
            
            // Skip
            if (((U3dModifierChainAttributes)chainAttributes).HasFlag(U3dModifierChainAttributes.BoundingSpherePresent))
            {
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
            }

            // Skip
            if (((U3dModifierChainAttributes)chainAttributes).HasFlag(U3dModifierChainAttributes.AxisAlignedBoundingBoxPresent))
            {
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
                _ = chainBlock.TryReadF32(out _);
            }

            chainBlock.ReadPadding();
            chainBlock.TryReadU32(out var childBlocksCount);
            List<BlockReader> childBlocks = [];

            while (!chainBlock.IsDataEmpty)
                childBlocks.Add(chainBlock.ReadBlock());

            foreach (var childBlock in childBlocks)
            {
                if (childBlock.Block.Type == BlockType.GroupNode)
                    ParseGroupNodeBlock(childBlock);
                // TODO: Handle other types: ModelNodeBlock, ShadingModifierBlock.
            }

            _doc.Nodes.Add(node.Name, node);
        }
    }

    private void ParseGroupNodeBlock(BlockReader r)
    {
        var node = new Node();

        if (r.TryReadString(_doc.TextEncoding, out var nodeName)) node.Name = nodeName;
        r.TryReadU32(out var parentCount);
        var parentsData = Enumerable.Repeat(0, (int)parentCount).Select(x =>
        {
            r.TryReadString(_doc.TextEncoding, out var name);
            return new { Name = name, Transform = r.ReadArray(16) };
        }).ToList();

        // TODO: Handle all parents.
        node.Parent = parentsData.First().Name;
        node.Transformation = new(parentsData.First().Transform);

        _doc.Nodes.Add(node.Name, node);
    }

    #endregion PrivateMethods
}

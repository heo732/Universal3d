using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Graphics;
using System.IO;
using U3DSpace.IO.BlockIO;
using U3DSpace.Primitives.NodePrimitives;

namespace U3DSpace.IO
{
    public static class DocumentWriter
    {
        #region PublicMethods

        public static void SavePDF(Stream pdfDocStream, U3DDocument u3dDoc)
        {
            var pdfDoc = new PdfDocument();
            PdfPageBase page = pdfDoc.Pages.Add(PdfPageSize.A4, new PdfMargins(30.0f), PdfPageRotateAngle.RotateAngle0, PdfPageOrientation.Landscape);
            var rectangle = new System.Drawing.Rectangle(0, 0, (int)(page.Size.Width - 60.0f), (int)(page.Size.Height - 60.0f));

            string u3dTempFileName = Path.GetTempFileName();
            using (var u3dDocStream = new FileStream(u3dTempFileName, FileMode.Create))
            {
                Save(u3dDocStream, u3dDoc);
            }
            Pdf3DAnnotation annotation = new Pdf3DAnnotation(rectangle, u3dTempFileName);
            File.Delete(u3dTempFileName);

            annotation.Activation = new Pdf3DActivation();
            annotation.Activation.ActivationMode = Pdf3DActivationMode.PageOpen;
            Pdf3DView View = new Pdf3DView();
            View.Background = new Pdf3DBackground(new PdfRGBColor(System.Drawing.Color.White));
            View.ViewNodeName = "DefaultView";
            View.RenderMode = new Pdf3DRendermode(Pdf3DRenderStyle.Solid);
            View.InternalName = "Default";
            View.LightingScheme = new Pdf3DLighting();
            View.LightingScheme.Style = Pdf3DLightingStyle.Day;
            annotation.Views.Add(View);
            page.AnnotationsWidget.Add(annotation);

            pdfDoc.SaveToStream(pdfDocStream, FileFormat.PDF);
        }

        public static void Save(Stream stream, U3DDocument doc)
        {
            using (var writer = new BinaryWriter(stream, doc.TextEncoding, true))
            {
                writer.Write(GetHeaderBlock(doc).ToArray());
                WriteNodes(writer, doc);
                WriteDeclarationsOfMeshes(writer, doc);
                WriteShaders(writer, doc);
                WriteMaterials(writer, doc);
                WriteDeclarationsOfTextures(writer, doc);
                WriteContinuationsOfMeshes(writer, doc);
                WriteContinuationsOfTextures(writer, doc);
            }
        }

        #endregion PublicMethods

        #region PrivateMethods

        private static Block GetHeaderBlock(U3DDocument doc)
        {
            var w = new BlockWriter();
            w.WriteI32(0x00000000); // version
            w.WriteU32(0x00000004); // profile identifier (0x00000004 - No compression mode)
            w.WriteU32(36); // declaration size
            w.WriteU64(732); // file size
            w.WriteU32(106); // character encoding: 106 = UTF-8
            //Meta data.
            w.WriteMetaU32(1); // Key/Value Pair Count
            w.WriteMetaU32(0); // Key/Value Pair Attributes; 0x00000000 - indicates the Value is formatted as a String
            w.WriteMetaString("{Created_by", doc.TextEncoding); // Key String
            w.WriteMetaString("GLTFtoU3D_converter}", doc.TextEncoding); // Value String
            return w.GetBlock(BlockType.Header);
        }

        private static void WriteNodes(BinaryWriter writer, U3DDocument doc)
        {
            foreach (Node node in doc.Nodes.Values)
            {
                if (string.IsNullOrEmpty(node.Mesh))
                {
                    writer.Write(GetGroupNodeModifierChain(node).ToArray());
                }
                else
                {
                    writer.Write(GetNodeModifierChain(node, doc).ToArray());
                }
            }
        }

        private static void WriteDeclarationsOfMeshes(BinaryWriter writer, U3DDocument doc)
        {
            throw new System.NotImplementedException();
        }

        private static void WriteContinuationsOfMeshes(BinaryWriter writer, U3DDocument doc)
        {
            throw new System.NotImplementedException();
        }

        private static void WriteShaders(BinaryWriter writer, U3DDocument doc)
        {
            throw new System.NotImplementedException();
        }

        private static void WriteMaterials(BinaryWriter writer, U3DDocument doc)
        {
            throw new System.NotImplementedException();
        }

        private static void WriteDeclarationsOfTextures(BinaryWriter writer, U3DDocument doc)
        {
            throw new System.NotImplementedException();
        }

        private static void WriteContinuationsOfTextures(BinaryWriter writer, U3DDocument doc)
        {
            throw new System.NotImplementedException();
        }

        private static Block GetGroupNodeBlock(Node node)
        {
            var w = new BlockWriter();
            w.WriteString(node.Name); // model node name
            w.WriteU32(1); // parent node count
            w.WriteString(node.Parent); // parent node name
            w.WriteArray(node.Transformation.ToArray()); // transformation
            return w.GetBlock(BlockType.GroupNode);
        }

        private static Block GetGroupNodeModifierChain(Node node)
        {
            var w = new BlockWriter();
            w.WriteString(node.Name); // modifier chain name
            w.WriteU32(0); // modifier chain type: 0 = node modifier chain
            w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
            // Bounding box info present.
            w.WritePadding();
            w.WriteU32(1); // modifier count in this chain
            w.WriteBlock(GetGroupNodeBlock(node));
            return w.GetBlock(BlockType.ModifierChain);
        }

        private static Block GetMeshDeclarationBlock(string meshName, uint vertexCount, uint normalCount, uint textureCoordCount, uint faceCount)
        {
            var w = new BlockWriter();
            w.WriteString(meshName); // mesh name, equal to ModelResourceModifierChain.ModifierChainName
            w.WriteU32(0); // chain index
            // Max mesh description.
            w.WriteU32(normalCount == 0 ? 1u : 0u); // mesh attributes: 1 = no normals
            w.WriteU32(faceCount); // face count
            w.WriteU32(vertexCount); // positions count
            w.WriteU32(normalCount); // normal count
            w.WriteU32(0); // diffuse color count
            w.WriteU32(0); // specular color count
            w.WriteU32(textureCoordCount); // texture coord count
            w.WriteU32(1); // shading count
            // Shading description.
            w.WriteU32(0); // shading attributes
            w.WriteU32(textureCoordCount == 0 ? 0u : 1u); // texture layer count
            if (textureCoordCount > 0)
            {
                w.WriteU32(2); // texture coord dimensions
            }
            w.WriteU32(0); // original shading id
            // Clod desc.
            w.WriteU32(vertexCount); // minimum resolution
            w.WriteU32(vertexCount); // maximum resolution
            // Resource Description.
            w.WriteU32(300); // position quality factor
            w.WriteU32(300); // normal quality factor
            w.WriteU32(300); // texture coord quality factor
            w.WriteF32(0.01f); // position inverse quant
            w.WriteF32(0.01f); // normal inverse quant
            w.WriteF32(0.01f); // texture coord inverse quant
            w.WriteF32(0.01f); // diffuse color inverse quant
            w.WriteF32(0.01f); // specular color inverse quant
            w.WriteF32(0.9f); // normal crease parameter
            w.WriteF32(0.5f); // normal update parameter
            w.WriteF32(0.985f); // normal tolerance parameter
            // Skeleton Description.
            w.WriteU32(0); // bone count
            return w.GetBlock(0xFFFFFF31);
        }

        private static Block GetModelResourceModifierChain(Node node, U3DDocument doc)
        {
            var w = new BlockWriter();
            w.WriteString(modifierChainName); // modifier chain name, bonded to ModelNodeBlock.ModelResourceName
            w.WriteU32(1); // modifier chain type: 1 = model resource modifier chain
            w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
            // Bounding box info present. Padding.
            w.WritePadding();
            w.WriteU32(1); // modifier count in this chain
            w.WriteBlock(GetMeshDeclarationBlock(meshName, vertexCount, normalCount, textureCoordCount, faceCount));
            return w.GetBlock(0xFFFFFF14);
        }

        private static Block GetMeshContinuationBlock(string meshName,
            List<Vector3> vertices, List<Vector3> normals, List<Vector2> textureCoordinates,
            List<Triangle> vertexFaces, List<Triangle> normalFaces, List<Triangle> textureFaces)
        {
            var w = new BlockWriter();
            w.WriteString(meshName); // mesh name
            w.WriteU32(0); // chain index
            // Base Mesh Description.
            w.WriteU32((uint)vertexFaces.Count); // base face count
            w.WriteU32((uint)vertices.Count); // base position count
            w.WriteU32((uint)normals.Count); // base normal count
            w.WriteU32(0); // base diffuse color count
            w.WriteU32(0); // base specular color count
            w.WriteU32((uint)textureCoordinates.Count); // base texture coordinate count
            // Base Mesh Data.
            foreach (var vertex in vertices)
            {
                w.WriteF32(vertex.X);
                w.WriteF32(vertex.Y);
                w.WriteF32(vertex.Z);
            }
            foreach (var normal in normals)
            {
                w.WriteF32(normal.X);
                w.WriteF32(normal.Y);
                w.WriteF32(normal.Z);
            }
            foreach (var textureCoord in textureCoordinates)
            {
                w.WriteF32(textureCoord.X);
                w.WriteF32(-textureCoord.Y);
                w.WriteF32(0);
                w.WriteF32(0);
            }
            // Base Face.
            for (int i = 0; i < vertexFaces.Count; i++)
            {
                w.WriteU32(0u); // shading id

                w.WriteU32(vertexFaces[i].A); // position index, face corner A

                if (normalFaces.Count > 0)
                    w.WriteU32(normalFaces[i].A); // normal index, face corner A

                if (textureFaces.Count > 0)
                    w.WriteU32(textureFaces[i].A); // texture coord index, face corner A

                w.WriteU32(vertexFaces[i].B); // position index, face corner B

                if (normalFaces.Count > 0)
                    w.WriteU32(normalFaces[i].B); // normal index, face corner B

                if (textureFaces.Count > 0)
                    w.WriteU32(textureFaces[i].B); // texture coord index, face corner B

                w.WriteU32(vertexFaces[i].C); // position index, face corner C

                if (normalFaces.Count > 0)
                    w.WriteU32(normalFaces[i].C); // normal index, face corner C

                if (textureFaces.Count > 0)
                    w.WriteU32(textureFaces[i].C); // texture coord index, face corner C
            }
            return w.GetBlock(0xFFFFFF3B);
        }

        private static Block GetModelNodeBlock(string nodeName, string parentNodeName)
        {
            var w = new BlockWriter();
            w.WriteString(nodeName); // model node name
            w.WriteU32(1); // parent node count
            w.WriteString(parentNodeName ?? ""); // parent node name
            var identityMatrix = new float[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            w.WriteArray(identityMatrix); // transformation
            w.WriteString(nodeName); // model resource name
            w.WriteU32(3); // visibility 3 = front and back
            return w.GetBlock(0xFFFFFF22);
        }

        private static Block GetShadingModifierBlock(string shadingModName, string shaderName)
        {
            var w = new BlockWriter();
            w.WriteString(shadingModName); // shading modifier name
            w.WriteU32(1); // chain index
            w.WriteU32(1); // shading attributes
            w.WriteU32(1); // shading list count
            w.WriteU32(1); // shader count
            w.WriteString(shaderName); // shader name
            return w.GetBlock(0xFFFFFF45);
        }

        private static Block GetNodeModifierChain(string nodeName, string parentNodeName, string shadingName, string shaderName)
        {
            var w = new BlockWriter();
            w.WriteString(nodeName); // modifier chain name
            w.WriteU32(0); // modifier chain type: 0 = node modifier chain
            w.WriteU32(0); // modifier chain attributes: 0 = neither bounding sphere nor
            // Bounding box info present.
            w.WritePadding();
            w.WriteU32(2); // modifier count in this chain
            w.WriteBlock(GetModelNodeBlock(nodeName, parentNodeName));
            w.WriteBlock(GetShadingModifierBlock(shadingName, shaderName));
            return w.GetBlock(0xFFFFFF14);
        }

        #endregion PrivateMethods
    }
}
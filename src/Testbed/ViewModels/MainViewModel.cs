using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.IO;
using Universal3d.Samples;
using Universal3d.Pdf;
using Spire.Pdf;

namespace Testbed.ViewModels;
public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    private static void SaveSample()
    {
        var dialog = new SaveFileDialog()
        {
            Filter = "U3D (*.u3d)|*.u3d|PDF (*.pdf)|*.pdf",
            FileName = "TexturedRubiksCube.u3d",
            AddExtension = true,
        };

        if (!(dialog.ShowDialog() ?? false))
            return;

        var doc = U3DSamples.TexturedRubiksCube;

        if (Path.GetExtension(dialog.FileName).Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
        {
            using var pdfDoc = doc.ToPdf();
            pdfDoc.SaveToFile(dialog.FileName, FileFormat.PDF);
            return;
        }

        doc.Save(dialog.FileName);
    }
}

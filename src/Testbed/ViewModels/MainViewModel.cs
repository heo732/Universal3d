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
    private void SaveSample()
    {
        var dialog = new SaveFileDialog()
        {
            Filter = "U3D (*.u3d)|*.u3d|PDF (*.pdf)|*.pdf",
            FileName = "TexturedRubiksCube.u3d",
            AddExtension = true,
        };

        if (!(dialog.ShowDialog() ?? false))
            return;

        var doc = U3dSamples.TexturedRubiksCube;

        if (Path.GetExtension(dialog.FileName).Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
        {
            using var pdfDoc = doc.ToPdf();
            pdfDoc.SaveToFile(dialog.FileName, FileFormat.PDF);
            return;
        }

        doc.Save(dialog.FileName);
    }

    [RelayCommand]
    private void WrapU3dToPdf()
    {
        var openDialog = new OpenFileDialog()
        {
            Filter = "U3D (*.u3d)|*.u3d",
        };

        if (!(openDialog.ShowDialog() ?? false))
            return;

        var u3dPath = openDialog.FileName;

        var saveDialog = new SaveFileDialog()
        {
            Filter = "PDF (*.pdf)|*.pdf",
            FileName = $"{Path.GetFileNameWithoutExtension(u3dPath)}.pdf",
            AddExtension = true,
        };

        if (!(saveDialog.ShowDialog() ?? false))
            return;

        var pdfPath = saveDialog.FileName;
        u3dPath.U3dToPdf().SaveToFile(pdfPath);
    }

    [RelayCommand]
    private void ExtractU3dFromPdf()
    {
        var openDialog = new OpenFileDialog()
        {
            Filter = "PDF (*.pdf)|*.pdf",
        };

        if (!(openDialog.ShowDialog() ?? false))
            return;

        var pdfPath = openDialog.FileName;
        var pdfDoc = new PdfDocument();
        pdfDoc.LoadFromFile(pdfPath);
        var u3dStreams = PdfParser.ExtractU3dStreams(pdfDoc);
        var pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        var index = 0;

        foreach (var u3dStream in u3dStreams)
        {
            var saveDialog = new SaveFileDialog()
            {
                Filter = "U3D (*.u3d)|*.u3d",
                FileName = $"{pdfName}_{++index}.u3d",
                AddExtension = true,
            };

            if (!(saveDialog.ShowDialog() ?? false))
                return;

            var u3dPath = saveDialog.FileName;
            File.WriteAllBytes(u3dPath, u3dStream.ToArray());
        }
    }
}

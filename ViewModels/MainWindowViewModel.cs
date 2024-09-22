using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace beskriv.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private Bitmap? _imageFile;

    [RelayCommand]
    public async Task OpenImage()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Missing StorageProvider instance.");

        var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Image File",
            AllowMultiple = false,
            FileTypeFilter = [SupportedImages, FilePickerFileTypes.All]
        });

        if (files.Count != 1) return;

        try
        {
            var newPath = files[0].TryGetLocalPath();
            
            if (newPath != null)
                ImageFile = new Bitmap(newPath);
        }
        catch (ArgumentException) { }
    }

    public static FilePickerFileType SupportedImages { get; } = new("Supported image files")
    {
        Patterns = ["*.png", "*.jpg", "*.jpeg", "*.webp"],
        AppleUniformTypeIdentifiers = ["public.png","public.jpeg","org.webmproject.webp"],
        MimeTypes = ["image/png","image/jpeg","image/webp"]
    };
}

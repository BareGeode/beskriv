using System.Text;
using ImageMagick;

namespace beskriv.Models;

public class ExifIO
{
    private static byte[] characterIdentifierCode = [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
    private string filePath;
    private MagickImage image;
    private IExifProfile profile;

    public ExifIO(string filePath)
    {
        this.filePath = filePath;

        try
        {
            image = new MagickImage(filePath);
        }
        catch (MagickBlobErrorException)
        {
            throw;
        }

        var temporaryProfile = image.GetExifProfile();
        if (temporaryProfile is null)
            profile = new ExifProfile();
        else
            profile = temporaryProfile;
    }

    public string ReadUserComment()
    {
        if (profile.GetValue(ExifTag.UserComment) is null) return "";

        byte[] userCommentBytes = profile.GetValue(ExifTag.UserComment)!.Value[8..];
        string userCommentString = Encoding.UTF8.GetString(userCommentBytes);

        return userCommentString;
    }

    public void SaveExif(string description)
    {
        profile.SetValue(ExifTag.UserComment, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, .. Encoding.UTF8.GetBytes(description)]);
        image.SetProfile(profile);
        image.Write(filePath);
    }
}
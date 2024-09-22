using System;
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
        if(temporaryProfile is null)
            profile = new ExifProfile();
        else
            profile = temporaryProfile;
    }

    public string readUserComment()
    {
        var image = new MagickImage(filePath);
        var profile = image.GetExifProfile();

        if (profile is null) return "";
        if (profile.GetValue(ExifTag.UserComment) is null) return "";

        byte[] userCommentBytes = profile.GetValue(ExifTag.UserComment)!.Value[8..];
        string userCommentString = Encoding.UTF8.GetString(userCommentBytes);

        return userCommentString;
    }
}
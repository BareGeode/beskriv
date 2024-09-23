# beskriv
Beskriv: a GUI application for viewing images and giving them descriptions.

https://github.com/user-attachments/assets/80c3e400-3082-4442-ade3-92d8d64b3176

---

### What is this?
The purpose of **beskriv** is to provide a tool that will let you quickly and easily add metadata descriptions to images (and maybe other media in the future?), whether that be Exif, XMP or other metadata formats. Currently, you can open PNG, JPG/JPEG and WEBP images and modify or add Exif's UserComment tag, which is embedded into the image file itself.

Why not use other existing tools for this, such as ExifTool? Well, beskriv aims to provide a simpler and quicker interface which will allow you to go through and add descriptions to lots of images quickly. For example, if you have digitized a lot of old photos and wish to document them, then you will be able to go through each photo with just a keybind and start typing a description immediately. beskriv does not aim to provide a complete tool for editing any metadata, and instead specializes in certain kinds of metadata.

### How to build?
This repository page does not currently distribute any binaries for beskriv, and you must build it yourself. Currently, it should be enough to just clone this repository and then run `dotnet build` along with `dotnet run` in the folder where you have cloned it to run the program. I have tested it on Windows and Ubuntu Linux, but it should also work for macOS systems.

### TODO
* Write tests
* Add keybinds for opening new images and saving the description
* Create a menu for settings, layout etc.
* Add support for other formats

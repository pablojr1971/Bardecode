Imports System.Drawing.Imaging
Imports System.Windows.Media.Imaging
Imports System.IO



Public Class Test

    Public Shared Sub Convert(Image As Bitmap, Path As String)

        Dim image2 As Bitmap = MakeGrayscale(Image)

        Dim fcb As System.Windows.Media.Imaging.FormatConvertedBitmap = New FormatConvertedBitmap(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image2.GetHbitmap(System.Drawing.Color.Transparent), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(image2.Width, image2.Height)), System.Windows.Media.PixelFormats.Gray8, BitmapPalettes.Gray256, 0.5)
        Dim pngBitmapEncoder As PngBitmapEncoder = New PngBitmapEncoder()
        pngBitmapEncoder.Interlace = PngInterlaceOption.Off
        pngBitmapEncoder.Frames.Add(BitmapFrame.Create(fcb))
        Dim fileStream As Stream = File.Open(Path, FileMode.Create)
        pngBitmapEncoder.Save(fileStream)
        fileStream.Close()
    End Sub



    Public Shared Function MakeGrayscale(ByVal original As Bitmap) As Bitmap


        ''create a blank bitmap the same size as original

        Dim newBitmap As Bitmap = New Bitmap(original.Width, original.Height)



        ''get a graphics object from the new image

        Dim g As Graphics = Graphics.FromImage(newBitmap)



        'create the grayscale ColorMatrix

        Dim colorMatrix As ColorMatrix = New ColorMatrix(New Single()() { _
          New Single() {0.3F, 0.3F, 0.3F, 0, 0}, _
          New Single() {0.59F, 0.59F, 0.59F, 0, 0}, _
          New Single() {0.11F, 0.11F, 0.11F, 0, 0}, _
          New Single() {0, 0, 0, 1, 0}, _
          New Single() {0, 0, 0, 0, 1}})



        'create some image attributes

        Dim attributes As ImageAttributes = New ImageAttributes()



        'set the color matrix attribute

        attributes.SetColorMatrix(colorMatrix)



        'draw the original image on the new image using the grayscale color matrix

        g.DrawImage(original, New Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes)



        'dispose the Graphics object

        g.Dispose()

        Return newBitmap



    End Function
End Class
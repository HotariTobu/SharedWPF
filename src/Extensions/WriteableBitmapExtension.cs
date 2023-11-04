using System.Windows.Media.Imaging;

namespace SharedWPF
{
    public static class WriteableBitmapExtension
    {
        public static int GetIndexAt(this WriteableBitmap bitmap, int x, int y)
        {
            return y * bitmap.BackBufferStride + x;
        }
    }
}

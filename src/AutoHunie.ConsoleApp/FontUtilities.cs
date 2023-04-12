using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace AutoHunie.ConsoleApp
{
    public class FontUtilities
    {
        public PrivateFontCollection FontCollection = new();
        
        public FontFamily AddToFontCollection(string path)
            => AddToFontCollection(File.ReadAllBytes(path));
        
        public FontFamily AddToFontCollection(byte[] fontBytes)
        {
            var handle = GCHandle.Alloc(fontBytes, GCHandleType.Pinned);
            var pointer = handle.AddrOfPinnedObject();
            
            try
            {
                FontCollection.AddMemoryFont(pointer, fontBytes.Length);
            }
            finally
            {
                handle.Free();
            }

            return FontCollection.Families.Last();
        }
    }
}

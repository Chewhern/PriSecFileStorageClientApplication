using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriSecFileStorageWeb.Helper
{
    public static class FileBytesChunkClass
    {
        public static int MaximumChunksFileBytesLength = 5242880;

        public static int MaximumChunksFileBytesWithMACLength = MaximumChunksFileBytesLength + 16;

        public static int NonceMaximumChunksFileBytesWithMACLength = MaximumChunksFileBytesWithMACLength + 24;       
    }
}

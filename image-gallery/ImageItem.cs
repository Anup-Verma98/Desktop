using System;
using System.Collections.Generic;
using System.Text;

namespace image_gallery
{
        public class ImageItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public byte[] Base64 { get; set; }
            public string Format { get; set; }
        }
}

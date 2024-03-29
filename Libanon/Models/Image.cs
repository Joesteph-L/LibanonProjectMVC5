﻿

namespace Libanon.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageBinary { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string Type { get; set; }


        public int? BookISBN { get; set; }
        public virtual Book Book { get; set; }
    }
}
using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{

    public partial class DocUploadWrapper
    {
        public ImageUploadRequest ImageUploadRequest { get; set; }
    }

    public partial class ImageUploadRequest
    {
        public Image Image { get; set; }
    }

    public partial class Image
    {
        public string Context { get; set; }
        public string Content { get; set; }
    }
}

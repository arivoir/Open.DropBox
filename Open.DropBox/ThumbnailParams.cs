using System.Runtime.Serialization;

namespace Open.DropBox
{

    [DataContract]
    public class ThumbnailParams
    {
        [DataMember(Name = "path", IsRequired = false)]
        public string Path { get; set; }
        [DataMember(Name = "format", IsRequired = false)]
        public string Format { get; set; }
        [DataMember(Name = "size", IsRequired = false)]
        public string Size { get; set; }
    }
}
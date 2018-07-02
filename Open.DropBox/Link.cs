using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class DropBoxLink
    {
        [DataMember(Name = "url", IsRequired = false)]
        public string Url { get; set; }
        [DataMember(Name = "expires", IsRequired = false)]
        public string Expires { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class CopyParams
    {
        [DataMember(Name = "from_path", IsRequired = false)]
        public string FromPath { get; set; }
        [DataMember(Name = "to_path", IsRequired = false)]
        public string ToPath { get; set; }
    }
}

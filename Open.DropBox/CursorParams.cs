using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class CursorParams
    {
        [DataMember(Name = "cursor", IsRequired = false)]
        public string Cursor { get; set; }
    }
}
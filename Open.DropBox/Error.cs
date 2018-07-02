using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "error_summary", IsRequired = false)]
        public string ErrorSummary { get; set; }
        [DataMember(Name = "error", IsRequired = false)]
        public ErrorInfo ErrorInfo { get; set; }
    }

    [DataContract]
    public class ErrorInfo
    {
        [DataMember(Name = ".tag", IsRequired = false)]
        public string Tag { get; set; }
    }
}
using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class UploadParams
    {
        [DataMember(Name = "path", IsRequired = false)]
        public string Path { get; set; }
        [DataMember(Name = "mode", IsRequired = false)]
        public string Mode { get; set; }
        [DataMember(Name = "autorename", IsRequired = false)]
        public bool Autorename { get; set; }
        [DataMember(Name = "mute", IsRequired = false)]
        public bool Mute { get; set; }
    }

    [DataContract]
    public class UploadSessionParams
    {
        [DataMember(Name = "close", EmitDefaultValue = false)]
        public bool? Close { get; set; }

        [DataMember(Name = "cursor", EmitDefaultValue = false)]
        public UploadSessionCursor Cursor { get; set; }

        [DataMember(Name = "commit", EmitDefaultValue = false)]
        public UploadParams Commit { get; set; }
    }

    [DataContract]
    public class UploadSessionCursor
    {
        [DataMember(Name = "session_id", EmitDefaultValue = false)]
        public string SessionId { get; set; }
        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public long Offset { get; set; }
    }

    [DataContract]
    public class UploadSessionResult
    {
        [DataMember(Name = "session_id", EmitDefaultValue = false)]
        public string SessionId { get; set; }
    }

}
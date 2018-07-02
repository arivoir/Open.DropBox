using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class MetadataParams
    {
        [DataMember(Name = "path", IsRequired = false)]
        public string Path { get; set; }
        [DataMember(Name = "include_media_info", IsRequired = false)]
        public bool IncludeMediaInfo { get; set; }
        [DataMember(Name = "include_deleted", IsRequired = false)]
        public bool IncludeDeleted { get; set; }
        [DataMember(Name = "include_has_explicit_shared_members", IsRequired = false)]
        public bool IncludeHasExplicitSharedMembers { get; set; }
    }
}
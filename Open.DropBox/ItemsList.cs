using System.Runtime.Serialization;

namespace Open.DropBox
{

    [DataContract]
    public class ListFolderParams
    {
        [DataMember(Name = "path", IsRequired = false)]
        public string Path { get; set; }
        [DataMember(Name = "recursive", IsRequired = false)]
        public bool Recursive { get; set; }
        [DataMember(Name = "include_media_info", IsRequired = false)]
        public bool IncludeMediaInfo { get; set; }
        [DataMember(Name = "include_deleted", IsRequired = false)]
        public bool IncludeDeleted { get; set; }
        [DataMember(Name = "include_has_explicit_shared_members", IsRequired = false)]
        public bool IncludeHasExplicitSharedMembers { get; set; }
    }

    [DataContract]
    public class ItemsList
    {
        [DataMember(Name = "entries", IsRequired = false)]
        public Item[] Entries { get; set; }
        [DataMember(Name = "cursor", IsRequired = false)]
        public string Cursor { get; set; }
        [DataMember(Name = "has_more", IsRequired = false)]
        public bool HasMore { get; set; }
    }
}
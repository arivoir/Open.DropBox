using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class ItemParams
    {
        [DataMember(Name = "path", IsRequired = false)]
        public string Path { get; set; }
    }

    [DataContract]
    public class Item
    {
        [DataMember(Name = ".tag", IsRequired = false)]
        public string Tag { get; set; }
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }
        [DataMember(Name = "id", IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Name = "client_modified", IsRequired = false)]
        public string ClientModified { get; set; }
        [DataMember(Name = "server_modified", IsRequired = false)]
        public string ServerModified { get; set; }
        [DataMember(Name = "rev", IsRequired = false)]
        public string Rev { get; set; }
        [DataMember(Name = "size", IsRequired = false)]
        public long Size { get; set; }
        [DataMember(Name = "path_lower", IsRequired = false)]
        public string PathLower { get; set; }
        [DataMember(Name = "path_display", IsRequired = false)]
        public string PathDisplay { get; set; }
        [DataMember(Name = "sharing_info", IsRequired = false)]
        public SharingInfo SharingInfo { get; set; }
        [DataMember(Name = "has_explicit_shared_members", IsRequired = false)]
        public bool HasExplicitSharedMembers { get; set; }
        
        [DataMember(Name = "property_groups", IsRequired = false)]
        public PropertyGroup[] PropertyGroups { get; set; }
    }

    [DataContract]
    public class PropertyGroup
    {
        [DataMember(Name = "template_id", IsRequired = false)]
        public string TemplateId { get; set; }
        [DataMember(Name = "fields", IsRequired = false)]
        public Field[] Fields { get; set; }
        
    }

    [DataContract]
    public class Field
    {
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }
        [DataMember(Name = "value", IsRequired = false)]
        public string Value { get; set; }
    }

    [DataContract]
    public class SharingInfo
    {
        [DataMember(Name = "read_only", IsRequired = false)]
        public bool ReadOnly { get; set; }
        [DataMember(Name = "parent_shared_folder_id", IsRequired = false)]
        public string ParentSharedFolderId { get; set; }
        [DataMember(Name = "modified_by", IsRequired = false)]
        public string ModifiedBy { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class Account
    {
        [DataMember(Name = "account_id")]
        public string AccountId { get; set; }
        [DataMember(Name = "name")]
        public Name Name { get; set; }
    }

    public class Name
    {
        [DataMember(Name = "given_name")]
        public string GivenName { get; set; }
        [DataMember(Name = "surname")]
        public string Surname { get; set; }
        [DataMember(Name = "familiar_name")]
        public string FamiliarName { get; set; }
        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }
    }

    [DataContract]
    public class DropBoxQuota
    {
        [DataMember(Name = "quota")]
        public long Quota { get; set; }
        [DataMember(Name = "normal")]
        public long Normal { get; set; }
        [DataMember(Name = "shared")]
        public long Shared { get; set; }
    }
}

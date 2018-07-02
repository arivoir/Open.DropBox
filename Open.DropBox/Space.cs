using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class Space
    {
        [DataMember(Name = "used")]
        public ulong Used { get; set; }
        [DataMember(Name = "allocation")]
        public SpaceAllocation Allocation { get; set; }
    }

    [DataContract]
    public class SpaceAllocation
    {
        [DataMember(Name = ".tag")]
        public string Tag { get; set; }
        [DataMember(Name = "allocated")]
        public ulong Allocated { get; set; }
    }
}
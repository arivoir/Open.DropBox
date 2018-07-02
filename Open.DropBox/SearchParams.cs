using System.Runtime.Serialization;

namespace Open.DropBox
{
    [DataContract]
    public class SearchParams
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "query")]
        public string Query { get; set; }
        [DataMember(Name = "start")]
        public int Start { get; set; }
        [DataMember(Name = "max_results")]
        public int MaxResults { get; set; }
        [DataMember(Name = "mode")]
        public string Mode { get; set; }
    }


    [DataContract]
    public class SearchList
    {
        [DataMember(Name = "matches")]
        public SerachMatch[] Matches { get; set; }
        [DataMember(Name = "more")]
        public bool More { get; set; }
        [DataMember(Name = "start")]
        public int Start { get; set; }
    }

    [DataContract]
    public class SerachMatch
    {
        [DataMember(Name = "match_type")]
        public SearchMatchType MatchType { get; set; }
        [DataMember(Name = "metadata")]
        public Item Metadata { get; set; }
    }

    [DataContract]
    public class SearchMatchType
    {
        [DataMember(Name = ".tag")]
        public string Tag { get; set; }
    }
}
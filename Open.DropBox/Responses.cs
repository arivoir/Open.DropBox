using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Open.DropBox
{
    public class Responses
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    [DataContract]
    public class DropBoxToken
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "secret")]
        public string Secret { get; set; }
    }
    public class AccountInfoResponse
    {
        public string country;
        public string display_name;

        public QuotaInfoResponse quota_info;
        public string uid;
        public string referral_link;
        public string email;

    }
    public class QuotaInfoResponse
    {
        public long shared;
        public long quota;
        public long normal;
    }
    public class FileMetadata : Responses
    {
        public int revision = 0;
        public bool thumb_exists = false;
        public int bytes = 0;
        public DateTime modified = DateTime.Now;
        public string path = string.Empty;
        public string Path
        {
            get
            {
                string trimmed = path.Trim('/');

                int index = trimmed.LastIndexOf('/');
                if (index < 0)
                {
                    return trimmed; // No directory, just a file name
                }
                return trimmed.Substring(index + 1);
            }
            set
            {
                if (value != path)
                {
                    path = value;
                    NotifyPropertyChanged("Path");
                }
            }
        }
        public bool is_dir = false;
        public string icon = string.Empty;
        public string mime_type = string.Empty;
        public string MimeType
        {
            get
            {
                if (is_dir)
                {
                    return "folder";
                }
                return mime_type;
            }
            set
            {
                if (value != mime_type)
                {
                    mime_type = value;
                    NotifyPropertyChanged("MimeType");
                }
            }
        }
        public string size = string.Empty;
        public string thumbnail = string.Empty;
        public string Thumbnail
        {
            get
            {
                return thumbnail;
            }
            set
            {
                if (thumbnail != value)
                {
                    thumbnail = value;
                    NotifyPropertyChanged("Thumbnail");
                }
            }
        }
    }
    public class FolderMetadata : Responses, INotifyPropertyChanged
    {
        public string hash = string.Empty;
        public DateTime modified = DateTime.Now;
        public bool thumb_exists = false;
        public int bytes = 0;
        public string path = string.Empty;
        public bool is_dir = false;
        public string size = string.Empty;
        public string root = string.Empty;
        public List<FileMetadata> contents = new List<FileMetadata>();
        public List<FileMetadata> Contents
        {
            get
            {
                return contents;
            }
            set
            {
                if (contents != value)
                {
                    contents = value;
                    NotifyPropertyChanged("Contents");
                }
            }
        }
        public string icon;


    }
}

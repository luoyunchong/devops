using System;
using FreeSql.DataAnnotations;

namespace DevOps.Core.Domain
{
    public class Post
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int PostId { get; set; }

        [Column(DbType = "varchar(50)")]
        public string ReplyContent { get; set; }
        public int BlogId { get; set; }
        public DateTime ReplyTime { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
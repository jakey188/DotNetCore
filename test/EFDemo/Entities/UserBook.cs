using DotNetCore.Data;
using System.ComponentModel.DataAnnotations;

namespace EFDemo.Entities
{
    // 复合主键使用场景

    public class UserBook : EntityBase<int>
    {
        public string UserId { get; set; }
        public int BookId { get; set; }

        public string Name { get; set; }
    }
}


using DotNetCore.Data;

namespace EFDemo.Data
{
    public class Book : EntityBase<int>
    {
        public string Name { get; set; }
    }

}

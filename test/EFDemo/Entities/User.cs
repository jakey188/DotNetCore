using DotNetCore.Data;

namespace EFDemo.Data
{
    public class User : EntityBase<string>
    {
        public string Name { get; set; }
    }
}

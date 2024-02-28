namespace DotNetCore.Data.EntityFrameworkCore.ModelBuilders
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}

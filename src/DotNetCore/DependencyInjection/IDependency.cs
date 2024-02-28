
using DotNetCore.DependencyInjection.Attributes;

namespace DotNetCore.DependencyInjection
{
    /// <summary>
    /// 实现此接口的类型将被注册为<see cref="ServiceLifetime.Scoped"/>模式
    /// </summary>
    [DependencyIgnore]
    public interface IScopedDependency 
    { }

    /// <summary>
    /// 实现此接口的类型将被注册为<see cref="ServiceLifetime.Scoped"/>模式
    /// </summary>
    [DependencyIgnore]
    public interface ISingletonDependency 
    { }

    /// <summary>
    /// 实现此接口的类型将被注册为<see cref="ServiceLifetime.Scoped"/>模式
    /// </summary>
    [DependencyIgnore]
    public interface ITransientDependency 
    {  }
}

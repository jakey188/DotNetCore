
namespace DotNetCore.DynamicFilters
{
    public enum FilterConcat
    {
        And,
        Or
    }

    public enum FilterOperator
    {
        /// <summary>
        /// 等于
        /// </summary>
        Eq,

        /// <summary>
        /// 不等于
        /// </summary>
        Ne,

        /// <summary>
        /// 小于
        /// </summary>
        Lt,

        /// <summary>
        /// 大于
        /// </summary>
        Gt,

        /// <summary>
        /// 小于等于
        /// </summary>
        Le,

        /// <summary>
        /// 大于等于
        /// </summary>
        Ge,

        /// <summary>
        /// 开头等于
        /// </summary>
        StartsWith,

        /// <summary>
        /// 结尾等于
        /// </summary>
        EndsWith,

        /// <summary>
        /// 包含
        /// </summary>
        Contains,

        /// <summary>
        /// 存在于
        /// </summary>
        In,
    }


    public enum FieldValueType
    {
        Number,
        String,
        Boolean,
        DateTime
    }
}

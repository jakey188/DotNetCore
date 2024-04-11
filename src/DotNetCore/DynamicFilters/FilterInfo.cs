using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.DynamicFilters
{
    public class FilterInfo
    {
        public FilterInfo() { }
        public FilterInfo(string? table, FilterOperator? @operator, string? fieldName, object? fieldValue, FilterConcat? concat = null, List<FilterInfo>? filters = null, FieldValueType? fieldType = null)
        {
            Concat = concat;
            Filters = filters;
            Table = table;
            FieldName = fieldName;
            Operator = @operator;
            FieldValue = fieldValue;
            FieldType = fieldType;
        }

        /// <summary>
        /// 条件连接符
        /// Field/Operator/Value 与 Concat/Filters 不要同时设置
        /// </summary>
        public FilterConcat? Concat { get; set; }

        /// <summary>
        /// 子过滤条件
        /// Field/Operator/Value 与 Concat/Filters 不要同时设置
        /// </summary>
        public List<FilterInfo>? Filters { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string? Table { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string? FieldName { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public FilterOperator? Operator { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object? FieldValue { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public FieldValueType? FieldType { get; set; }
    }
}

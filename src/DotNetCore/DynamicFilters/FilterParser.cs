using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.DynamicFilters
{
    public class FilterParser
    {
        /// <summary>
        /// 创建过滤条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="isParameter"></param>
        /// <param name="parameterIndex"></param>
        /// <returns></returns>
        public FilterParserResult CreateFilter(FilterInfo filter, bool isParameter = true, int parameterIndex = 0)
        {
            var sb = new StringBuilder();

            var parameters = new Dictionary<string, object>();

            if (IsIgnoreFilter(filter)) filter.FieldName = string.Empty;

            ParseFilter(FilterConcat.And, filter, true);

            void ParseFilter(FilterConcat concat, FilterInfo fi, bool isend)
            {
                if (!string.IsNullOrWhiteSpace(fi.FieldName))
                {
                    parameterIndex++;

                    CreateFilterField(fi, parameters, sb, parameterIndex, isParameter);
                }

                if (fi.Filters?.Any() == true)
                {
                    fi.Filters = fi.Filters.Where(c => IsIgnoreFilter(c) == false).ToList();

                    if (fi.Filters.Any())
                    {
                        if (string.IsNullOrEmpty(fi.FieldName) == false)
                            sb.Append(" AND ");
                        if (fi.Concat == FilterConcat.Or) sb.Append("(");
                        for (var i = 0; i < fi.Filters.Count; i++)
                            ParseFilter(fi.Concat!.Value, fi.Filters[i], i == fi.Filters.Count - 1);
                        if (fi.Concat == FilterConcat.Or) sb.Append(")");
                    }
                }

                if (isend == false)
                {
                    if (string.IsNullOrEmpty(fi.FieldName) == false || fi.Filters?.Any() == true)
                    {
                        switch (concat)
                        {
                            case FilterConcat.And: sb.Append(" AND "); break;
                            case FilterConcat.Or: sb.Append(" OR "); break;
                        }
                    }
                }
            }

            return new FilterParserResult { Filters = sb, Parameters = parameters, CurrentParameterIndex = parameterIndex };
        }

        /// <summary>
        /// 创建某个字段过滤条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="parameters"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="parameterIndex"></param>
        /// <param name="isParameter"></param>
        private void CreateFilterField(FilterInfo filter, Dictionary<string, object> parameters, StringBuilder filterBuilder, int parameterIndex, bool isParameter)
        {
            var value = FilterHelper.SqlReplace(filter.FieldValue);

            var isArray = false;
            var isString = true;
            switch (filter.Operator)
            {
                case FilterOperator.Contains:
                    value = "%" + value + "%";
                    break;
                case FilterOperator.EndsWith:
                    value = "%" + value;
                    break;
                case FilterOperator.StartsWith:
                    value = value + "%";
                    break;
                case FilterOperator.In:
                    value = FilterHelper.GetFilterListValue(filter);
                    isArray = true;
                    break;
                default:
                    value = FilterHelper.GetRealValue(filter.Operator!.Value, value?.ToString(), filter.FieldType, out isString);
                    break;
            }

            if (isParameter)
            {
                var parasKey = $"p{parameterIndex}_{filter.Table}_{filter.FieldName}";

                if (!isArray)
                    filterBuilder.Append($"`{filter.Table}`.`{filter.FieldName}` {FilterHelper.Operators[filter.Operator!.Value]} @{parasKey}");
                else
                    filterBuilder.Append($"`{filter.Table}`.`{filter.FieldName}`  In @{parasKey} ");

                if (!parameters.ContainsKey(parasKey))
                    parameters.Add(parasKey, value ?? string.Empty);
            }
            else
            {
                if (!isArray)
                {
                    if (isString)
                        filterBuilder.Append($"`{filter.Table}`.`{filter.FieldName}` {FilterHelper.Operators[filter.Operator!.Value]} '{value}'");
                    else
                        filterBuilder.Append($"`{filter.Table}`.`{filter.FieldName}` {FilterHelper.Operators[filter.Operator!.Value]} {value}");
                }
                else
                    filterBuilder.Append($"`{filter.Table}`.`{filter.FieldName}`  IN ({value}) ");
            }

        }

        /// <summary>
        /// 忽略过滤条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private bool IsIgnoreFilter(FilterInfo filter)
        {
            var list = new List<FilterOperator>()
            {
                FilterOperator.Contains, FilterOperator.StartsWith, FilterOperator.EndsWith
            };
            return !string.IsNullOrWhiteSpace(filter.FieldName) && filter.Operator != null &&
                list.Contains(filter.Operator.Value) && string.IsNullOrEmpty(filter.FieldValue?.ToString());
        }
    }
}

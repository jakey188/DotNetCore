using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.DynamicFilters
{
    public class FilterHelper
    {
        public static Dictionary<FilterOperator, string> Operators = new Dictionary<FilterOperator, string>()
        {
            { FilterOperator.Contains,"Like" },
            { FilterOperator.StartsWith,"Like" },
            { FilterOperator.EndsWith,"Like" },
            { FilterOperator.Eq,"=" },
            { FilterOperator.Gt,">" },
            { FilterOperator.Ge,">=" },
            { FilterOperator.Lt,"<" },
            { FilterOperator.Le,"<=" },
            { FilterOperator.Ne,"!=" },
            { FilterOperator.In,"In" }
        };


        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        /// <exception cref="OneInException"></exception>
        public static FieldValueType GetFieldType(string? fieldType)
        {
            if (Enum.TryParse(fieldType, out FieldValueType value))
            {
                return value;
            }
            else
            {
                return FieldValueType.String;
            }
        }


        /// <summary>
        /// 获取连接符
        /// </summary>
        /// <param name="concat"></param>
        /// <returns></returns>
        /// <exception cref="OneInException"></exception>
        public static FilterConcat GetConcat(string? concat)
        {
            if (Enum.TryParse(concat, out FilterConcat value))
            {
                return value;
            }
            else
            {
                return FilterConcat.And;
            }
        }


        /// <summary>
        /// 获取操作符
        /// </summary>
        /// <param name="operator"></param>
        /// <returns></returns>
        /// <exception cref="OneInException"></exception>
        public static FilterOperator GetFilterOperator(string? @operator)
        {
            if (Enum.TryParse(@operator, out FilterOperator value))
            {
                return value;
            }
            else
            {
                throw new Exception("未知的操作符");
            }
        }


        /// <summary>
        /// 替换符号
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static object? SqlReplace(object? value)
        {
            if (value != null)
            {
                if (value is string fiValueString)
                {
                    if (!string.IsNullOrWhiteSpace(fiValueString))
                    {
                        //return MySqlHelper.EscapeString(fiValueString);// 依赖MySqlConnector
                        return fiValueString.Replace("'", "\'");
                    }
                }
            }
            return value;
        }


        /// <summary>
        /// 将字符串转成数字
        /// </summary>
        /// <remarks>处理不同形式的浮点数表示，如整数、小数点后有或没有零等</remarks>
        /// <param name="value">包含整数或浮点数值的字符串</param>
        /// <returns></returns>
        public static object ConvertToNumber(string value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            return value;
        }


        /// <summary>
        /// 获取数组值
        /// </summary>
        /// <param name="filterInfo"></param>
        /// <returns>当操作位为In时</returns>
        public static string[] GetFilterListValue(FilterInfo filterInfo)
        {
            if (filterInfo.FieldValue is string fiValueString) return fiValueString.Split(',');
            if (filterInfo.FieldValue is IEnumerable fiValueIe)
            {
                var fiValueList = new List<string>();
                foreach (var fiValueIeItem in fiValueIe)
                    fiValueList.Add(string.Concat(fiValueIeItem));
                return fiValueList.ToArray();
            }
            return new string[0];
        }

        /// <summary>
        /// 获取类型值
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        /// <param name="isString"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static object? GetRealValue(FilterOperator @operator, string? value, FieldValueType? valueType, out bool isString)
        {
            object? realValue = value;
            isString = true;
            if (value != null && valueType != null)
            {
                switch (valueType)
                {
                    case FieldValueType.DateTime:
                        realValue = DateTime.Parse(value);
                        break;
                    case FieldValueType.String:
                        realValue = value;
                        break;
                    case FieldValueType.Boolean:
                        realValue = bool.Parse(value);
                        isString = false;
                        break;
                    case FieldValueType.Number:
                        realValue = FilterHelper.ConvertToNumber(value);
                        isString = false;
                        break;
                    default:
                        return value;
                }
            }

            return realValue;
        }

        /// <summary>
        /// 获取类型值
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private object? GetRealValue(FilterOperator @operator, string? value, string valueType)
        {
            object? realValue = null;

            if (value != null)
            {
                switch (valueType.ToLower())
                {
                    case "int":
                        //realValue = value
                        realValue = int.Parse(value);
                        break;
                    case "float":
                        realValue = float.Parse(value);
                        break;
                    case "double":
                        realValue = double.Parse(value);
                        break;
                    case "decimal":
                        realValue = decimal.Parse(value);
                        break;
                    case "time":
                        realValue = DateTime.Parse(value);
                        break;
                    case "datetime":
                        realValue = DateTime.Parse(value);
                        break;
                    case "string":
                        realValue = value;
                        break;
                    case "enum":
                        realValue = Enum.Parse(typeof(Enum), value);
                        break;
                    case "boolean":
                        realValue = bool.Parse(value);
                        break;
                    case "number":
                        realValue = FilterHelper.ConvertToNumber(value);
                        break;
                    default:
                        throw new NotSupportedException("不支持的类型转换");
                }
            }

            return realValue;
        }
    }
}

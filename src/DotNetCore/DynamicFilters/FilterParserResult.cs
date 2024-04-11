using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore.DynamicFilters
{
    public class FilterParserResult
    {
        /// <summary>
        /// 条件
        /// </summary>
        public StringBuilder Filters { get; set; } = new StringBuilder();

        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, object>? Parameters { get; set; }

        public int CurrentParameterIndex { get; set; } = 0;

        public override string ToString()
        {
            return Filters.ToString();
        }
    }
}

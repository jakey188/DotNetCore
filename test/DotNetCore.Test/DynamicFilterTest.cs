using DotNetCore.DynamicFilters;
using System.IO;

namespace DotNetCore.Test
{
    public class DynamicFilterTest
    {
        public class FilterParser1_UnitTest
        {
            private string userTable = "user";
            private string departmentTable = "department";
            private string tagTable = "tag";

            [Fact]
            public void FilterInfo_CreateFilter1()
            {
                var filter = new FilterInfo(table: userTable, fieldName: "id", @operator: FilterOperator.Eq, fieldValue: 1);

                var result = new FilterParser().CreateFilter(filter);

                Assert.Equal("`user`.`id` = @p1_user_id", result.ToString());
            }


            [Fact]
            public void FilterInfo_CreateFilter2()
            {
                var filter1 = new FilterInfo(table: userTable, fieldName: "id", @operator: FilterOperator.Eq, fieldValue: 1);

                var filter = new FilterInfo
                {
                    Concat = FilterConcat.And,
                    Filters = new List<FilterInfo>
                {
                    filter1,
                    filter1
                }
                };

                var result = new FilterParser().CreateFilter(filter);

                Assert.Equal("`user`.`id` = @p1_user_id AND `user`.`id` = @p2_user_id", result.ToString());
            }

            [Fact]
            public void FilterInfo_CreateFilter3()
            {
                var filter1 = new FilterInfo(table: userTable, fieldName: "id", @operator: FilterOperator.Eq, fieldValue: 1);

                var filter = new FilterInfo
                {
                    Concat = FilterConcat.Or,
                    Filters = new List<FilterInfo>
                {
                    filter1,
                    filter1
                }
                };

                var result = new FilterParser().CreateFilter(filter);

                Assert.Equal("(`user`.`id` = @p1_user_id OR `user`.`id` = @p2_user_id)", result.ToString());
            }

            [Fact]
            public void FilterInfo_CreateFilter4()
            {
                var filter1 = new FilterInfo(table: userTable, fieldName: "id", @operator: FilterOperator.Eq, fieldValue: 1);

                var filter = new FilterInfo
                {
                    Concat = FilterConcat.And,
                    Filters = new List<FilterInfo>
                {
                        filter1,
                    new FilterInfo
                    {
                        Concat = FilterConcat.Or,
                        Filters  = new List<FilterInfo>
                        {
                            filter1,
                            filter1
                        }
                    }
                }
                };

                var result = new FilterParser().CreateFilter(filter);

                Assert.Equal("`user`.`id` = @p1_user_id AND (`user`.`id` = @p2_user_id OR `user`.`id` = @p3_user_id)", result.ToString());
            }
        }
    }
}
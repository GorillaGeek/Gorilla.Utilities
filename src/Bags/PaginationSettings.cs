
using Gorilla.Utilities.Enums;

namespace Gorilla.Utilities.Bags
{
    public class PaginationSettings
    {

        public PaginationSettings(string orderColumn)
        {
            Skip = 0;
            Take = 10;
            OrderDirection = SortOrder.Ascending;
            OrderColumn = orderColumn;
        }

        public PaginationSettings(string orderColumn, int pageSize, int page)
        {
            Take = pageSize;
            Skip = (page - 1) * pageSize;
            Page = page;
            OrderColumn = orderColumn;
            OrderDirection = SortOrder.Ascending;
        }

        public int Page { get; set; }

        public int Skip { get; set; }

        /// <summary>
        /// Take
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Search
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Order Column
        /// </summary>
        public string OrderColumn { get; set; }

        /// <summary>
        /// Order Direction
        /// </summary>
        public SortOrder OrderDirection { get; set; }
    }
}

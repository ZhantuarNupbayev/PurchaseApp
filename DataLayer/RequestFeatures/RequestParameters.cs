using System;

namespace DataLayer.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; }
    }

    public class ProductParameters : RequestParameters { }

    public class PurchaseParameters : RequestParameters
    {
        public PurchaseParameters()
        {
            OrderBy = "totalprice";
        }
        public string StartDate { get; set; }
        public string EndDate { get; set; } = DateTime.MaxValue.ToString();

        public bool ValidDateRange => Convert.ToDateTime(EndDate) > Convert.ToDateTime(StartDate);

        public string SearchTerm { get; set; }
    }

}

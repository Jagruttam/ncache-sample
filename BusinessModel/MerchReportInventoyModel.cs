using BusinessCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Models.FramesDashboard
{
    [Serializable]
    public class MerchReportInventoyModel
    {
        [NCacheKeyField("Inventory", Index = 1)]
        public DateTime? FullDate_CY { get; set; }
        [NCacheKeyField(Index = 2)]
        public DateTime? FullDate_LY { get; set; }
        [NCacheKeyField(Index = 3)]
        public int? ItemID { get; set; }
        [NCacheKeyField(Index = 4)]
        public int StoreNumber { get; set; }
        public int? OnHand_LY { get; set; }
        public int? OnHand_CY { get; set; }
        public decimal? DefaultCost_CY { get; set; }
        public decimal? DefaultCost_LY { get; set; }
        public decimal? Retail_CY { get; set; }
        public decimal? Retail_LY { get; set; }
        public int? RetailRangeId { get; set; }
        public int? ParentRetailRangeId { get; set; }
        public int? GrandParentRetailRangeId { get; set; }
    }
}

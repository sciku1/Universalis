﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Universalis.Application.Views
{
    public class HistoryView
    {
        [JsonProperty("entries")]
        public List<MinimizedSaleView> Sales { get; set; } = new();

        [JsonProperty("itemID")]
        public uint ItemId { get; set; }

        [JsonProperty("worldID", NullValueHandling = NullValueHandling.Ignore)]
        public uint? WorldId { get; set; }

        [JsonProperty("worldName", NullValueHandling = NullValueHandling.Ignore)]
        public string WorldName { get; set; }

        [JsonProperty("dcName", NullValueHandling = NullValueHandling.Ignore)]
        public string DcName { get; set; }

        [JsonProperty("lastUploadTime")]
        public double LastUploadTimeUnixMilliseconds { get; set; }

        [JsonProperty("stackSizeHistogram")]
        public SortedDictionary<int, int> StackSizeHistogram { get; set; } = new();

        [JsonProperty("stackSizeHistogramNQ")]
        public SortedDictionary<int, int> StackSizeHistogramNq { get; set; } = new();

        [JsonProperty("stackSizeHistogramHQ")]
        public SortedDictionary<int, int> StackSizeHistogramHq { get; set; } = new();

        [JsonProperty("regularSaleVelocity")]
        public float SaleVelocity { get; set; }

        [JsonProperty("nqSaleVelocity")]
        public float SaleVelocityNq { get; set; }

        [JsonProperty("hqSaleVelocity")]
        public float SaleVelocityHq { get; set; }
    }
}
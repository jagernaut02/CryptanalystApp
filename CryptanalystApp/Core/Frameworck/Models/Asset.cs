﻿using UIControl;

namespace CryptanalystApp.Core.Frameworck.Models
{
    public class Asset : IAsset
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal PriceUsd { get; set; }
    }
}

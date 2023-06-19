using Caliburn.Micro;
using System;

namespace CryptanalystApp.Core.Frameworck.Models
{
    public class Coin : PropertyChangedBase
    {
        private decimal _volumeUsd24Hr;
        private decimal _priceUsd;
        private decimal _changePercent24Hr;

        public string Id { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal VolumeUsd24Hr
        {
            get { return _volumeUsd24Hr; }
            set 
            { 
                _volumeUsd24Hr =  Math.Round(value, 2);
                NotifyOfPropertyChange(() => VolumeUsd24Hr);
            }
        }
        public decimal PriceUsd 
        { 
            get { return _priceUsd; }
            set
            {
                _priceUsd = Math.Round(value, 2);
                NotifyOfPropertyChange(() => PriceUsd);
            }
        }
        public decimal ChangePercent24Hr 
        { 
            get { return _changePercent24Hr; }
            set
            {
                _changePercent24Hr = Math.Round(value, 2);
                NotifyOfPropertyChange(() => ChangePercent24Hr);
            }
        }   
    }
}

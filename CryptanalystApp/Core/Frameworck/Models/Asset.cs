using UIControl;

namespace CryptanalystApp.Core.Frameworck.Models
{
    public class Asset : IAsset
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal PriceUsd { get; set; }

        //private string _id;
        //private int _rank;
        //private string _symbol;
        //private string _name;
        //private decimal _priceUsd;

        //public string Id 
        //{
        //    get { return _id; }
        //    set 
        //    {
        //        _id = value; 
        //        NotifyOfPropertyChange(() => Id); 
        //    }
        //}
        //public int Rank 
        //{ 
        //    get { return _rank; }
        //    set
        //    {
        //        _rank = value;
        //        NotifyOfPropertyChange(() => Rank);
        //    }
        //}
        //public string Symbol
        //{
        //    get { return _symbol; }
        //    set
        //    {
        //        _symbol = value;
        //        NotifyOfPropertyChange(() => Symbol);
        //    }
        //}

        //public string Name
        //{
        //    get { return _name; }
        //    set 
        //    { 
        //        _name = value;
        //        NotifyOfPropertyChange(() => Name); 
        //    }
        //}
        //public decimal PriceUsd
        //{
        //    get { return _priceUsd; }
        //    set
        //    {
        //        _priceUsd = value;
        //        NotifyOfPropertyChange(() => PriceUsd);
        //    }
        //}

        public Asset() { }
    }
}

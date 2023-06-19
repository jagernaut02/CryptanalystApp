
namespace UIControl
{
    public interface IAsset 
    {
        string Id { get; set; }
        int Rank { get; set; }
        string Symbol { get; set; }
        string Name { get; set; }
        decimal PriceUsd { get; set; }
    }
}

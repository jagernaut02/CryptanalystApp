using Caliburn.Micro;
using System.Collections.Generic;
using UIControl;

namespace CryptanalystApp.Core.Frameworck.Services
{
    public interface IShell
    {
        IEnumerable<IAsset> AllAssets { get; set; }
        object ActiveOptions { get; set; }

        IObservableCollection<IScreen> GetDocuments();

        void OpenDescriptionView(string id);

        void ChangeCurrentActiveItem(IScreen view);

        void ChangeCurrentActiveItem(IScreen view, int index);
    }
}

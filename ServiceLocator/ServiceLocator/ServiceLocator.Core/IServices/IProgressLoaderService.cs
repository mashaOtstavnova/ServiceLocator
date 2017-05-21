using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocator.Core.IServices
{
    public interface IProgressLoaderService
    {
        void ShowProgressBar();
        void HideProgressBar();
    }
}

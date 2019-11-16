using System;
using System.Threading.Tasks;

namespace ResilITApp.Control
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}

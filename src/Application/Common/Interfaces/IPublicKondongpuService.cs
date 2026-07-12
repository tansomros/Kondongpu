namespace Kondongpu.Application.Common.Interfaces
{

    /// <summary>
    /// บริการสำหรับ
    /// </summary>
    public interface IPublicKondongpuService
    {
        Task GetCheckupResult(string hospitalNumber);
    }
}

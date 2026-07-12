namespace Kondongpu.Application.Common.Interfaces
{
    public interface ISuthAppService
    {
        Task NotifyCallCheckup(
            string hospitalNumber,
            string checkup,
            string location,
            int roomService);
    }
}

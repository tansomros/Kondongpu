using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}

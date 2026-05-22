using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IAdminService
{
    Task<IReadOnlyList<UserDto>> ManageUsersAsync(CancellationToken cancellationToken = default);
    Task<ReportDto> ViewReportsAsync(CancellationToken cancellationToken = default);
}

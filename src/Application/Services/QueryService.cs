using Application.DTOs;
using Application.Common.Interfaces;

public class QueryService : IQuery  // Sửa IQueryable thành IQuery
{
    private readonly IUserRepository _userRepository;

    public QueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserDTO>> QueryTableAsync(
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await _userRepository.GetUsersPagedAsync(page, pageSize, ct);

        var userDTOs = result.Items.Select(u => new UserDTO
        {
            UserName = u?.UserName,
            UserEmail = u?.UserEmail
        }).ToList();

        return new PagedResult<UserDTO>(userDTOs, result.TotalCount, result.Page, result.PageSize);
    }
}

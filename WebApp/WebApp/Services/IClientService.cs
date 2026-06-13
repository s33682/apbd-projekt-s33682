using WebApp.DTOs;

namespace WebApp.Services;

public interface IClientService
{
    public Task AddIndividualClient(PostNewIndividualClientDto dto);
    public Task AddCompanyClient(PostNewCompanyClientDto dto);
    public Task DeleteClient(int clientId);
    public Task UpdateIndividualClient(int clientId, PutUpdateIndividualClientDto dto);
    public Task UpdateCompanyClient(int clientId, PutUpdateCompanyClientDto dto);
}
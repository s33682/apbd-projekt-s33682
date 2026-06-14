using WebApp.DTOs;

namespace WebApp.Services;

public interface IContractService
{
    public Task CreateContract(PostNewContractDto dto);
    public Task AddContractPayment(int contractId, PostContractPaymentDto dto);
}
using WebApp.DTOs;

namespace WebApp.Services;

public interface ISubsriptionService
{
    public Task CreateSubscription(PostCreateSubscriptionDto dto);
    public Task AddSubscriptionPayment(int subscriptionId, PostSubscriptionPaymentDto dto);
}
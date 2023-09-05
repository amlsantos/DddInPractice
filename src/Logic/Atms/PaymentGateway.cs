namespace Logic.Atms;

public class PaymentGateway : IPaymentGateway
{
    public void ChargePayment(decimal amount)
    {
        var guid = Guid.NewGuid();

        Console.WriteLine($"Payment {guid} with amount {amount}, was charged successfully");
    }
}
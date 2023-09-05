namespace Logic.Atms;

public interface IPaymentGateway
{
    public void ChargePayment(decimal amount);
}
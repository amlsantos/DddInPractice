namespace Logic.Atms;

public class AtmDto
{
    public AtmDto(long id, decimal cash)
    {
        Id = id;
        Cash = cash;
    }

    public long Id { get; private set; }
    public decimal Cash { get; private set; }
}
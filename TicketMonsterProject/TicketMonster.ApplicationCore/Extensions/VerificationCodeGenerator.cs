namespace TicketMonster.ApplicationCore.Extensions;

public static class VerificationCodeGenerator
{
    public static string GenerateVerificationCode(int length)
    {
        string code = Guid.NewGuid().ToString("N")[..length];
        return code;
    }
}
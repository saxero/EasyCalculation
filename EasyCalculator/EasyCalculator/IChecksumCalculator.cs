
namespace EasyCalculator
{
    public interface IChecksumCalculator
    {
        bool VerifyChecksum(string message);

        string CalculateChecksum(string message);
    }
}

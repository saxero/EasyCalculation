
namespace EasyCalculator
{
    public interface IChecksumCalculator
    {
        bool VerifyChecksum(string message);

        string GetMessageWithChecksumHeaderAndFooter(string message);
    }
}

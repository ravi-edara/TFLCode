using System.Threading.Tasks;

namespace TFLCodeChallenge
{
    public interface IRoadStatusService
    {
        Task GetRoadStatus();

        Task<Road> GetRoadStatusById(string roadId);
    }
}
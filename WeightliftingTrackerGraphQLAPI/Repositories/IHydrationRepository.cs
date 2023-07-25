using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public interface IHydrationRepository
    {
        Task<IEnumerable<Hydration>> GetHydrations();
        Task<Hydration> GetHydrationById(int id);
        Task<Hydration> CreateHydration(Hydration newHydration);
        Task<Hydration> UpdateHydration(Hydration updatedHydration);
        Task<Hydration> DeleteHydration(int hydrationId);
    }
}

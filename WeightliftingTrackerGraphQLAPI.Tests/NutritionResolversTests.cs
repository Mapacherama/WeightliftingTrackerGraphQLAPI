using NUnit.Framework;
using Moq;
using MySql.Data.MySqlClient;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Repositories;

namespace WeightliftingTrackerGraphQLAPI.Tests
{
    public class NutritionResolversTests
    {
        private Mock<IMySqlDataAccess>? _mockDataAccess;
        private Mock<INutritionRepository>? _mockNutritionRepository;
        private NutritionResolvers? _nutritionResolvers;

        private NutritionDTO? _testNutrition;

        [SetUp]
        public void SetUp()
        {
            _mockDataAccess = new Mock<IMySqlDataAccess>();
            _mockNutritionRepository = new Mock<INutritionRepository>();

            _nutritionResolvers = new NutritionResolvers(_mockNutritionRepository.Object);

            _testNutrition = new NutritionDTO
            {
                Id = 1,
                Calories = 2000,
                Protein = 150,
                Carbohydrates = 250,
                Fats = 70
            };
        }
    }
}

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
                FoodName = "Test Food",
                Calories = 200,
                Protein = 10,
                Carbohydrates = 30,
                Fats = 5
            };
        }

        [Test]
        public async Task GetNutritions_Returns_Nutritions_List()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("FoodName", typeof(string));
            dt.Columns.Add("Calories", typeof(float));
            dt.Columns.Add("Protein", typeof(float));
            dt.Columns.Add("Carbohydrates", typeof(float));
            dt.Columns.Add("Fats", typeof(float));

            DataRow row = dt.NewRow();
            row["Id"] = _testNutrition.Id;
            row["FoodName"] = _testNutrition.FoodName;
            row["Calories"] = _testNutrition.Calories;
            row["Protein"] = _testNutrition.Protein;
            row["Carbohydrates"] = _testNutrition.Carbohydrates;
            row["Fats"] = _testNutrition.Fats;
            dt.Rows.Add(row);

            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync(dt);
            _mockNutritionRepository.Setup(r => r.GetNutritions()).ReturnsAsync(new List<NutritionDTO> { _testNutrition });

            var result = await _nutritionResolvers.GetNutritions();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<NutritionDTO>>(result);
            var nutritionsList = result as IList<NutritionDTO>;
            Assert.AreEqual(1, nutritionsList.Count);
            Assert.AreEqual(_testNutrition.Id, nutritionsList[0].Id);
            Assert.AreEqual(_testNutrition.FoodName, nutritionsList[0].FoodName);
            Assert.AreEqual(_testNutrition.Calories, nutritionsList[0].Calories);
            Assert.AreEqual(_testNutrition.Protein, nutritionsList[0].Protein);
            Assert.AreEqual(_testNutrition.Carbohydrates, nutritionsList[0].Carbohydrates);
            Assert.AreEqual(_testNutrition.Fats, nutritionsList[0].Fats);
        }

        [Test]
        public async Task CreateNutrition_Returns_New_Nutrition()
        {
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));
            _mockDataAccess.Setup(d => d.ExecuteScalarAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync(_testNutrition.Id);
            _mockNutritionRepository.Setup(r => r.CreateNutrition(It.IsAny<Nutrition>()))
                .ReturnsAsync((Nutrition newNutrition) => newNutrition);

            var nutrition = new Nutrition
            {
                Id = _testNutrition.Id,
                FoodName = _testNutrition.FoodName,
                Calories = _testNutrition.Calories,
                Protein = _testNutrition.Protein,
                Carbohydrates = _testNutrition.Carbohydrates,
                Fats = _testNutrition.Fats
            };

            var result = await _nutritionResolvers.CreateNutrition(nutrition);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Nutrition>(result);
            Assert.AreEqual(_testNutrition.Id, result.Id);
            Assert.AreEqual(_testNutrition.FoodName, result.FoodName);
            Assert.AreEqual(_testNutrition.Calories, result.Calories);
            Assert.AreEqual(_testNutrition.Protein, result.Protein);
            Assert.AreEqual(_testNutrition.Carbohydrates, result.Carbohydrates);
            Assert.AreEqual(_testNutrition.Fats, result.Fats);
        }

        [Test]
        public async Task CreateNutrition_ThrowsException_When_NutritionIsNull()
        {
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));
            _mockDataAccess.Setup(d => d.ExecuteScalarAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));

            _mockNutritionRepository.Setup(r => r.CreateNutrition(It.IsAny<Nutrition>()))
                .ThrowsAsync(new ArgumentNullException());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _nutritionResolvers.CreateNutrition(null));
        }

        [Test]
        public async Task UpdateNutrition_Returns_Updated_Nutrition()
        {
            _testNutrition.FoodName = "Updated Test Food";
            _testNutrition.Calories = 250;
            _testNutrition.Protein = 12;
            _testNutrition.Carbohydrates = 35;
            _testNutrition.Fats = 7;

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("FoodName", typeof(string));
            dt.Columns.Add("Calories", typeof(float));
            dt.Columns.Add("Protein", typeof(float));
            dt.Columns.Add("Carbohydrates", typeof(float));
            dt.Columns.Add("Fats", typeof(float));

            DataRow row = dt.NewRow();
            row["Id"] = _testNutrition.Id;
            row["FoodName"] = _testNutrition.FoodName;
            row["Calories"] = _testNutrition.Calories;
            row["Protein"] = _testNutrition.Protein;
            row["Carbohydrates"] = _testNutrition.Carbohydrates;
            row["Fats"] = _testNutrition.Fats;
            dt.Rows.Add(row);

            var nutrition = new Nutrition
            {
                Id = _testNutrition.Id,
                FoodName = _testNutrition.FoodName,
                Calories = _testNutrition.Calories,
                Protein = _testNutrition.Protein,
                Carbohydrates = _testNutrition.Carbohydrates,
                Fats = _testNutrition.Fats
            };

            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync(dt);
            _mockNutritionRepository.Setup(r => r.UpdateNutrition(It.IsAny<Nutrition>()))
                .ReturnsAsync((Nutrition newNutrition) => newNutrition);

            var result = await _nutritionResolvers.UpdateNutrition(nutrition);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Nutrition>(result);
            Assert.AreEqual(_testNutrition.Id, result.Id);
            Assert.AreEqual(_testNutrition.FoodName, result.FoodName);
            Assert.AreEqual(_testNutrition.Calories, result.Calories);
            Assert.AreEqual(_testNutrition.Protein, result.Protein);
            Assert.AreEqual(_testNutrition.Carbohydrates, result.Carbohydrates);
            Assert.AreEqual(_testNutrition.Fats, result.Fats);
        }

        [Test]
        public async Task UpdateNutrition_ThrowsException_When_NutritionIsNull()
        {
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));

            _mockNutritionRepository.Setup(r => r.UpdateNutrition(It.IsAny<Nutrition>()))
                .ThrowsAsync(new ArgumentNullException());

            Assert.ThrowsAsync<ArgumentNullException>(() => _nutritionResolvers.UpdateNutrition(null));
        }

        [Test]
        public async Task UpdateNutrition_ThrowsException_When_NutritionDoesNotExist()
        {
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync((DataTable)null);
            _mockNutritionRepository.Setup(r => r.UpdateNutrition(It.IsAny<Nutrition>())).ThrowsAsync(new Exception());

            var nutrition = new Nutrition
            {
                Id = _testNutrition.Id,
                FoodName = _testNutrition.FoodName,
                Calories = _testNutrition.Calories,
                Protein = _testNutrition.Protein,
                Carbohydrates = _testNutrition.Carbohydrates,
                Fats = _testNutrition.Fats
            };

            Assert.ThrowsAsync<Exception>(async () => await _nutritionResolvers.UpdateNutrition(nutrition), $"No nutrition found with ID: {_testNutrition.Id}");
        }
    }
}

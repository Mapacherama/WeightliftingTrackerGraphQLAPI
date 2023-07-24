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
    public class WorkoutResolversTests
    {
        private Mock<IMySqlDataAccess>? _mockDataAccess;
        private Mock<IWorkoutRepository>? _mockWorkoutRepository;
        private WorkoutResolvers? _workoutResolvers;

        private WorkoutDTO? _testWorkout;

        [SetUp]
        public void SetUp()
        {            
            _mockDataAccess = new Mock<IMySqlDataAccess>();
            _mockWorkoutRepository = new Mock<IWorkoutRepository>();
            
            _workoutResolvers = new WorkoutResolvers(_mockWorkoutRepository.Object);

            _testWorkout = new WorkoutDTO
            {
                Id = 1,
                ExerciseName = "Test Exercise",
                Sets = 3,
                Reps = 10,
                Weight = 50
            };
            
        }

        [Test]
        public async Task GetWorkouts_Returns_Workouts_List()
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ExerciseName", typeof(string));
            dt.Columns.Add("Sets", typeof(int));
            dt.Columns.Add("Reps", typeof(int));
            dt.Columns.Add("Weight", typeof(decimal));

            DataRow row = dt.NewRow();
            row["Id"] = _testWorkout.Id;
            row["ExerciseName"] = _testWorkout.ExerciseName;
            row["Sets"] = _testWorkout.Sets;
            row["Reps"] = _testWorkout.Reps;
            row["Weight"] = _testWorkout.Weight;
            dt.Rows.Add(row);

            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync(dt);
            _mockWorkoutRepository.Setup(r => r.GetWorkouts()).ReturnsAsync(new List<WorkoutDTO> { _testWorkout });

            // Act
            var result = await _workoutResolvers.GetWorkouts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<WorkoutDTO>>(result);
            var workoutsList = result as IList<WorkoutDTO>;
            Assert.AreEqual(1, workoutsList.Count);
            Assert.AreEqual(_testWorkout.Id, workoutsList[0].Id);
            Assert.AreEqual(_testWorkout.ExerciseName, workoutsList[0].ExerciseName);
            Assert.AreEqual(_testWorkout.Sets, workoutsList[0].Sets);
            Assert.AreEqual(_testWorkout.Reps, workoutsList[0].Reps);
            Assert.AreEqual(_testWorkout.Weight, workoutsList[0].Weight);
        }

        [Test]
        public async Task CreateWorkout_Returns_New_Workout()
        {
            // Arrange
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));
            _mockDataAccess.Setup(d => d.ExecuteScalarAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync(_testWorkout.Id);
            _mockWorkoutRepository.Setup(r => r.CreateWorkout(It.IsAny<Nutrition>()))
       .ReturnsAsync((Nutrition newWorkout) => newWorkout);

            var workout = new Nutrition
            {
                Id = _testWorkout.Id,
                ExerciseName = _testWorkout.ExerciseName,
                Sets = _testWorkout.Sets,
                Reps = _testWorkout.Reps,
                Weight = _testWorkout.Weight
            };           

            // Act
            var result = await _workoutResolvers.CreateWorkout(workout);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Nutrition>(result);
            Assert.AreEqual(_testWorkout.Id, result.Id);
            Assert.AreEqual(_testWorkout.ExerciseName, result.ExerciseName);
            Assert.AreEqual(_testWorkout.Sets, result.Sets);
            Assert.AreEqual(_testWorkout.Reps, result.Reps);
            Assert.AreEqual(_testWorkout.Weight, result.Weight);
        }

        [Test]
        public async Task CreateWorkout_ThrowsException_When_WorkoutIsNull()
        {
            
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));
            _mockDataAccess.Setup(d => d.ExecuteScalarAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));

            _mockWorkoutRepository.Setup(r => r.CreateWorkout(It.IsAny<Nutrition>()))
    .ThrowsAsync(new ArgumentNullException());

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _workoutResolvers.CreateWorkout(null));

        }

        [Test]
        public async Task UpdateWorkout_Returns_Updated_Workout()
        {
            // Arrange
            _testWorkout.ExerciseName = "Updated Test Exercise";
            _testWorkout.Sets = 5;
            _testWorkout.Reps = 12;
            _testWorkout.Weight = 60;

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ExerciseName", typeof(string));
            dt.Columns.Add("Sets", typeof(int));
            dt.Columns.Add("Reps", typeof(int));
            dt.Columns.Add("Weight", typeof(decimal));

            DataRow row = dt.NewRow();
            row["Id"] = _testWorkout.Id;
            row["ExerciseName"] = _testWorkout.ExerciseName;
            row["Sets"] = _testWorkout.Sets;
            row["Reps"] = _testWorkout.Reps;
            row["Weight"] = _testWorkout.Weight;
            dt.Rows.Add(row);

            var workout = new Nutrition
            {
                Id = _testWorkout.Id,
                ExerciseName = _testWorkout.ExerciseName,
                Sets = _testWorkout.Sets,
                Reps = _testWorkout.Reps,
                Weight = _testWorkout.Weight
            };

            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync(dt);
            _mockWorkoutRepository.Setup(r => r.UpdateWorkout(It.IsAny<Nutrition>()))
            .ReturnsAsync((Nutrition newWorkout) => newWorkout);

            var result = await _workoutResolvers.UpdateWorkout(workout);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Nutrition>(result);
            Assert.AreEqual(_testWorkout.Id, result.Id);
            Assert.AreEqual(_testWorkout.ExerciseName, result.ExerciseName);
            Assert.AreEqual(_testWorkout.Sets, result.Sets);
            Assert.AreEqual(_testWorkout.Reps, result.Reps);
            Assert.AreEqual(_testWorkout.Weight, result.Weight);
        }

        [Test]
        public async Task UpdateWorkout_ThrowsException_When_WorkoutIsNull()
        {
            
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));

            _mockWorkoutRepository.Setup(r => r.UpdateWorkout(It.IsAny<Nutrition>()))
    .ThrowsAsync(new ArgumentNullException());

            Assert.ThrowsAsync<ArgumentNullException>(() => _workoutResolvers.UpdateWorkout(null));

        }

        [Test]
        public async Task UpdateWorkout_ThrowsException_When_WorkoutDoesNotExist()
        {
            _mockDataAccess.Setup(d => d.ExecuteQueryAsync(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).ReturnsAsync((DataTable)null);
            _mockWorkoutRepository.Setup(r => r.UpdateWorkout(It.IsAny<Nutrition>())).ThrowsAsync(new Exception());

            var workout = new Nutrition
            {
                Id = _testWorkout.Id,
                ExerciseName = _testWorkout.ExerciseName,
                Sets = _testWorkout.Sets,
                Reps = _testWorkout.Reps,
                Weight = _testWorkout.Weight
            };


            Assert.ThrowsAsync<Exception>(async() => await _workoutResolvers.UpdateWorkout(workout), $"No workout found with ID: {_testWorkout.Id}");
        }


    }
}

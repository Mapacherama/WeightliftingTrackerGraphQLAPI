using NUnit.Framework;
using Moq;
using MySql.Data.MySqlClient;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;
using System.Data;

namespace WeightliftingTrackerGraphQLAPI.Tests
{
    public class WorkoutResolversTests
    {
        private Mock<IMySqlDataAccess>? _mockDataAccess;
        private WorkoutResolvers? _workoutResolvers;
        private Workout? _testWorkout;

        [SetUp]
        public void SetUp()
        {
            _mockDataAccess = new Mock<IMySqlDataAccess>();
            _workoutResolvers = new WorkoutResolvers(_mockDataAccess.Object);

            _testWorkout = new Workout
            {
                Id = 1,
                ExerciseName = "Test Exercise",
                Sets = 3,
                Reps = 10,
                Weight = 50
            };
        }

        [Test]
        public void GetWorkouts_Returns_Workouts_List()
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

            _mockDataAccess.Setup(d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).Returns(dt);

            // Act
            var result = _workoutResolvers.GetWorkouts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Workout>>(result);
            var workoutsList = result as IList<Workout>;
            Assert.AreEqual(1, workoutsList.Count);
            Assert.AreEqual(_testWorkout.Id, workoutsList[0].Id);
            Assert.AreEqual(_testWorkout.ExerciseName, workoutsList[0].ExerciseName);
            Assert.AreEqual(_testWorkout.Sets, workoutsList[0].Sets);
            Assert.AreEqual(_testWorkout.Reps, workoutsList[0].Reps);
            Assert.AreEqual(_testWorkout.Weight, workoutsList[0].Weight);
        }

        [Test]
        public void CreateWorkout_Returns_New_Workout()
        {
            // Arrange
            _mockDataAccess.Setup(d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));
            _mockDataAccess.Setup(d => d.ExecuteScalar(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).Returns(_testWorkout.Id);

            // Act
            var result = _workoutResolvers.CreateWorkout(_testWorkout);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Workout>(result);
            Assert.AreEqual(_testWorkout.Id, result.Id);
            Assert.AreEqual(_testWorkout.ExerciseName, result.ExerciseName);
            Assert.AreEqual(_testWorkout.Sets, result.Sets);
            Assert.AreEqual(_testWorkout.Reps, result.Reps);
            Assert.AreEqual(_testWorkout.Weight, result.Weight);
        }

        [Test]
        public void CreateWorkout_ThrowsException_When_WorkoutIsNull()
        {
            
            _mockDataAccess.Setup(d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));
            _mockDataAccess.Setup(d => d.ExecuteScalar(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));

            
            Assert.Throws<ArgumentNullException>(() => _workoutResolvers.CreateWorkout(null));
        }

        [Test]
        public void UpdateWorkout_Returns_Updated_Workout()
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

            _mockDataAccess.Setup(d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).Returns(dt);
            
            var result = _workoutResolvers.UpdateWorkout(_testWorkout);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Workout>(result);
            Assert.AreEqual(_testWorkout.Id, result.Id);
            Assert.AreEqual(_testWorkout.ExerciseName, result.ExerciseName);
            Assert.AreEqual(_testWorkout.Sets, result.Sets);
            Assert.AreEqual(_testWorkout.Reps, result.Reps);
            Assert.AreEqual(_testWorkout.Weight, result.Weight);
        }

        [Test]
        public void UpdateWorkout_ThrowsException_When_WorkoutIsNull()
        {
            
            _mockDataAccess.Setup(d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<MySqlParameter[]>()));

            
            Assert.Throws<ArgumentNullException>(() => _workoutResolvers.UpdateWorkout(null));
        }

        [Test]
        public void UpdateWorkout_ThrowsException_When_WorkoutDoesNotExist()
        {
            _mockDataAccess.Setup(d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<MySqlParameter[]>())).Returns((DataTable)null);

            
            Assert.Throws<Exception>(() => _workoutResolvers.UpdateWorkout(_testWorkout), $"No workout found with ID: {_testWorkout.Id}");
        }


    }
}

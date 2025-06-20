using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Tests.Utilities;
using System;
using System.Data.SQLite;

namespace BMYLBH2025_SDDAP.Tests
{
    /// <summary>
    /// Base class for all unit tests providing common setup and teardown functionality
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        protected IDbConnectionFactory ConnectionFactory { get; private set; }
        protected string TestConnectionString { get; private set; }
        protected TestDbHelper DbHelper { get; private set; }
        protected MockDataGenerator MockData { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            // Create unique test database for each test
            var testDbName = $"test_{Guid.NewGuid():N}.db";
            TestConnectionString = $"Data Source=:memory:;Version=3;New=True;";
            
            // Initialize test infrastructure
            ConnectionFactory = new SqliteConnectionFactory(TestConnectionString);
            DbHelper = new TestDbHelper(ConnectionFactory);
            MockData = new MockDataGenerator();

            // Setup test database schema
            SetupTestDatabase();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            // Cleanup resources
            ConnectionFactory = null;
            DbHelper = null;
            MockData = null;
        }

        protected virtual void SetupTestDatabase()
        {
            try
            {
                DbHelper.CreateTestSchema();
                DbHelper.SeedTestData();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to setup test database: {ex.Message}");
            }
        }

        protected void AssertNotNull(object obj, string message = "Object should not be null")
        {
            Assert.IsNotNull(obj, message);
        }

        protected void AssertAreEqual<T>(T expected, T actual, string message = "Values should be equal")
        {
            Assert.AreEqual(expected, actual, message);
        }

        protected void AssertIsTrue(bool condition, string message = "Condition should be true")
        {
            Assert.IsTrue(condition, message);
        }

        protected void AssertIsFalse(bool condition, string message = "Condition should be false")
        {
            Assert.IsFalse(condition, message);
        }

        protected void AssertThrows<TException>(Action action, string message = "Expected exception was not thrown")
            where TException : Exception
        {
            try
            {
                action();
                Assert.Fail(message);
            }
            catch (TException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected {typeof(TException).Name} but got {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
} 
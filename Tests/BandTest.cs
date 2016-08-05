using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    // override the DB connection to use the test database
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      // Arrange, Act
      int result = Band.GetAll().Count;
      // Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameName()
    {
      //Arrange, Act
      Band firstBand = new Band("Best Band");
      Band secondBand = new Band("Best Bands");

      //Assert
      Assert.Equal(firstBand, secondBand);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

  }
}

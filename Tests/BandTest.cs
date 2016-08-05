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
      Band secondBand = new Band("Best Band");

      //Assert
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Band testBand = new Band("Tres Cool Band");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAll_GetsAllBandsFromDatabase()
    {
      //Arrange
      string name1 = "The Band";
      Band testBand1 = new Band(name1);
      testBand1.Save();
      string name2 = "New Band";
      Band testBand2 = new Band(name2);
      testBand2.Save();
      string name3 = "False Band";
      Band testBand3 = new Band(name3);
      testBand2.Save();


      List<Band> testBands = new List<Band> {testBand1, testBand2};
      //Act
      List<Band> resultBands = Band.GetAll();
      //Assert
      Assert.Equal(testBands.Count, resultBands.Count);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

  }
}

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
      List<Band> testBands = new List<Band> {testBand1, testBand2};

      //Act
      List<Band> resultBands = Band.GetAll();

      //Assert
      Assert.Equal(testBands.Count, resultBands.Count);
    }

    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Rabbit Band");
      testBand.Save();
      //Act
      int testId = testBand.GetId();
      Band foundBand = Band.Find(testId);
      //Assert
      Assert.Equal(testBand.GetName(), foundBand.GetName());
    }

    [Fact]
    public void Test_AddVenue_AddsVenueToBand()
    {
     //Arrange
     Band testBand = new Band("Band of Thieves");
     testBand.Save();

     Venue testVenue1 = new Venue("La Venue");
     testVenue1.Save();
     Venue testVenue2 = new Venue("Le Venue");
     testVenue2.Save();
     //Act
     List<Venue> resultList = new List<Venue>{};
     resultList.Add(testVenue1);
     List<Venue> testList = new List<Venue>{testVenue1};
     //Assert
     Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Test_GetVenues_GetsAllVenuesInThisBand()
    {
      //Arrange
      Band newBand = new Band("Bandito");
      newBand.Save();
      Venue name1Venue = new Venue("Ima Venue");
      name1Venue.Save();
      Venue name2Venue = new Venue("Yura Venue");
      name2Venue.Save();

      newBand.AddVenue(name1Venue);
      newBand.AddVenue(name2Venue);
      List<Venue> testBandVenues = new List<Venue> {name1Venue, name2Venue};
      //Act
      List<Venue> resultBandVenues = newBand.GetVenues();
      //Assert
      Assert.Equal(testBandVenues, resultBandVenues);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

  }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    // properties
    private string _name;
    private int _id;

    // constructors, getters, setters
    public Band(string name, int Id = 0)
    {
      _name = name;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }

    // other methods
    public override bool Equals(System.Object otherBand)
    {
      if(!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = this.GetId() == newBand.GetId();
        bool nameEquality = this.GetName() == newBand.GetName();
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@Name);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        Band newBand = new Band(studentName, studentId);
        allBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBands;
    }

    public static Band Find(int newId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = (@BandId);", conn);
      SqlParameter venueParameter = new SqlParameter();
      venueParameter.ParameterName = "@BandId";
      venueParameter.Value = newId;
      cmd.Parameters.Add(venueParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      int id = 0;
      string name = null;

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Band foundBand = new Band(name, id);
      return foundBand;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);
      SqlParameter venueParameter = new SqlParameter();
      venueParameter.ParameterName = "@VenueId";
      venueParameter.Value = this.GetId();
      cmd.Parameters.Add(venueParameter);

      SqlParameter bandParameter = new SqlParameter();
      bandParameter.ParameterName = "@BandId";
      bandParameter.Value = newVenue.GetId();
      cmd.Parameters.Add(bandParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Venue> GetVenues()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands ON (bands.id = venues_bands.band_id) JOIN venues ON (venues_bands.venue_id = venues.id) WHERE bands.id = @BandId;", conn);
     SqlParameter venueIdParameter = new SqlParameter();
     venueIdParameter.ParameterName = "@BandId";
     venueIdParameter.Value = this.GetId().ToString();

     cmd.Parameters.Add(venueIdParameter);

     SqlDataReader rdr = cmd.ExecuteReader();
     List<Venue> bands = new List<Venue> {};
     while(rdr.Read())
     {
       int thisVenueId = rdr.GetInt32(0);
       string bandName = rdr.GetString(1);
       Venue foundVenue = new Venue(bandName, thisVenueId);
       bands.Add(foundVenue);
     }
     if(rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return bands;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE from bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}

# _Band Tracker Database with C#, SQL, Nancy, and Razor_

#### _A site for storing venues and bands that play there_

#### By _**Russ Davies**_

## Description

This site will allow the user to add bands to a venue and venues to a band. It will also allow them to delete and edit any venue.

## Setup

 1. Clone this repository
 2. Run "DNU restore" on PowerShell in the cloned repository.
 3. Put the following into SQL Studio:
CREATE DATABASE band_tracker;
GO
USE band_tracker;
GO
CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255), stylist_id INT);
GO
CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));
GO
CREATE TABLE venues_bands (id INT IDENTITY(1,1), venue_id INT, band_id INT);
GO
 4. Create an additional database with the same previous steps called band_tracker_test.
 4. Then type in "DNX Kestrel" and enter.
 5. Go to your web browser and type in "LocalHost:5004" to view the homepage.

## Known Bugs
No known bugs

## Support and contact details
Please contact Russ Davies at russdavies392@gmail.com if you have any questions.

## Technologies Used
* C#
* Nancy
* Razor
* SQL

### License
Copyright (c) 2016 Russ Davies

This software is licensed under the MIT license.

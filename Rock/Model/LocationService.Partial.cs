﻿// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// Data access and service class for <see cref="Rock.Model.Location"/> entities.
    /// </summary>
    public partial class LocationService
    {
        /// <summary>
        /// Returns the first
        /// <see cref="Rock.Model.Location" /> where the address matches the provided address.  If no address is found with the provided values,
        /// the address will be standardized. If there is still not a match, the address will be saved as a new location.
        /// </summary>
        /// <param name="street1">A <see cref="System.String" /> representing the Address Line 1 to search by.</param>
        /// <param name="street2">A <see cref="System.String" /> representing the Address Line 2 to search by.</param>
        /// <param name="city">A <see cref="System.String" /> representing the City to search by.</param>
        /// <param name="state">A <see cref="System.String" /> representing the State to search by.</param>
        /// <param name="postalCode">A <see cref="System.String" /> representing the Zip/Postal code to search by</param>
        /// <param name="country">The country.</param>
        /// <param name="verifyLocation">if set to <c>true</c> [verify location].</param>
        /// <returns>
        /// The first <see cref="Rock.Model.Location" /> where an address match is found, if no match is found a new <see cref="Rock.Model.Location" /> is created and returned.
        /// </returns>
        public Location Get( string street1, string street2, string city, string state, string postalCode, string country, bool verifyLocation = true )
        {
            // Make sure it's not an empty address
            if ( string.IsNullOrWhiteSpace( street1 ) )
            {
                return null;
            }

            // First check if a location exists with the entered values
            Location existingLocation = Queryable().FirstOrDefault( t =>
                ( t.Street1 == street1 || ( ( street1 == null || street1 == "" ) && ( t.Street1 == null || t.Street1 == "" ) ) ) &&
                ( t.Street2 == street2 || ( ( street2 == null || street2 == "" ) && ( t.Street2 == null || t.Street2 == "" ) ) ) &&
                ( t.City == city || ( ( city == null || city == "" ) && ( t.City == null || t.City == "" ) ) ) &&
                ( t.State == state || ( ( state == null || state == "" ) && ( t.State == null || t.State == "" ) ) ) &&
                ( t.PostalCode == postalCode || ( ( postalCode == null || postalCode == "" ) && ( t.PostalCode == null || t.PostalCode == "" ) ) ) &&
                ( t.Country == country || ( ( country == null || country == "" ) && ( t.Country == null || t.Country == "" ) ) ) );
            if ( existingLocation != null )
            {
                return existingLocation;
            }

            // If existing location wasn't found with entered values, try standardizing the values, and
            // search for an existing value again
            var newLocation = new Location
            {
                Street1 = street1,
                Street2 = street2,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country
            };

            if ( verifyLocation )
            {
                Verify( newLocation, false );
            }

            existingLocation = Queryable().FirstOrDefault( t =>
                ( t.Street1 == newLocation.Street1 || ( ( newLocation.Street1 == null || newLocation.Street1 == "" ) && ( t.Street1 == null || t.Street1 == "" ) ) ) &&
                ( t.Street2 == newLocation.Street2 || ( ( newLocation.Street2 == null || newLocation.Street2 == "" ) && ( t.Street2 == null || t.Street2 == "" ) ) ) &&
                ( t.City == newLocation.City || ( ( newLocation.City == null || newLocation.City == "" ) && ( t.City == null || t.City == "" ) ) ) &&
                ( t.State == newLocation.State || ( ( newLocation.State == null || newLocation.State == "" ) && ( t.State == null || t.State == "" ) ) ) &&
                ( t.PostalCode == newLocation.PostalCode || ( ( newLocation.PostalCode == null || newLocation.PostalCode == "" ) && ( t.PostalCode == null || t.PostalCode == "" ) ) ) &&
                ( t.Country == newLocation.Country || ( ( newLocation.Country == null || newLocation.Country == "" ) && ( t.Country == null || t.Country == "" ) ) ) );

            if ( existingLocation != null )
            {
                return existingLocation;
            }

            // Create a new context/service so that save does not affect calling method's context
            var rockContext = new RockContext();
            var locationService = new LocationService( rockContext );
            locationService.Add( newLocation );
            rockContext.SaveChanges();

            // refetch it from the database to make sure we get a valid .Id
            return Get( newLocation.Guid );
        }

        /// <summary>
        /// Returns a <see cref="Rock.Model.Location"/> by GeoPoint. If a match is not found,
        /// a new Location will be added based on the Geopoint.
        /// </summary>
        /// <param name="point">A <see cref="System.Data.Entity.Spatial.DbGeography"/> object
        ///     representing the Geopoint for the location.</param>
        /// <returns>The first <see cref="Rock.Model.Location"/> that matches the specified GeoPoint.</returns>
        public Location GetByGeoPoint( DbGeography point )
        {
            // get the first address that has a GeoPoint the value
            // use the 'Where Max(ID)' trick instead of TOP 1 to optimize SQL performance
            var qryWhere = Queryable()
                .Where( a =>
                    a.GeoPoint != null &&
                    a.GeoPoint.SpatialEquals( point ) );

            var result = Queryable().Where( a => a.Id == qryWhere.Max( b => b.Id ) ).FirstOrDefault();

            if ( result == null )
            {
                // if the Location can't be found, save the new location to the database
                Location newLocation = new Location
                {
                    GeoPoint = point,
                    Guid = Guid.NewGuid()
                };

                // Create a new context/service so that save does not affect calling method's context
                var rockContext = new RockContext();
                var locationService = new LocationService( rockContext );
                locationService.Add( newLocation );
                rockContext.SaveChanges();

                // refetch it from the database to make sure we get a valid .Id
                return Get( newLocation.Guid );
            }

            return result;
        }

        /// <summary>
        /// Returns the first <see cref="Rock.Model.Location"/> with a GeoFence that matches
        /// the specified GeoFence.
        /// </summary>
        /// <param name="fence">A <see cref="System.Data.Entity.Spatial.DbGeography"/> object that
        ///  represents the GeoFence of the location to retrieve.</param>
        /// <returns>The <see cref="Rock.Model.Location"/> for the specified GeoFence. </returns>
        public Location GetByGeoFence( DbGeography fence )
        {
            // get the first address that has the GeoFence value
            // use the 'Where Max(ID)' trick instead of TOP 1 to optimize SQL performance
            var qryWhere = Queryable()
                .Where( a =>
                    a.GeoFence != null &&
                    a.GeoFence.SpatialEquals( fence ) );

            var result = Queryable().Where( a => a.Id == qryWhere.Max( b => b.Id ) ).FirstOrDefault();

            if ( result == null )
            {
                // if the Location can't be found, save the new location to the database
                Location newLocation = new Location
                {
                    GeoFence = fence,
                    Guid = Guid.NewGuid()
                };

                // Create a new context/service so that save does not affect calling method's context
                var rockContext = new RockContext();
                var locationService = new LocationService( rockContext );
                locationService.Add( newLocation );
                rockContext.SaveChanges();

                // refetch it from the database to make sure we get a valid .Id
                return Get( newLocation.Guid );
            }

            return result;
        }

        /// <summary>
        /// Performs Address Verification on the provided <see cref="Rock.Model.Location" />.
        /// </summary>
        /// <param name="location">A <see cref="Rock.Model.Location" /> to verify.</param>
        /// <param name="reVerify">if set to <c>true</c> [re verify].</param>
        public void Verify( Location location, bool reVerify )
        {
            string inputLocation = location.ToString();

            // Create new context to save service log without affecting calling method's context
            var rockContext = new RockContext();
            Model.ServiceLogService logService = new Model.ServiceLogService( rockContext );

            // Try each of the verification services that were found through MEF
            foreach ( var service in Rock.Address.VerificationContainer.Instance.Components )
            {
                if ( service.Value.Value.IsActive )
                {
                    string result;
                    bool success = service.Value.Value.VerifyLocation( location, reVerify, out result );
                    if ( !string.IsNullOrWhiteSpace( result ) )
                    {
                        // Log the results of the service
                        Model.ServiceLog log = new Model.ServiceLog();
                        log.LogDateTime = RockDateTime.Now;
                        log.Type = "Location Verify";
                        log.Name = service.Value.Metadata.ComponentName;
                        log.Input = inputLocation;
                        log.Result = result.Left( 200 );
                        log.Success = success;
                        logService.Add( log );
                    }

                    if ( success )
                    {
                        break;
                    }
                }
            }

            rockContext.SaveChanges();
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="Rock.Model.Location">Locations</see> that are descendants of a <see cref="Rock.Model.Location"/>
        /// </summary>
        /// <param name="parentLocationId">A <see cref="System.Int32"/> representing the Id of the <see cref="Rock.Model.Location"/></param>
        /// <returns>A collection of <see cref="Rock.Model.Location"/> entities that are descendants of the provided parent <see cref="Rock.Model.Location"/>.</returns>
        public IEnumerable<Location> GetAllDescendents( int parentLocationId )
        {
            return ExecuteQuery( string.Format(
                @"
                WITH CTE AS (
                    SELECT * FROM [Location] WHERE [ParentLocationId]={0}
                    UNION ALL
                    SELECT [a].* FROM [Location] [a]
                    INNER JOIN  CTE pcte ON pcte.Id = [a].[ParentLocationId]
                )
                SELECT * FROM CTE
                ", parentLocationId ) );
        }

        /// <summary>
        /// Gets all ancestors.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public IEnumerable<Location> GetAllAncestors( int locationId )
        {
            return ExecuteQuery( string.Format(
                @"
                WITH CTE AS (
                    SELECT * FROM [Location] WHERE [Id]={0}
                    UNION ALL
                    SELECT [a].* FROM [Location] [a]
                    INNER JOIN CTE ON CTE.[ParentLocationId] = [a].[Id]
                )
                SELECT * FROM CTE
                WHERE [Name] IS NOT NULL 
                AND [Name] <> ''
                ", locationId ) );
        }

        /// <summary>
        /// Gets the CampusID associated with the Location from the location or from the location's parent path
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public int? GetCampusIdForLocation( int? locationId)
        {
            if ( !locationId.HasValue )
            {
                return null;
            }

            // If location is not a campus, check the location's parent locations to see if any of them are a campus
            var location = this.Get( locationId.Value );
            int? campusId = location.CampusId;
            if ( !campusId.HasValue )
            {
                var campusLocations = new Dictionary<int, int>();
                Rock.Web.Cache.CampusCache.All()
                    .Where( c => c.LocationId.HasValue )
                    .Select( c => new
                    {
                        CampusId = c.Id,
                        LocationId = c.LocationId.Value
                    } )
                    .ToList()
                    .ForEach( c => campusLocations.Add( c.CampusId, c.LocationId ) );

                foreach ( var parentLocationId in this.GetAllAncestors( locationId.Value )
                    .Select( l => l.Id ) )
                {
                    campusId = campusLocations
                        .Where( c => c.Value == parentLocationId )
                        .Select( c => c.Key )
                        .FirstOrDefault();
                    if ( campusId != 0 )
                    {
                        break;
                    }
                }
            }

            return campusId;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public string GetPath( int locationId )
        {
            var locations = GetAllAncestors( locationId );
            if ( locations.Any() )
            {
                var locationNames = locations.Select( l => l.Name ).ToList();
                locationNames.Reverse();
                return locationNames.AsDelimited( " > " );
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the locations associated to a device and optionally any child locaitons
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="includeChildLocations">if set to <c>true</c> [include child locations].</param>
        /// <returns></returns>
        public IEnumerable<Location> GetByDevice( int deviceId, bool includeChildLocations = true )
        {
            string childQuery = includeChildLocations ? @"
                    UNION ALL
                    SELECT [a].*
                        FROM [Location] [a]
                            INNER JOIN  CTE pcte ON pcte.Id = [a].[ParentLocationId]
" : "";

            return ExecuteQuery( string.Format(
                @"
            WITH CTE AS (
                SELECT L.*
                    FROM [DeviceLocation] D
                        INNER JOIN [Location] L ON L.[Id] = D.[LocationId]
                WHERE D.[DeviceId] = {0}
                {1}
            )

            SELECT * FROM CTE
            ", deviceId, childQuery ) );
        }
    }
}
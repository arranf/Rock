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
using System.Linq;

using Quartz;

using Rock.Model;
using Rock.Web.Cache;
using Rock.Attribute;

namespace Rock.Jobs
{
    /// <summary>
    /// Job to keep a heartbeat of the job process so we know when the jobs stop working
    /// </summary>
    [IntegerField("Max Records Per Run", "The maximum number of records to run per run.", true, 1000 )]
    [IntegerField( "Throttle Period", "The number of milliseconds to wait between records. This helps to throttle requests to the lookup services.", true, 500 )]
    [IntegerField( "Retry Period", "The number of days to wait before retrying a unsuccessful address lookup.", true, 200 )]
    [DisallowConcurrentExecution]
    public class LocationServicesVerify : IJob
    {
        
        /// <summary> 
        /// Empty constructor for job initilization
        /// <para>
        /// Jobs require a public empty constructor so that the
        /// scheduler can instantiate the class whenever it needs.
        /// </para>
        /// </summary>
        public LocationServicesVerify()
        {
        }
        
        /// <summary> 
        /// Job that updates the JobPulse setting with the current date/time.
        /// This will allow us to notify an admin if the jobs stop running.
        /// 
        /// Called by the <see cref="IScheduler" /> when a
        /// <see cref="ITrigger" /> fires that is associated with
        /// the <see cref="IJob" />.
        /// </summary>
        public virtual void  Execute(IJobExecutionContext context)
        {
            // get the job map
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            int maxRecords = Int32.Parse( dataMap.GetString( "MaxRecordsPerRun" ) );
            int throttlePeriod = Int32.Parse( dataMap.GetString( "ThrottlePeriod" ) );
            int retryPeriod = Int32.Parse( dataMap.GetString( "RetryPeriod" ) );

            DateTime retryDate = DateTime.Now.Subtract(new TimeSpan(retryPeriod, 0, 0, 0));

            var rockContext = new Rock.Data.RockContext();
            LocationService locationService = new LocationService(rockContext);
            var addresses = locationService.Queryable()
                                .Where( l => (
                                    (l.IsGeoPointLocked == null || l.IsGeoPointLocked == false) // don't ever try locked address
                                    && (l.IsActive == true && l.Street1 != null && l.PostalCode != null) // or incomplete addresses
                                    && (
                                        (l.GeocodedDateTime == null && (l.GeocodeAttemptedDateTime == null || l.GeocodeAttemptedDateTime < retryDate)) // has not been attempted to be geocoded since retry date
                                        ||
                                        (l.StandardizedDateTime == null && (l.StandardizeAttemptedDateTime == null || l.StandardizeAttemptedDateTime < retryDate)) // has not been attempted to be standardize since retry date
                                    )
                                ))
                                .Take( maxRecords ).ToList();

            foreach ( var address in addresses )
            {
                locationService.Verify( address, false ); // currently not reverifying 
                rockContext.SaveChanges();
                System.Threading.Thread.Sleep( throttlePeriod );
            }

        }

    }
}
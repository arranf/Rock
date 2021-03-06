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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock.Store
{
    /// <summary>
    /// Base service class for store services
    /// </summary>
    public class StoreServiceBase
    {
        /// <summary>
        /// Internal variable to store the url to the store.
        /// </summary>
        protected string _rockStoreUrl = string.Empty;

        /// <summary>
        /// The Client Timeout
        /// </summary>
        protected int _clientTimeout = 12000;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreServiceBase"/> class.
        /// </summary>
        public StoreServiceBase()
        {
            // set configuration variables
            _rockStoreUrl = ConfigurationManager.AppSettings["RockStoreUrl"];
        }
    }
}

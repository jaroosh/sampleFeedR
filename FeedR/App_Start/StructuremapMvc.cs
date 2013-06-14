// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;
using System.Web.Mvc;
using FeedR.Commons.Feed;
using FeedR.Commons.Information;
using FeedR.Commons.Interfaces;
using FeedR.Commons.Messaging;
using FeedR.Commons.Model;
using FeedR.Data;
using FeedR.Factories;
using FeedR.Hubs;
using FeedR.Services.Repositories;
using StructureMap;
using FeedR.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(FeedR.App_Start.StructuremapMvc), "Start")]

namespace FeedR.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
			IContainer container = IoC.Initialize();

            // Setup our custom configuration here.
            container.Configure(ConfigureInjections);
            
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);

            // Setup a builder for controllers to inject the repos.
            ControllerBuilder.Current.SetControllerFactory(new SMControllerFactory());
            
        }

        /// <summary>
        /// Configures all injections.
        /// </summary>
        /// <param name="obj"></param>
        private static void ConfigureInjections(ConfigurationExpression configuration)
        {
            configuration.For<IRepository<UserDto>>().HttpContextScoped().Use<Repository<UserDto>>();
            configuration.For<IUserRepository>().HttpContextScoped().Use<UserRepository>(); // this is necessary if we would like to use EF for example and make sure no 2 threads will access one DbContext.

            // For demo purposes, Im using a single source throughout the whole app.
            configuration.For<IFeedSource>().Singleton().Use((() =>
                {
                    var source = new FeedSource<SimpleNewsGator>();
                    source.Subscribe(new FeedDispatcher(new FeedItemHub()));
                    source.Start();
                    return source;
                }));
        
        }
    }

    
}
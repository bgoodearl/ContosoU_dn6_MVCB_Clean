﻿using CU.Application;
using CU.Application.Common.Interfaces;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.Interfaces;
using CU.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace CU.ApplicationIntegrationTests
{
    public class TestFixture : TestBedFixture
    {
        public const string DbCollectionName = "DatabaseCollection";
        private static int fixtureInstanceCount = 0;
        internal static bool FixtureDbInitialized { get; private set; }

        public TestFixture()
        {
            ++fixtureInstanceCount;
        }

        #region Other Factories

        internal IServiceScopeFactory GetServiceScopeFactory(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper.Should().NotBeNull();

            IServiceScopeFactory? scopeFactory = GetService<IServiceScopeFactory>(testOutputHelper);
            scopeFactory.Should().NotBeNull();

            if (scopeFactory != null)
            {
                return scopeFactory;
            }
            throw new InvalidOperationException("GetServiceScopeFactory - invalid configuration");
        }

        #endregion Other Factories


        #region School Data Factories

        internal ISchoolRepositoryFactory GetSchoolRepositoryFactory(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper.Should().NotBeNull();

            ISchoolRepositoryFactory? schoolRepositoryFactory = GetService<ISchoolRepositoryFactory>(testOutputHelper);
            schoolRepositoryFactory.Should().NotBeNull();
            if (schoolRepositoryFactory != null)
            {
                return schoolRepositoryFactory;
            }

            throw new InvalidOperationException("GetSchoolRepositoryFactory - invalid configuration");
        }

        internal ISchoolViewDataRepositoryFactory GetSchoolViewDataRepositoryFactory(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper.Should().NotBeNull();

            ISchoolViewDataRepositoryFactory? schoolViewDataRepositoryFactory = GetService<ISchoolViewDataRepositoryFactory>(testOutputHelper);
            schoolViewDataRepositoryFactory.Should().NotBeNull();
            if (schoolViewDataRepositoryFactory != null)
            {
                return schoolViewDataRepositoryFactory;
            }

            throw new InvalidOperationException("GetSchoolViewDataRepositoryFactory - invalid configuration");
        }

        #endregion School Data Factories

        #region TestBed

        protected override IEnumerable<string> GetConfigurationFiles()
        {
            IEnumerable<string> configFiles = new List<string>(TestBase.ConfigurationFiles);
            return configFiles;
        }

        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            if (configuration != null)
            {
                services.AddApplicationLayer();
                services.AddInfrastructure(configuration);
            }
        }

        protected override ValueTask DisposeAsyncCore()
            => new();

        #endregion TestBed

    }
}

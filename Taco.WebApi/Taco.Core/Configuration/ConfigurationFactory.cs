using Microsoft.Extensions.Configuration;
using System;
using Taco.Core.Configuration.Models;

namespace Taco.Core
{
    public class ConfigurationFactory
    {

        private readonly IConfiguration _configuration;
        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="configuration">    The configuration. </param>
        public ConfigurationFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Setup(configuration);
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        ///
        /// <param name="configuration">    The configuration. </param>
        /// <param name="contentRootPath">  Full pathname of the content root file. </param>
        public ConfigurationFactory(IConfiguration configuration, string contentRootPath)
        {
            _configuration = configuration;
            ContentRootPath = contentRootPath;
            Setup(configuration);
        }

        /// <summary>
        ///     Gets the full pathname of the content root file.
        /// </summary>
        ///
        /// <value>
        ///     The full pathname of the content root file.
        /// </value>
        public string ContentRootPath { get; }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        ///
        /// <value>
        ///     The connection string.
        /// </value>
        public string ConnectionString { get; private set; }

        public string DataPath { get; set; }

        /// <summary>
        ///     Gets or sets the data base.
        /// </summary>
        ///
        /// <value>
        ///     The data base.
        /// </value>
        public DataBaseSettings DataBase { get; set; }

        /// <summary>
        ///     Gets or sets the Amazon S3 settings.
        /// </summary>
        ///
        /// <value>
        ///     The Amazon S3.
        /// </value>
        public AmazonS3Settings AmazonS3{ get; set; }

        /// <summary>
        ///     Gets or sets the CORS origins.
        /// </summary>
        ///
        /// <value>
        ///     The CORS origins.
        /// </value>
        public string[] CorsOrigins { get; set; }

        private void Setup(IConfiguration configuration)
        {
            DataBase = GetConfigurationSectionInstance<DataBaseSettings>(configuration, "DataBaseSettings");

            if (DataBase.DataSource == "JSON")
            {
                DataPath = _configuration.GetSection("DataPaths").GetValue<string>(DataBase.ConnectionStringName);
            }
            else if (DataBase.DataSource == "DB")
            {
                ConnectionString = _configuration.GetConnectionString(DataBase.ConnectionStringName);
            }

            AmazonS3 = GetConfigurationSectionInstance<AmazonS3Settings>(configuration, "AmazonS3Settings");
            CorsOrigins = configuration.GetSection("CorsOrigins").Get<string[]>();
        }

        private T GetConfigurationSectionInstance<T>(IConfiguration configuration, string section)
        {
            var t = Activator.CreateInstance<T>();
            configuration.GetSection(section).Bind(t);
            return t;
        }
    }
}

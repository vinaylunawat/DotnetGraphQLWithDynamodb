﻿namespace Framework.Configuration.Models
{
    using Framework.Configuration;
    using System;

    /// <summary>
    /// Defines the <see cref="ApplicationOptions" />.
    /// </summary>
    public class ApplicationOptions : ConfigurationOptions
    {
        /// <summary>
        /// Gets or sets the ConnectionString.
        /// </summary>
        //public string ConnectionString { get; set; }
        public string ConnectionString { get; set; }

        public string CognitoAuthorityURL { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether DeleteDefaultSchema.
        /// </summary>
        //public bool DeleteDefaultSchema { get; set; }

        /// <summary>
        /// Gets or sets the StorageConnectionString.
        /// </summary>
        //public string StorageConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the UserServiceBaseUri.
        /// </summary>
        //public Uri UserServiceBaseUri { get; set; }

        /// <summary>
        /// Gets or sets the amazonS3Congiguration.
        /// </summary>
        public AmazonSQSConfigurationOptions amazonSQSConfigurationOptions { get; set; }

        /// <summary>
        /// Gets or sets the amazonSQSCongiguration.
        /// </summary>
        public AmazonS3ConfigurationOptions amazons3ConfigurationOptions { get; set; }
    }
}

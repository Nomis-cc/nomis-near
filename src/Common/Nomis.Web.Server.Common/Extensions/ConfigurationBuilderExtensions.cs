// ------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationBuilderExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Serilog;

namespace Nomis.Web.Server.Common.Extensions
{
    /// <summary>
    /// <see cref="IConfigurationBuilder"/> extension methods.
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        private const string ConfigsDirectory = "Configs";

        /// <summary>
        /// Delegate to pass one of IConfigurationBuilder.AddJsonFile to <see cref="AddConfigFiles"/> method.
        /// </summary>
        private delegate IConfigurationBuilder AddConfigFileDelegate(string filename, bool optional, bool reloadOnChange);

        /// <summary>
        /// Adds all config files to <see cref="IConfigurationBuilder"/> as JSON config source.
        /// </summary>
        /// <param name="builder"><see cref="IConfigurationBuilder"/>.</param>
        /// <param name="relativePath">Directory to find. Root app directory if null or empty.</param>
        /// <param name="filenamePattern">Filename pattern to search.</param>
        /// <param name="includeOnlyForCurrentEnvironment">Include only for current environment.</param>
        public static IConfigurationBuilder AddJsonFiles(
            this IConfigurationBuilder builder,
            string relativePath = ConfigsDirectory,
            string filenamePattern = "*.json",
            bool includeOnlyForCurrentEnvironment = false)
        {
            var addJsonFileDelegate = new AddConfigFileDelegate(builder.AddJsonFile);
            AddConfigFiles(relativePath, filenamePattern, addJsonFileDelegate, includeOnlyForCurrentEnvironment);
            return builder;
        }

        /// <summary>
        /// Private method with shared logic of config adding.
        /// </summary>
        /// <param name="relativePath">Directory to find. Root app directory if null or empty.</param>
        /// <param name="filenamePattern">Filename pattern to search.</param>
        /// <param name="addConfigFile">Delegate to pass one of IConfigurationBuilder.AddJsonFile.</param>
        /// <param name="includeOnlyForCurrentEnvironment">Include only for current environment.</param>
        private static void AddConfigFiles(
            string relativePath,
            string filenamePattern,
            AddConfigFileDelegate addConfigFile,
            bool includeOnlyForCurrentEnvironment = false)
        {
            var appAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (appAssembly != null)
            {
                string? appRoot = Path.GetDirectoryName(appAssembly.Location);
                if (!string.IsNullOrWhiteSpace(appRoot))
                {
                    string configsFolder = Path.Combine(appRoot, relativePath);
                    foreach (string fileName in GetAvailableFiles(configsFolder, filenamePattern, includeSubdirectories: true, includeOnlyForCurrentEnvironment))
                    {
                        addConfigFile(filename: fileName, optional: true, reloadOnChange: true);
                    }
                }
            }
        }

        /// <summary>
        /// Lists all files in directory and subdirectories.
        /// </summary>
        /// <param name="directory">Directory path to search in.</param>
        /// <param name="searchPattern">File search pattern.</param>
        /// <param name="includeSubdirectories">Include subdirectories.</param>
        /// <param name="includeOnlyForCurrentEnvironment">Include only for current environment.</param>
        /// <returns>Returns a list of file paths.</returns>
        private static IEnumerable<string> GetAvailableFiles(
            string directory,
            string searchPattern,
            bool includeSubdirectories = true,
            bool includeOnlyForCurrentEnvironment = false)
        {
            var result = new List<string>();
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            try
            {
                if (includeSubdirectories)
                {
                    foreach (string subDir in Directory.GetDirectories(directory))
                    {
                        var subDirFiles = GetAvailableFiles(subDir, searchPattern, includeSubdirectories);
                        result.AddRange(subDirFiles);
                    }
                }

                string[] dirFiles = Directory.GetFiles(directory, searchPattern, SearchOption.TopDirectoryOnly);
                result.AddRange(includeOnlyForCurrentEnvironment
                    ? dirFiles.Where(x => x.Contains($".{env}."))
                    : dirFiles);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Logger.Warning(ex, "Unauthorized access.");
            }

            return result;
        }
    }
}
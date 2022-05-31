namespace BDP.Domain.Services;

public interface IConfigurationService
{
    /// <summary>
    /// Gets or sets a value in the configuration dictionary
    /// </summary>
    /// <param name="key">The key of the configuration entry</param>
    /// <returns></returns>
    string this[string key] { get; set; }

    /// <summary>
    /// Gets a string value from the configuration dictionray
    /// </summary>
    /// <param name="key">The key of the configuraiton entry to get</param>
    /// <returns>The configuration value</returns>
    string Get(string key);

    /// <summary>
    /// Gets a string value from the configuration dictionary, using a default
    /// value if the key does not exist
    /// </summary>
    /// <param name="key">The key of the configuration entry to get</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The configuration value</returns>
    string Get(string key, string defaultValue);

    /// <summary>
    /// Gets an integer value from the configuration dictionray
    /// </summary>
    /// <param name="key">The key of the configuraiton entry to get</param>
    /// <returns>The configuration value</returns>
    int GetInt(string key);

    /// <summary>
    /// Gets an integer value from the configuration dictionary, using a default
    /// value if the key does not exist
    /// </summary>
    /// <param name="key">The key of the configuration entry to get</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The configuration value</returns>
    int GetInt(string key, int defaultValue);

    /// <summary>
    /// Gets a double value from the configuration dictionray
    /// </summary>
    /// <param name="key">The key of the configuraiton entry to get</param>
    /// <returns>The configuration value</returns>
    double GetDouble(string key);

    /// <summary>
    /// Gets a double value from the configuration dictionary, using a default
    /// value if the key does not exist
    /// </summary>
    /// <param name="key">The key of the configuration entry to get</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The configuration value</returns>
    double GetDouble(string key, double defaultValue);

    /// <summary>
    /// Gets a boolean value from the configuration dictionray
    /// </summary>
    /// <param name="key">The key of the configuraiton entry to get</param>
    /// <returns>The configuration value</returns>
    bool GetBool(string key);

    /// <summary>
    /// Gets a boolean value from the configuration dictionary, using a default
    /// value if the key does not exist
    /// </summary>
    /// <param name="key">The key of the configuration entry to get</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The configuration value</returns>
    bool GetBool(string key, bool defaultValue);

    /// <summary>
    /// Sets or updates a configuration entry with the supplied key and value
    /// </summary>
    /// <param name="key">The key of the configuration entry</param>
    /// <param name="value">The value to set</param>
    void Set(string key, string value);

    /// <summary>
    /// Checks for the existence of a key in the configuration dictionary
    /// </summary>
    /// <param name="key">The key to check for</param>
    /// <returns>True if the key exists, false otherwise</returns>
    bool HasKey(string key);

    /// <summary>
    /// Deserializes a configuration group into an object
    /// </summary>
    /// <param name="name">The name of the configuration group</param>
    /// <param name="output">The output object</param>
    void Bind<T>(string name, T output);
}
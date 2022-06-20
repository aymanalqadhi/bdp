using BDP.Domain.Services;

namespace BDP.Web.Api.Services;

public class AppSettingsConfigurationService : IConfigurationService
{
    #region Private fields

    private readonly IConfiguration _config;

    #endregion Private fields

    #region Ctors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="config">The appconfig object</param>
    public AppSettingsConfigurationService(IConfiguration config)
    {
        _config = config;
    }

    #endregion Ctors

    #region Public properties

    /// <inheritdoc/>
    public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    #endregion Public properties

    #region Public methods

    /// <inheritdoc/>
    public string Get(string key)
        => _config[key];

    /// <inheritdoc/>
    public string Get(string key, string defaultValue)
        => _config.GetValue(key, defaultValue);

    /// <inheritdoc/>
    public int GetInt(string key)
        => _config.GetValue<int>(key);

    /// <inheritdoc/>
    public int GetInt(string key, int defaultValue)
        => _config.GetValue(key, defaultValue);

    /// <inheritdoc/>
    public double GetDouble(string key)
        => _config.GetValue<double>(key);

    /// <inheritdoc/>
    public double GetDouble(string key, double defaultValue)
        => _config.GetValue(key, defaultValue);

    /// <inheritdoc/>
    public bool GetBool(string key)
        => _config.GetValue<bool>(key);

    /// <inheritdoc/>
    public bool GetBool(string key, bool defaultValue)
        => _config.GetValue(key, defaultValue);

    /// <inheritdoc/>
    public void Set(string key, string value)
        => _config[key] = value;

    /// <inheritdoc/>
    public bool HasKey(string key)
        => _config.GetValue<object?>(key, null) == null;

    /// <inheritdoc/>
    public void Bind<T>(string name, T output)
        => _config.Bind(name, output);

    #endregion Public methods
}
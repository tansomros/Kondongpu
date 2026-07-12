using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Kondongpu.Domain.Common;

public abstract class SmartEnum<T> where T : SmartEnum<T>
{
    private static readonly Lazy<IReadOnlyList<T>> _all = new(() =>
        typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => (T)f.GetValue(null)!)
            .OrderBy(e => e.Sort)
            .ToList()
            .AsReadOnly());

    private static readonly Lazy<Dictionary<string, T>> _byValue = new(() =>
        All.ToDictionary(e => e.Value, StringComparer.OrdinalIgnoreCase));

    private static readonly Lazy<ResourceManager?> _resourceManager = new(() =>
    {
        var baseName = $"Kondongpu.Domain.Resources.{typeof(T).Name}";
        try
        {
            var rm = new ResourceManager(baseName, typeof(T).Assembly);
            rm.GetString("_probe_"); // force load to detect missing .resx
            return rm;
        }
        catch (MissingManifestResourceException)
        {
            return null;
        }
    });

    public string Value { get; }
    public string AbnormalFlag { get; }
    public string DisplayName { get; }
    public int Sort { get; }

    protected SmartEnum(string value,string abnormalFlag, string displayName, int sort)
    {
        Value = value;
        AbnormalFlag=abnormalFlag;
        DisplayName = displayName;
        Sort = sort;
    }

    public static IReadOnlyList<T> All => _all.Value;

    public static T FromValue(string value)
    {
        if (_byValue.Value.TryGetValue(value, out var result))
            return result;

        throw new InvalidOperationException($"'{value}' is not a valid value for {typeof(T).Name}.");
    }

    public static bool TryFromValue(string value, out T? result)
        => _byValue.Value.TryGetValue(value, out result);

    /// <summary>
    /// Returns the display name localized to the given culture code.
    /// Falls back to the default DisplayName when cultureCode is null or the resource is not found.
    /// </summary>
    public string GetDisplayName(string? cultureCode = null)
    {
        if (string.IsNullOrEmpty(cultureCode))
            return DisplayName;

        var rm = _resourceManager.Value;
        if (rm is null)
            return DisplayName;

        try
        {
            var culture = CultureInfo.GetCultureInfo(cultureCode);
            return rm.GetString(Value, culture) ?? DisplayName;
        }
        catch (CultureNotFoundException)
        {
            return DisplayName;
        }
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
        => obj is SmartEnum<T> other && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode()
        => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
}

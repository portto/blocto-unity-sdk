using System.Collections.Generic;
using Newtonsoft.Json;

// namespace Flow.Net.Sdk.Client.Unity.Models.Apis
namespace Blocto.Sdk.Core.Model
{
public partial class Error
{
    [JsonProperty("code", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int Code { get; set; }

    [JsonProperty("message", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    } 
}
}
using System.Text.Json.Serialization;

namespace pinguinera_final_module.Shared.Enums;

[JsonConverter( typeof( JsonStringEnumConverter ) )]
public enum QuoteType
{
    WHOLESALE,
    RETAIL
}
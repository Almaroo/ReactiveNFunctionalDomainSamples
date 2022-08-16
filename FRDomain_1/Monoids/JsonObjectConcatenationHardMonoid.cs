using System.Text.Json;
using LanguageExt.TypeClasses;
using System.Text.Json.Nodes;

namespace FRDomain_1.Monoids;

public static partial class JsonExtensions{
    public static TNode? CopyNode<TNode>(this TNode? node) where TNode : JsonNode => node?.Deserialize<TNode>();

    public static JsonObject AddOrReplace(this JsonObject @this, JsonObject other) =>
        JsonObjectConcatenationHardMonoid.Inst.Append(@this, other);

    public static JsonObject AddOrAppend(this JsonObject @this, JsonObject other) =>
        JsonObjectConcatenationSoftMonoid.Inst.Append(@this, other);
}

public class JsonObjectConcatenationSoftMonoid : Monoid<JsonObject>
{
    public static JsonObjectConcatenationSoftMonoid Inst => new();
    
    public JsonObject Append(JsonObject x, JsonObject y) =>
        y.Fold(x.CopyNode()!, (@object, kvp) =>
            @object
                .Find(pair => pair.Key == kvp.Key)
                .Map(pair =>
                {
                    var node = @object[pair.Key];

                    var _ = (node, kvp.Value) switch
                    {
                        (JsonValue, JsonValue other) => @object[pair.Key] = other.CopyNode(),
                        (JsonObject @this, JsonObject other) => @object[pair.Key] = Append(@this, other),
                        (JsonArray @this, JsonArray other) => @object[pair.Key] = new JsonArray(@this.Concat(@other).ToArray()),
                        _ => @object[pair.Key] = kvp.Value.CopyNode(),
                    };
                    
                    return @object;
                })
                .IfNone(() =>
                {
                    @object.Add(kvp.Key, kvp.Value.CopyNode());
                    return @object;
                })
        );

    public JsonObject Empty() => new();
}

public class JsonObjectConcatenationHardMonoid : Monoid<JsonObject>
{
    public static JsonObjectConcatenationHardMonoid Inst => new();
    
    public JsonObject Append(JsonObject x, JsonObject y) =>
        y.Fold(x.CopyNode()!, (@object, kvp) =>
            @object
                .Find(pair => pair.Key == kvp.Key)
                .Map(_ =>
                {
                    @object[kvp.Key] = kvp.Value.CopyNode();
                    return @object;
                })
                .IfNone(() =>
                {
                    @object.Add(kvp.Key, kvp.Value.CopyNode());
                    return @object;
                })
        );

    public JsonObject Empty() => new();
}

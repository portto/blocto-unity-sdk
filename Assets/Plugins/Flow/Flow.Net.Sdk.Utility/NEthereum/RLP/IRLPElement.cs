namespace Flow.Net.Sdk.Utility
{
    /// <summary>
    ///     Wrapper class for decoded elements from an RLP encoded byte array.
    /// </summary>
    public interface IRLPElement
    {
        byte[] RLPData { get; }
    }
}
namespace Sysmeta.JsonRpc
{
    /// <summary>
    /// Represents a single HTTP header item
    /// </summary>
    public class HttpHeader
    {
        /// <summary>
        /// The name of the HTTP header
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of the HTTP header
        /// </summary>
        public string Value { get; set; }
    }
}
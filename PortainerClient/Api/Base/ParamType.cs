namespace PortainerClient.Api.Base
{
    /// <summary>
    /// Request parameter types
    /// </summary>
    public enum ParamType
    {
        /// <summary>
        /// File parameter (send file to server)
        /// </summary>
        File,

        /// <summary>
        /// Query parameter (attach parameter to query string)
        /// </summary>
        QueryParam,

        /// <summary>
        /// Body parameter (attach parameter to body (form))
        /// </summary>
        BodyParam,

        /// <summary>
        /// JSON Body parameter (attach parameter as JSON Body of request)
        /// </summary>
        JsonBody
    }
}

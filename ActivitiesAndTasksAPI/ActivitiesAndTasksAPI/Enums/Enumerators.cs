namespace ActivitiesAndTasksAPI.Enums
{

    /// <summary>
    /// Enum representing standard HTTP response status codes with descriptions.
    /// </summary>
    public enum HttpResponseCode
    {
        // --- 1xx: Informational ---

        /// <summary>100 Continue - The server has received the request headers and the client should proceed to send the body.</summary>
        Continue = 100,

        /// <summary>101 Switching Protocols - The requester has asked the server to switch protocols.</summary>
        SwitchingProtocols = 101,

        /// <summary>102 Processing - The server is processing the request but no response is available yet.</summary>
        Processing = 102,



        // --- 2xx: Success ---

        /// <summary>200 OK - The request has succeeded.</summary>
        OK = 200,

        /// <summary>201 Created - The request has been fulfilled and resulted in a new resource being created.</summary>
        Created = 201,

        /// <summary>202 Accepted - The request has been accepted for processing, but the processing has not been completed.</summary>
        Accepted = 202,

        /// <summary>203 Non-Authoritative Information - The server successfully processed the request, but is returning information from another source.</summary>
        NonAuthoritativeInformation = 203,

        /// <summary>204 No Content - The server successfully processed the request and is not returning any content.</summary>
        NoContent = 204,

        /// <summary>205 Reset Content - The server successfully processed the request, asks client to reset the view.</summary>
        ResetContent = 205,

        /// <summary>206 Partial Content - The server is delivering only part of the resource due to a range header sent by the client.</summary>
        PartialContent = 206,



        // --- 3xx: Redirection ---

        /// <summary>300 Multiple Choices - The request has more than one possible response.</summary>
        MultipleChoices = 300,

        /// <summary>301 Moved Permanently - The requested resource has been permanently moved to a new URL.</summary>
        MovedPermanently = 301,

        /// <summary>302 Found - The requested resource has been temporarily moved to a different URL.</summary>
        Found = 302,

        /// <summary>303 See Other - The response to the request can be found under another URI using GET.</summary>
        SeeOther = 303,

        /// <summary>304 Not Modified - The resource has not been modified since the last request.</summary>
        NotModified = 304,

        /// <summary>307 Temporary Redirect - The resource is temporarily located at a different URI.</summary>
        TemporaryRedirect = 307,

        /// <summary>308 Permanent Redirect - The resource is permanently located at a different URI.</summary>
        PermanentRedirect = 308,



        // --- 4xx: Client Errors ---

        /// <summary>400 Bad Request - The request could not be understood or was missing required parameters.</summary>
        BadRequest = 400,

        /// <summary>401 Unauthorized - Authentication failed or user does not have permissions for the desired action.</summary>
        Unauthorized = 401,

        /// <summary>402 Payment Required - Reserved for future use (used for digital payment systems).</summary>
        PaymentRequired = 402,

        /// <summary>403 Forbidden - Authentication succeeded but authenticated user does not have access to the resource.</summary>
        Forbidden = 403,

        /// <summary>404 Not Found - The requested resource could not be found.</summary>
        NotFound = 404,

        /// <summary>405 Method Not Allowed - The HTTP method is not supported for the requested resource.</summary>
        MethodNotAllowed = 405,

        /// <summary>406 Not Acceptable - The requested resource is not available in a format acceptable to the client.</summary>
        NotAcceptable = 406,

        /// <summary>407 Proxy Authentication Required - Authentication with the proxy is required.</summary>
        ProxyAuthenticationRequired = 407,

        /// <summary>408 Request Timeout - The server timed out waiting for the request.</summary>
        RequestTimeout = 408,

        /// <summary>409 Conflict - The request could not be completed due to a conflict with the current state of the resource.</summary>
        Conflict = 409,

        /// <summary>410 Gone - The requested resource is no longer available and will not be available again.</summary>
        Gone = 410,

        /// <summary>411 Length Required - The request did not specify the length of its content.</summary>
        LengthRequired = 411,

        /// <summary>412 Precondition Failed - The server does not meet one of the preconditions that the requester put on the request.</summary>
        PreconditionFailed = 412,

        /// <summary>413 Payload Too Large - The request is larger than the server is willing or able to process.</summary>
        PayloadTooLarge = 413,

        /// <summary>414 URI Too Long - The URI provided was too long for the server to process.</summary>
        URITooLong = 414,

        /// <summary>415 Unsupported Media Type - The request entity has a media type which the server does not support.</summary>
        UnsupportedMediaType = 415,

        /// <summary>416 Range Not Satisfiable - The client has asked for a portion of the file, but the server cannot supply that portion.</summary>
        RangeNotSatisfiable = 416,

        /// <summary>417 Expectation Failed - The server cannot meet the requirements of the Expect request-header field.</summary>
        ExpectationFailed = 417,

        /// <summary>422 Unprocessable Entity - The request was well-formed but could not be followed due to semantic errors.</summary>
        UnprocessableEntity = 422,

        /// <summary>429 Too Many Requests - The user has sent too many requests in a given amount of time.</summary>
        TooManyRequests = 429,



        // --- 5xx: Server Errors ---

        /// <summary>500 Internal Server Error - The server encountered an unexpected condition which prevented it from fulfilling the request.</summary>
        InternalServerError = 500,

        /// <summary>501 Not Implemented - The server does not support the functionality required to fulfill the request.</summary>
        NotImplemented = 501,

        /// <summary>502 Bad Gateway - The server received an invalid response from the upstream server.</summary>
        BadGateway = 502,

        /// <summary>503 Service Unavailable - The server is currently unable to handle the request due to overload or maintenance.</summary>
        ServiceUnavailable = 503,

        /// <summary>504 Gateway Timeout - The upstream server failed to send a request in the time allowed by the server.</summary>
        GatewayTimeout = 504,

        /// <summary>505 HTTP Version Not Supported - The server does not support the HTTP protocol version used in the request.</summary>
        HTTPVersionNotSupported = 505
    }

}

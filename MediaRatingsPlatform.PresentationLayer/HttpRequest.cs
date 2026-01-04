namespace MediaRatingsPlatform.PresentationLayer;

public class HttpRequest {
    public HttpMethod Method { get; set; } = HttpMethod.Get;
    public List<string?> Path { get; set; } = [];
    public Dictionary<string, string> QueryParams { get; set; } = [];
    public Dictionary<string, string> Headers { get; set; } = [];
    public string? Body { get; set; }
    public string PathId { get; set; } = "";

    public string GetToken()
    {
        string token = "";
        string value;
        if (this.Headers.TryGetValue("Authorization", out value)) {
            if (value.Length > 7) {
                if (value.Substring(0, 7) == "Bearer ") {
                    token = value.Substring(7);
                }
            }
        }
        return token;
    }
    
    public string GetComparator()
    {
        return PathId;
    }
}

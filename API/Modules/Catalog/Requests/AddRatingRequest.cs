namespace API.Modules.Catalog.Requests;

public sealed record AddRatingRequest(
    double Rate,
    string Feedback = "");

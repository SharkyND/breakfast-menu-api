namespace BuberBreakfast.Contracts.Breakfast;

public record UpsertBreakfastResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    DateTime LastModifiedDateTime,
    string Savory,
    string Sweet
);
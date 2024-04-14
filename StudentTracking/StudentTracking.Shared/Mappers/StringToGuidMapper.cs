namespace StudentTracking.Shared.Mappers;

public static class StringToGuidMapper
{
    public static Guid MapTo(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId))
        {
            throw new Exception( $"Invalid id [{guidId}]");
        }
        return guidId;
    }
}
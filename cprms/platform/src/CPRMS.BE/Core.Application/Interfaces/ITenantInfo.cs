namespace Core.Application.Interfaces
{
    public interface ITenantInfo
    {
        string Id { get; set; }
        string Name { get; set; }
        string SchemaName { get; set; } 
    }
}

namespace JiraToolkit.Dtos
{
    internal class Configuration
    {
        public Environment[] Environments { get; set; }
        public Query[] Queries { get; set; }
    }
}
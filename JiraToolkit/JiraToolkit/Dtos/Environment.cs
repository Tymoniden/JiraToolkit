namespace JiraToolkit.Dtos
{
    internal sealed class Environment
    {
        public string Name { get; set; }
        public string RootUrl { get; set; }

        public string[] Prefixes { get; set; }
    }
}

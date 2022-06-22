namespace Freedom.Factory.Generic
{
    public class PathFactory : IPathFactory
    {
        private readonly string _basePath;

        public PathFactory(string basePath)
        {
            _basePath = basePath;
        }

        public string GetBasePath()
        {
            return _basePath;
        }
    }
}
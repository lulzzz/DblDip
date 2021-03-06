using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class PhotoStudioBuilder
    {
        private PhotoStudio _photoStudio;

        public static PhotoStudio WithDefaults()
        {
            return new PhotoStudio();
        }

        public PhotoStudioBuilder()
        {
            _photoStudio = WithDefaults();
        }

        public PhotoStudio Build()
        {
            return _photoStudio;
        }
    }
}

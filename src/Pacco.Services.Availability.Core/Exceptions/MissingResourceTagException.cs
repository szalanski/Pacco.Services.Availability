namespace Pacco.Services.Availability.Core.Exceptions
{
    public class MissingResourceTagException : DomainException
    {
        public override string Code => "missing_resource_tags";
        public MissingResourceTagException() : base("Resource tags are missing.")
        {
        }
    }
}

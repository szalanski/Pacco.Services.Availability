namespace Pacco.Services.Availability.Core.Exceptions
{
    public class InvalidResourceTagException : DomainException
    {
        public override string Code => "invalid_resource_tags";
        public InvalidResourceTagException() : base("Resource tags are invalid.")
        {
        }
    }
}

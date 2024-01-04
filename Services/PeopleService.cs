namespace MyApp.Namespace
{
    public class PeopleService : IPeopleService
    {
        public bool Validate(Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                return false;
            }
            return true;
        }
    }
}
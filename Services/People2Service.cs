namespace MyApp.Namespace
{
    public class People2Service : IPeopleService
    {
        public bool Validate(Person person)
        {
            if (string.IsNullOrEmpty(person.Name) || person.Name.Length < 3 || person.Name.Length > 100)
            {
                return false;
            }
            return true;
        }
    }
}
using jcardenasS5A.Utils;
namespace jcardenasS5A
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo { get; set; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();

            MainPage = new Views.Principal();
            PersonRepo = personRepository;

        }
    }
}

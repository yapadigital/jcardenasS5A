namespace jcardenasS5A.Views;
using jcardenasS5A.Models;

public partial class Principal : ContentPage
{
    public Persona selectedPersona;
    public Principal()
    {
        InitializeComponent();
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.PersonRepo.AddNewPerson(txtNombre.Text);
        lblStatus.Text = App.PersonRepo.StatusMessage;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App.PersonRepo.GetAllPeople();
        Listapersonas.ItemsSource = people;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Persona persona)
        {
            selectedPersona = persona;
        }
    }

    private async void btnActualiza_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var persona = button?.BindingContext as Persona;

        if (persona == null) return;

        string result = await DisplayPromptAsync("Actualizar",
            "Ingrese el nuevo nombre:",
            initialValue: persona.Name);

        if (!string.IsNullOrEmpty(result))
        {
            persona.Name = result;
            App.PersonRepo.UpdatePerson(persona);
            List<Persona> people = App.PersonRepo.GetAllPeople();
            Listapersonas.ItemsSource = people;
        }
    }

    private async void btnBorra_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var persona = button?.BindingContext as Persona;

        if (persona == null) return;

        bool answer = await DisplayAlert("Confirmar",
            $"¿Está seguro de eliminar a {persona.Name}?",
            "Sí", "No");

        if (answer)
        {
            App.PersonRepo.DeletePerson(persona);
            List<Persona> people = App.PersonRepo.GetAllPeople();
            Listapersonas.ItemsSource = people;
        }
    }

   
}
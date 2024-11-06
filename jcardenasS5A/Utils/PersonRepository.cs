using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcardenasS5A.Models;
using SQLite;

namespace jcardenasS5A.Utils
{
    public class PersonRepository
    {

        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }



        public PersonRepository(string path)
        {
            dbPath = path;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre es un campo requerido");

                Persona person = new() { Name = nombre };

                result = conn.Insert(person);

                StatusMessage = string.Format("Dato insertado");
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error" + ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error" + ex.Message);
            }
            return new List<Persona>();
        }

        // Proceso Actualizar 
        public void UpdatePerson(Persona persona)
        {
            try
            {
                Init();
                if (persona.Id <= 0)
                    throw new Exception("ID de persona inválido");

                int result = conn.Update(persona);
                if (result > 0)
                    StatusMessage = "Persona actualizada correctamente";
                else
                    StatusMessage = "No se pudo actualizar la persona";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar: {ex.Message}";
            }
        }

        // Proceso Borrar Persona
        public void DeletePerson(Persona persona)
        {
            try
            {
                Init();
                if (persona == null || persona.Id <= 0)
                    throw new Exception("Persona no válida para eliminar");

                int result = conn.Delete(persona);
                if (result > 0)
                    StatusMessage = "Persona eliminada correctamente";
                else
                    StatusMessage = "No se pudo eliminar la persona";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar: {ex.Message}";
            }
        }
    }


}





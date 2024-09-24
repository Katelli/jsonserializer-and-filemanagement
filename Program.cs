namespace jsonserializer_and_filemanagement;
using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string path = "test.txt";
            if(!File.Exists(path))
            {
                using(StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("I am testing a different method");
                    sw.WriteLine("Testing, testing");
                }
            }
            string text = File.ReadAllText(path);
            if(!string.IsNullOrEmpty(text))
            {
                Console.WriteLine(text);
            }
        }
        catch(IOException exception)
        {
            Console.WriteLine($"An error occured while attempting to write to the file test.txt {exception.Message}");
        }
        catch(Exception exception)
        {
            Console.WriteLine($"{exception.Message}\n");
        }

        Console.WriteLine("\n");

        try 
        {

            string filePath = "pet.json";

            List<Pet>? pets = new List<Pet>();
            if(!File.Exists(filePath))
            {
                File.CreateText(filePath);
            }            
            else
            {
                string existingJSON = File.ReadAllText(filePath);
                Console.WriteLine($"Data already exists within the file pet.json {File.ReadAllText(filePath)}");
                if(!string.IsNullOrWhiteSpace(existingJSON))
                {
                    pets = JsonSerializer.Deserialize<List<Pet>>(existingJSON);
                }
            }

            Console.WriteLine("What is your name?");
            string? name = Console.ReadLine();
            Console.WriteLine("What is your pet's name?");
            string? petName = Console.ReadLine();
            Console.WriteLine("What kind of animal is it?");
            string? animal = Console.ReadLine();
            Console.WriteLine("How old is it?");
            string? ageInput = Console.ReadLine();
            int age;
            while(!int.TryParse(ageInput, out age))
            {
                Console.WriteLine("There was an error with the data submitted, please input their age using numbers");
                ageInput = Console.ReadLine();
            }

            var newPet = new Pet
            {
                Owner = name,
                PetName = petName,
                Animal = animal,
                PetAge = age
            };
            pets?.Add(newPet);
            Console.WriteLine($"Your name is {newPet.Owner}. Your have a {newPet.Animal} named {newPet.PetName}, and they're {newPet.PetAge} years old.");

            string json = JsonSerializer.Serialize(pets, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(filePath, json);

            Console.WriteLine("Data was successfully written to the JSON object!");
        }
        catch(IOException exception)
        {
            Console.WriteLine($"An error occured while attempting to write to the file pet.json {exception.Message}");
        }
        catch(Exception exception)
        {
            Console.WriteLine($"{exception.Message}\n");
        }

    }
}

/*
Oppgave 3 (Valgfri, vanskelig)
Bruk enten C# HTTP klassen, eller CURL (CLI) via Git BASH terminal til å hente ut data fra en RestAPI på nett
Skriv ut dataen som hentes via C# HTTP eller CURL til terminalen eller til en tekstfil via File klassen
Nedenfor finner dere en liste over forskjellige APIer som kan brukes til å hente JSON data fra nettet
*/
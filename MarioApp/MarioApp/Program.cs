using CsvHelper;
using System.Globalization;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Display Characters");
        Console.WriteLine("2. Add Character");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            
            DisplayCharacters();
        }
        else if (choice == "2")
        {
          
            AddCharacter();
        }
        else
        {
            Console.WriteLine("Invalid option. Please choose 1 or 2.");
        }
    }

    static void DisplayCharacters()
    {
        try
        {
            using (var reader = new StreamReader("mario.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var characters = csv.GetRecords<Character>().ToList();
                foreach (var character in characters)
                {
                    Console.WriteLine($"{character.Name} - {character.Type} - {character.DebutYear} - {character.Abilities}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }


    static void AddCharacter()
    {
        try
        {
            Console.WriteLine("Enter character details:");

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Type: ");
            var type = Console.ReadLine();

            Console.Write("Debut Year: ");
            var debutYear = int.Parse(Console.ReadLine());

            Console.Write("Abilities: ");
            var abilities = Console.ReadLine();

            var newCharacter = new Character(name, type, debutYear, abilities);

            
            using (var writer = new StreamWriter("mario.csv", append: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(newCharacter);
                csv.NextRecord();
            }

            Console.WriteLine("Character added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding character: {ex.Message}");
        }
    }
}

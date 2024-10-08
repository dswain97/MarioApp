﻿using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";


var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

Console.WriteLine("Hello World!");


string file = "mario.csv";

if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
       
    List<UInt64> Ids = [];
    List<string> Names = [];
    List<string> Descriptions = [];

     
     List<Character> characters = [];
    try
    {
       StreamReader sr = new(file);
        
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            Console.WriteLine(line);
            if (line is not null)
            {
                Character character = new();
                
                string[] characterDetails = line.Split(',');
               
                Ids.Add(UInt64.Parse(characterDetails[0]));
                character.Id = UInt64.Parse(characterDetails[0]);
                Names.Add(characterDetails[1]);
                character.Name = characterDetails[1] ?? string.Empty;
             
                Descriptions.Add(characterDetails[2]);
                character.Description = characterDetails[2] ?? string.Empty;
                characters.Add(character);
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }
    

    string? choice;
    do
    {
        
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

     
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            Character character = new();
                 Console.WriteLine("Enter new character name: ");
            
                character.Name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(character.Name)){ 
               
                 
                 List<string> LowerCaseNames = characters.ConvertAll(character => character.Name.ToLower());
                if (LowerCaseNames.Contains(character.Name.ToLower()))
                {
                    
                    logger.Info($"Duplicate name {character.Name}");
                }
                else
                {
                   
                     character.Id = characters.Max(character => character.Id) + 1;
                
                     
                    Console.WriteLine("Enter description:");
                    
                   character.Description = Console.ReadLine() ?? string.Empty;
                  
                    StreamWriter sw = new(file, true);
                    
                    sw.WriteLine($"{character.Id},{character.Name},{character.Description}");
                    sw.Close();
                   
                    characters.Add(character);
                    
                    logger.Info($"Character id {character.Id} added");
                }
            } else {
                logger.Error("You must enter a name");
            }
        }
        else if (choice == "2")
        {
                  
                  
            for (int i = 0; i < Ids.Count; i++)
            {
                
                Console.WriteLine($"Id: {Ids[i]}");
                Console.WriteLine($"Name: {Names[i]}");
                Console.WriteLine($"Description: {Descriptions[i]}");
                Console.WriteLine();
            }
            
        }
    } while (choice == "1" || choice == "2");
    
    
}

logger.Info("Program ended");

   

﻿using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

Console.WriteLine("Hello World!");


string file = "mario.csv";
// make sure movie file exists
if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
        // create parallel lists of character details
    // lists are used since we do not know number of lines of data
    List<UInt64> Ids = [];
    List<string> Names = [];
    List<string> Descriptions = [];

     // to populate the lists with data, read from the data file
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
                // character details are separated with comma(,)
                string[] characterDetails = line.Split(',');
                // 1st array element contains id
                Ids.Add(UInt64.Parse(characterDetails[0]));
                character.Id = UInt64.Parse(characterDetails[0]);
                Names.Add(characterDetails[1]);
                character.Name = characterDetails[1] ?? string.Empty;
                // 3rd array element contains character description
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
    /*

    string? choice;
    do
    {
        // display choices to user
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

        // input selection
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            Character character = new();
                 Console.WriteLine("Enter new character name: ");
            string? Name = Console.ReadLine();
            if (!string.IsNullOrEmpty(Name)){
                character.Name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(character.Name)){
                UInt64 Id = Ids.Max() + 1;
                Console.WriteLine($"{Id}, {Name}");
                  // check for duplicate name
                List<string> LowerCaseNames = Names.ConvertAll(n => n.ToLower());
                if (LowerCaseNames.Contains(Name.ToLower()))
                 List<string> LowerCaseNames = characters.ConvertAll(character => character.Name.ToLower());
                if (LowerCaseNames.Contains(character.Name.ToLower()))
                {
                    logger.Info($"Duplicate name {Name}");
                    logger.Info($"Duplicate name {character.Name}");
                }
                else
                {
                    // generate id - use max value in Ids + 1
                    UInt64 id = Ids.Max() + 1;
                     character.Id = characters.Max(character => character.Id) + 1;
                    Console.WriteLine($"{Id}, {Name}");
                     // input character description
                    Console.WriteLine("Enter description:");
                    string? Description = Console.ReadLine();
                    Console.WriteLine($"{Id}, {Name}, {Description}");
                   // Console.WriteLine($"{Id}, {Name}, {Description}");
                    // create file from data
                    StreamWriter sw = new(file, true);
                    sw.WriteLine($"{Id},{Name},{Description}");
                    sw.WriteLine($"{character.Id},{character.Name},{character.Description}");
                    sw.Close();
                    // add new character details to Lists
                    Ids.Add(Id);
                    Names.Add(Name);
                    Descriptions.Add(Description);
                    characters.Add(character);
                    // log transaction
                    logger.Info($"Character id {Id} added");  
                }
            } else {
                logger.Error("You must enter a name");
            }
        }
        else if (choice == "2")
        {
                  // loop thru Lists
                  /*
            for (int i = 0; i < Ids.Count; i++)
            {
                // display character details
                Console.WriteLine($"Id: {Ids[i]}");
                Console.WriteLine($"Name: {Names[i]}");
                Console.WriteLine($"Description: {Descriptions[i]}");
                Console.WriteLine();
            }
            */
        }
    } while (choice == "1" || choice == "2");
    */
}

   
logger.Info("Program ended");
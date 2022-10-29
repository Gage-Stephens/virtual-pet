
using System.Runtime.InteropServices;
using template_csharp_virtual_pet;

Console.BackgroundColor = ConsoleColor.Cyan;
Console.ForegroundColor = ConsoleColor.Black;

Pet activePet;
Pet thisPet = new Pet();
Pet pet1 = new Pet("Fido", "dog", 60, 60, 60);
Pet pet2 = new Pet("Fluffy", "cat", 60, 60, 60);
Pet pet3= new Pet("Lucky", "gold fish", 60, 60, 60);
Pet pet4= new Pet("Polly", "parrot", 60, 60, 60);
Pet pet5= new Pet("Ray", "Gecko", 60, 60, 60);

List<Pet> petShelter = new List<Pet>() { pet1, pet2, pet3, pet4, pet5};

bool continueRunning = true;
while (continueRunning)
{
    Console.Clear();
    Console.WriteLine("Welcome to Virtual Pet!\n");
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("1. Create your pet.");
    Console.WriteLine("2. See your pet's status.");
    Console.WriteLine("3. Feed your pet.");
    Console.WriteLine("4. Play with your pet.");
    Console.WriteLine("5. Take your pet to the doctor.");
    Console.WriteLine("6. Admit a pet to the shelter or adopt a pet out.");
    Console.WriteLine("7. List pets in the shelter.");
    Console.WriteLine("8. Interact with all pets in the shelter.");
    Console.WriteLine("9. Interact with a particular shelter pet.");

    Console.WriteLine("Press Q to quit");

    var playerChoice = Console.ReadLine().ToLower();

    switch (playerChoice)
    {
        case "1":
            // Create a pet
            Console.WriteLine("What's the name of your pet?");
            var petName = Console.ReadLine();
            Console.WriteLine("What species is your pet?");
            var petSpecies = Console.ReadLine();
            thisPet.Name = petName;
            thisPet.Species = petSpecies;
            break;
        case "2":
            // Display pet info
            Console.WriteLine($"\n Name {thisPet.Name} Species {thisPet.Species} Health {thisPet.Health} Hunger {thisPet.Hunger} Boredom {thisPet.Boredom}");
            Console.Write("\nPress enter to return to the Main Menu");
            Console.ReadLine();
            break;
        case "3":
            //Feed your pet
            thisPet.Feed();
            Console.WriteLine($"\n Feeding your pet has changed it's status: Health {thisPet.Health} Hunger {thisPet.Hunger} Boredom {thisPet.Boredom}");
            Console.ReadLine();
            break;
        case "4":
            // Play with your pet
            thisPet.Play();
            Console.WriteLine($"\n Playing with your pet has changed it's status: Health {thisPet.Health} Hunger {thisPet.Hunger} Boredom {thisPet.Boredom}");
            Console.ReadLine();
            break;
        case "5":
            // Take pet to the doctor
            thisPet.SeeDoctor();
            Console.WriteLine($"\n Taking your pet to the doctor has changed it's status: Health {thisPet.Health} Hunger {thisPet.Hunger} Boredom {thisPet.Boredom}");
            Console.ReadLine();
            break;
        case "6":
            // Add a pet to the shelter or adopt a pet out
            Console.WriteLine("What do you want to do? \n A. Admit another pet to the shelter. \n B. Adopt a pet out of the shelter. \n");
            var response = Console.ReadLine().ToLower();
            if (response == "a")
            {
                var newShelterPet = new Pet();
                Console.WriteLine("What is the pet's name?");
                var name = Console.ReadLine();
                Console.WriteLine("What is the pet's species?");
                var species = Console.ReadLine();
                var hunger = 60;
                var health = 60;
                var boredom = 60;
                newShelterPet = new Pet(name, species, hunger, boredom, health);
                
                petShelter.Add(newShelterPet);
                Console.WriteLine($"\n{name} the {species} has been added");
            }
            else if (response == "b")
            {
                Console.WriteLine("Name of the pet you want to adopt out?");
                var adoptName = Console.ReadLine();
                for (int j = 0; j < petShelter.Count; j++)
                {
                    if(petShelter[j].Name == adoptName)
                    {
                        petShelter.RemoveAt(j);
                        Console.WriteLine("That pet has been adopted out!");
                    }
                }
            }

            Console.Write("\nPress enter to return to the Main Menu");
            Console.ReadLine();
            break;
        case "7":
            // List pets in the shelter
            Console.WriteLine("\nHere's a list of the pets in the shelter:\n");
            for (int i = 0; i < petShelter.Count; i++)
            {
                 Console.WriteLine($"{petShelter[i].Name,8} the {petShelter[i].Species,8} --  Health = {petShelter[i].Health}, Hunger = {petShelter[i].Hunger}, Boredom = {petShelter[i].Boredom}");
            }

            Console.Write("\nPress enter to return to the Main Menu");
            Console.ReadLine();
            break;
        case "8":
            // Interact with all pets
            Console.WriteLine("How would you like to interact with all pet in the shelter?\nF. Feed\nP. Play\nD. See Doctor");
            var interactOptionAll = Console.ReadLine().ToLower();
            for (int l = 0; l < petShelter.Count; l++)
            {
                if (interactOptionAll == "f")
                {
                    petShelter[l].Hunger = FeedPet(petShelter[l].Hunger);
                 }
                else if (interactOptionAll == "p")
                {
                    var playResult = PlayWithPet(petShelter[l].Hunger, petShelter[l].Boredom, petShelter[l].Health);
                    petShelter[l].Hunger = playResult.Item1;
                    petShelter[l].Boredom = playResult.Item2;
                    petShelter[l].Health = playResult.Item3;
                  }
                else if (interactOptionAll == "d")
                {
                    petShelter[l].Health = PetToDoctor(petShelter[l].Health);
                }
                Console.WriteLine($"{petShelter[l].Name,8} the {petShelter[l].Species,8} --  Health = {petShelter[l].Health}, Hunger = {petShelter[l].Hunger}, Boredom = {petShelter[l].Boredom}");
            }
            var action = "";
            if(interactOptionAll == "f")
            {
                action = "fed";
            }
            else if(interactOptionAll == "p") 
            {
                action = "played with";
            }
            else if(interactOptionAll=="d")
            {
                action = "took to the doctor";
            }

               
            Console.WriteLine("\nYou " + action + " all the pets.");
            Console.Write("\nPress enter to return to the Main Menu");
            Console.ReadLine();
            break;
        case "9":
            // Interact with a particular pet
            Console.WriteLine("Name of the pet you want to interact with?");
            var interactName = Console.ReadLine();
            for (int k = 0; k < petShelter.Count; k++)
            {
                if (petShelter[k].Name == interactName)
                {
                    Console.WriteLine($"{petShelter[k].Name} is in the Shelter.\n");
                    Console.WriteLine("How would you like to interact with this pet?\nF. Feed\nP. Play\nD. See Doctor");
                    var interactOption = Console.ReadLine().ToLower();
                    if (interactOption == "f")
                    {
                        petShelter[k].Hunger = FeedPet(petShelter[k].Hunger);
                        Console.WriteLine($"You fed {petShelter[k].Name}. Well done!");
                    }
                    else if (interactOption == "p") 
                    {
                        var playResult = PlayWithPet(petShelter[k].Hunger, petShelter[k].Boredom, petShelter[k].Health);
                        petShelter[k].Hunger = playResult.Item1;
                        petShelter[k].Boredom = playResult.Item2;   
                        petShelter[k].Health = playResult.Item3;
                        Console.WriteLine($"You played with {petShelter[k].Name}.");
                    }
                    else if (interactOption == "d")
                    {
                        petShelter[k].Health = PetToDoctor(petShelter[k].Health);
                        Console.WriteLine($"{petShelter[k].Name} is extra healthy after seeing the doctor!");
                    }
                }
            }

            Console.Write("\nPress enter to return to the Main Menu");
            Console.ReadLine();
            break;
        case "q":
            // Quit the app
            continueRunning = false;
            Console.WriteLine("Thanks for playing!");
            Console.Beep();
            break;
        default:
            break;

    }

    thisPet.Tick();

    int FeedPet(int hunger)
    {
        hunger -= 10;
        return hunger;
    }

    static (int, int, int) PlayWithPet(int hunger, int boredom, int health)
    {
        hunger += 10;
        boredom -= 20;
        health += 10;
        return (hunger, boredom, health);
    }

    int PetToDoctor(int health)
    {
        health += 30;
        return health;
    }


}
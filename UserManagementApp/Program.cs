using UserManagementApp.Models;
using UserManagementApp.Repositories;

UserRepository userRepository = new UserRepository();
bool result = await userRepository.LoadUsers();
if (!result)
{
    Console.WriteLine("Something went wrong!");
}
else
{
    Console.WriteLine("Welcome!");
    while (true)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("Please select your operation.");
        Console.WriteLine("1.Get all users\n2.Add user\n3.Update user\n4.Delete user\n5.Quit");
        Console.Write("Operation: ");
        string? operation = Console.ReadLine();
        bool flag = false;
        Console.WriteLine("--------------------------------------------------");
        switch (operation)
        {
            case "1":
                userRepository.AllUsers();
                break;
            case "2":
                while (true)
                {
                    Console.Write("First name (at least two characters): ");
                    string? firstName = Console.ReadLine();
                    Console.Write("Last name (at least two characters): ");
                    string? lastName = Console.ReadLine();
                    Console.Write("Gender (0 for female and 1 for male): ");
                    string? gender = Console.ReadLine();
                    if (firstName != null && lastName != null && firstName.Trim().Length >= 2 && lastName.Trim().Length >= 2 && (gender == "0" || gender == "1"))
                    {
                        await userRepository.AddUser(new User { FirstName = firstName, LastName = lastName, Gender = Convert.ToInt32(gender) });
                        break;
                    }
                    Console.WriteLine("Please enter the data correctly!");
                    Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>");
                }
                break;
            case "3":
                Console.WriteLine("Please select the user who you want to update.");
                userRepository.AllUsers();
                if (UserRepository.users.Count > 0)
                {
                    while (true)
                    {
                        Console.Write("Your choice: ");
                        string? choice = Console.ReadLine();
                        int number;
                        if (int.TryParse(choice, out number))
                        {
                            if (number <= UserRepository.users.Count && number >= 1)
                            {
                                while (true)
                                {
                                    Console.Write("First name (at least two characters): ");
                                    string? firstName = Console.ReadLine();
                                    Console.Write("Last name (at least two characters): ");
                                    string? lastName = Console.ReadLine();
                                    Console.Write("Gender (0 for female and 1 for male): ");
                                    string? gender = Console.ReadLine();
                                    if (firstName != null && lastName != null && firstName.Trim().Length >= 2 && lastName.Trim().Length >= 2 && (gender == "0" || gender == "1"))
                                    {
                                        await userRepository.UpdateUser(number - 1, new User { FirstName = firstName, LastName = lastName, Gender = Convert.ToInt32(gender) });
                                        break;
                                    }
                                    Console.WriteLine("Please enter the data correctly!");
                                    Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>");
                                }
                                break;
                            }
                        }
                        Console.WriteLine("Please enter the user number correctly!");
                    }
                }
                else
                {
                    Console.WriteLine("You can't select any user!");
                }
                break;
            case "4":
                Console.WriteLine("Please select the user who you want to delete.");
                userRepository.AllUsers();
                if (UserRepository.users.Count > 0)
                {
                    while (true)
                    {
                        Console.Write("Your choice: ");
                        string? choice = Console.ReadLine();
                        int number;
                        if (int.TryParse(choice, out number))
                        {
                            if (number <= UserRepository.users.Count && number >= 1)
                            {
                                await userRepository.DeleteUser(number - 1);
                                break;
                            }
                        }
                        Console.WriteLine("Please enter the user number correctly!");
                    }
                }
                else
                {
                    Console.WriteLine("You can't select any user!");
                }
                break;
            case "5":
                flag = true;
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Wrong operation!");
                break;
        }
        if (flag)
        {
            break;
        }
    }
}
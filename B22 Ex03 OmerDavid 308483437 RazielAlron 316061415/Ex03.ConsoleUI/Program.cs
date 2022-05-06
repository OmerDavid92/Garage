using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {

        }

        public void Start()
        {

        }

        public void PrintMainMenu()
        {
            Console.WriteLine("--- Main Menu ---");
            Console.WriteLine("1 - Add vehicle to garage");
            Console.WriteLine("2 - Display list of all vehicle's licenses by vehicle type");
            Console.WriteLine("3 - Change vehicle status");
            Console.WriteLine("4 - Inflate wheel air pressure to max");
            Console.WriteLine("5 - Refuel fuel-vehicle by liters");
            Console.WriteLine("6 - Recharge electric-vehicle by minutes");
            Console.WriteLine("7 - Display vehicle's details by license number");
        }
        public Customer CreateCustomer()
        {
            string ownerName = string.Empty;
            string phoneNumber = string.Empty;

            Console.WriteLine("Please enter Owner Name:");
            ownerName = Console.ReadLine();
            Console.WriteLine("Please enter Phone number:");
            phoneNumber = Console.ReadLine();

            return new Customer(ownerName, phoneNumber);
        }

        public List<string> GetVehicleDetailsFromUser(Garage.VehicleType i_VehicleType)
        {
            List<string> dataMembers = Garage.GetChoosenVehicleTypeDataMembers(i_VehicleType);
            List<string> userInputs = new List<string>();

            Console.WriteLine("--- Add Vehicle Menu ---");

            foreach (string member in dataMembers)
            {
                Console.WriteLine("Please enter {0}:", member);
                userInputs.Add(Console.ReadLine());
            }

            return userInputs;
        }

        public Vehicle CreateVehicle(Garage.VehicleType i_VehicleType, List<string> i_VehicleUserInput)
        {
            Vehicle newVehicle = null;
            bool isParsed = false;
            
            isParsed = Garage.TryParseVehicle(i_VehicleType, i_VehicleUserInput, out newVehicle);
            if (!isParsed)
            {
                //Menu
            }

            return newVehicle;
        }
    }
}

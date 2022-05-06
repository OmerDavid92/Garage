namespace Ex03.ConsoleUI
{
    using Ex03.GarageLogic;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class GarageUI
    {
        public enum GarageMainMenu
        {
            Exit,
            AddVehicle,
            DisplayLicense,
            ChangeStatus,
            InflateWheels,
            Refuel,
            Recharge,
            DisplayVehicleDetails
        }

        private Garage m_Garage = new Garage();



        public GarageMainMenu GetMainMenuInput()
        {
            string userInput = string.Empty;
            int enumSelection = 0;
            bool successfulParse = false;
            int garageMainMenuEnumMaxValue = (int)Enum.GetValues(typeof(GarageMainMenu)).Cast<GarageMainMenu>().Max();

            PrintMainMenu();
            userInput = Console.ReadLine();
            successfulParse = int.TryParse(userInput, out enumSelection);
            
            while (!(successfulParse && enumSelection >= 0 && enumSelection <= garageMainMenuEnumMaxValue)) 
            {
                Console.WriteLine("Wrong Input, Enter a number between 0-{0}", garageMainMenuEnumMaxValue);
                userInput = Console.ReadLine();
                successfulParse = int.TryParse(userInput, out enumSelection);
            }

            return (GarageMainMenu)enumSelection;
        }

        public string GetLicenseNumber()
        {
            string licenseNumber = string.Empty;

            Console.WriteLine("Please Enter License Number");
            licenseNumber = Console.ReadLine();

            while (licenseNumber.Equals(string.Empty))
            {
                Console.WriteLine("Please Enter License Number");
                licenseNumber = Console.ReadLine();
            }

            return licenseNumber;
        }

        public Garage.VehicleType GetVehicleType()
        {
            string vehicleType = string.Empty;
            int enumSelection = 0;
            bool successfulParse = false;
            int vehicleTypeEnumMaxValue = (int)Enum.GetValues(typeof(Garage.VehicleType)).Cast<Garage.VehicleType>().Max();

            Console.WriteLine("Please enter vehicle type: ");
            vehicleType = Console.ReadLine();
            successfulParse = int.TryParse(vehicleType, out enumSelection);
            
            while (!(successfulParse && enumSelection >= 0 && enumSelection <= vehicleTypeEnumMaxValue))
            {
                Console.WriteLine("Wrong Input, Enter a number between 0-{0}", vehicleTypeEnumMaxValue);
                vehicleType = Console.ReadLine();
                successfulParse = int.TryParse(vehicleType, out enumSelection);
            }

            return (Garage.VehicleType)enumSelection;
        }

        ///CHECK WTF HAPPANNING WITH EXCEPTIONS
        private Vehicle getVehicle(Garage.VehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            List<string> vehicleDataMembers = null;

            vehicleDataMembers = GetVehicleDetailsFromUser(i_VehicleType);
            vehicle = CreateVehicle(i_VehicleType, vehicleDataMembers);

            while (vehicle != null)
            {
                Console.WriteLine("Please enter valid vehicle details");
                vehicleDataMembers = GetVehicleDetailsFromUser(i_VehicleType);
                vehicle = CreateVehicle(i_VehicleType, vehicleDataMembers);
            }

            return vehicle;
        }

        public void AddVehicle()
        {
            Vehicle vehicle = null;
            Garage.VehicleType vehicleType = 0;
            Customer customer = null;
            string licenseNumber = GetLicenseNumber();

            if (!m_Garage.CheckAndChangeVehicleStatusInGarage(licenseNumber))
            {
                vehicleType = GetVehicleType();
                customer = CreateCustomer();
                customer.m_Vehicle = getVehicle(vehicleType);
                m_Garage.m_GarageCustomers.Add(customer);
            }
            else
            {
                Console.WriteLine("License Number is Already submitted, Status changed to 'In Repair'");
            }     
        }

        public void Start()
        {
            GarageMainMenu userInput;
            userInput = GetMainMenuInput();

            while (userInput != GarageMainMenu.Exit)
            {
                switch (userInput)
                {
                    case GarageMainMenu.AddVehicle:
                        AddVehicle();
                        break;
                    case GarageMainMenu.DisplayLicense:

                        break;
                    case GarageMainMenu.ChangeStatus:

                        break;
                    case GarageMainMenu.InflateWheels:

                        break;
                    case GarageMainMenu.Refuel:

                        break;
                    case GarageMainMenu.Recharge:

                        break;
                    case GarageMainMenu.DisplayVehicleDetails:

                        break;
                }

                userInput = GetMainMenuInput();
            }
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
            Console.WriteLine("0 - Exit");
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
            List<string> dataMembers = Garage.GetChosenVehicleTypeDataMembers(i_VehicleType);
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

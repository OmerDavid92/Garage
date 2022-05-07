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
        public Customer.CarStatus GetCarStatus()
        {
            string carStatus = string.Empty;
            int enumSelection = 0;
            bool successfulParse = false;
            int vehicleTypeEnumMaxValue = (int)Enum.GetValues(typeof(Customer.CarStatus)).Cast<Customer.CarStatus>().Max();

            Console.WriteLine("Please enter car status: 0 - InRepair, 1 - Repared,  2- Paid");
            carStatus = Console.ReadLine();
            successfulParse = int.TryParse(carStatus, out enumSelection);

            while (!(successfulParse && enumSelection >= 0 && enumSelection <= vehicleTypeEnumMaxValue))
            {
                Console.WriteLine("Wrong Input, Enter a number between 0-{0}", vehicleTypeEnumMaxValue);
                carStatus = Console.ReadLine();
                successfulParse = int.TryParse(carStatus, out enumSelection);
            }

            return (Customer.CarStatus)enumSelection;
        }
        
        public FuelVehicle.FuelType GetFuelType()
        {
            string fuelType = string.Empty;
            int enumSelection = 0;
            bool successfulParse = false;
            int fuelTypeEnumMaxValue = (int)Enum.GetValues(typeof(FuelVehicle.FuelType)).Cast<FuelVehicle.FuelType>().Max();

            Console.WriteLine("Please enter car status: 0 - Soler, 1 - Octan95,  2 - Octan96, 3 - Octan98");
            fuelType = Console.ReadLine();
            successfulParse = int.TryParse(fuelType, out enumSelection);

            while (!(successfulParse && enumSelection >= 0 && enumSelection <= fuelTypeEnumMaxValue))
            {
                Console.WriteLine("Wrong Input, Enter a number between 0-{0}", fuelTypeEnumMaxValue);
                fuelType = Console.ReadLine();
                successfulParse = int.TryParse(fuelType, out enumSelection);
            }

            return (FuelVehicle.FuelType)enumSelection;
        }

        public int GetCarStatusOrAllVehicles()
        {
            string carStatus = string.Empty;
            int userSelection = 0;
            bool successfulParse = false;
            int vehicleTypeEnumMaxValue = (int)Enum.GetValues(typeof(Customer.CarStatus)).Cast<Customer.CarStatus>().Max();

            Console.WriteLine("Please enter car status: 0 - InRepair, 1 - Repared,  2- Paid, -1 - All Vehicles");
            carStatus = Console.ReadLine();
            successfulParse = int.TryParse(carStatus, out userSelection);
            
            while (!(successfulParse && userSelection >= -1 && userSelection <= vehicleTypeEnumMaxValue))
            {
                Console.WriteLine("Wrong Input, Enter a number between -1-{0}", vehicleTypeEnumMaxValue);
                carStatus = Console.ReadLine();
                successfulParse = int.TryParse(carStatus, out userSelection);
            }

            return userSelection;
        }

        public void GetLicenseNumbersOfVehiclesInGarageByStatus()
        {
            List<string> vehiclesLicensesNumberByStatus;
            int carStatus = GetCarStatusOrAllVehicles();
            
            vehiclesLicensesNumberByStatus = m_Garage.GetLisencePlatesByStatus(carStatus);
            if (carStatus == -1)
            {
                Console.WriteLine("Licenses Number of Vehicles in Garage:");
            }
            else
            {
                Console.WriteLine("Licenses Number of Vehicles in Garage with status {0}", (Customer.CarStatus)carStatus);
            }

            foreach (string licenseNumber in vehiclesLicensesNumberByStatus)
            {
                Console.WriteLine(licenseNumber);
            }
        }

        public void ChangeStatus()
        {
            string licenseNumber = GetLicenseNumber();
            Customer.CarStatus carStatus = GetCarStatus();

            m_Garage.ChangeVehicleStatusInGarage(licenseNumber, carStatus);
            Console.WriteLine("Car status changed to {0}", carStatus);
        }

        public void InflateWheels()
        {
            string licenseNumber = GetLicenseNumber();

            m_Garage.InflateMaxWheels(licenseNumber);
            Console.WriteLine("The transInflation accured");
        }

        public float GetAmountToFuel()
        {
            string userInput = string.Empty;
            bool successfulParse = false;
            float amountToFuel = 0;

            while (!(successfulParse && amountToFuel>=0))
            {
                Console.WriteLine("Please Enter Amount of fuel in Liters (Bigger than 0)");
                userInput = Console.ReadLine();
                successfulParse = float.TryParse(userInput, out amountToFuel);
            }

            return amountToFuel;
        }

        public float GetAmountToCharge()
        {
            string userInput = string.Empty;
            bool successfulParse = false;
            float amountToCharge = 0;

            while (!(successfulParse && amountToCharge >= 0))
            {
                Console.WriteLine("Please Enter Amount of charge in hours (Bigger than 0)");
                userInput = Console.ReadLine();
                successfulParse = float.TryParse(userInput, out amountToCharge);
            }

            return amountToCharge;
        }

        public void Refuel()
        {
            string licenseNumber = GetLicenseNumber();
            FuelVehicle.FuelType fuelType = GetFuelType();
            float amountToFuel = GetAmountToFuel();

            m_Garage.RefuelVehicle(licenseNumber, fuelType, amountToFuel);
            Console.WriteLine("Vehicle Refueled");
        }

        public void ReCharge()
        {
            string licenseNumber = GetLicenseNumber();
            float amountToCharge = GetAmountToCharge();

            m_Garage.RechargeVehicle(licenseNumber, amountToCharge);
            Console.WriteLine("Vehicle Recharged");
        }

        public void PrintVehicleProperties()
        {
            string licenseNumber = GetLicenseNumber();
            Vehicle vehicle = m_Garage.GetVehicleByLisenceNumber(licenseNumber);
            List<string> vehicleProperties = vehicle.GetVehicleProperties();
            
            foreach (string property in vehicleProperties)
            {
                Console.WriteLine(property);
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
                        GetLicenseNumbersOfVehiclesInGarageByStatus();
                        break;
                    case GarageMainMenu.ChangeStatus:
                        ChangeStatus();
                        break;
                    case GarageMainMenu.InflateWheels:
                        InflateWheels();
                        break;
                    case GarageMainMenu.Refuel:
                        Refuel();
                        break;
                    case GarageMainMenu.Recharge:
                        ReCharge();
                        break;
                    case GarageMainMenu.DisplayVehicleDetails:
                        PrintVehicleProperties();
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

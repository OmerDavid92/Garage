namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public class Garage
    {
        public enum VehicleType
        {
            FuelCar,
            FuelMotor,
            ElectricCar,
            ElectricMotor,
            Truck
        }

        public List<Customer> m_GarageCustomers { get; } = new List<Customer>();

        public static List<string> GetChosenVehicleTypeDataMembers(VehicleType i_VehicleType)
        {
            List<string> dataMembers = null;

            switch (i_VehicleType)
            {
                case VehicleType.FuelCar:
                    dataMembers = FuelCar.GetDataMembers();
                    break;
                case VehicleType.FuelMotor:
                    dataMembers = FuelMotor.GetDataMembers();
                    break;
                case VehicleType.ElectricCar:
                    dataMembers = ElectricCar.GetDataMembers();
                    break;
                case VehicleType.ElectricMotor:
                    dataMembers = ElectricMotor.GetDataMembers();
                    break;
                case VehicleType.Truck:
                    dataMembers = Truck.GetDataMembers();
                    break;
            }

            return dataMembers;
        }

        public static Vehicle ParseVehicle(VehicleType i_VehicleType, List<string> i_DataMembers)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case VehicleType.FuelCar:
                    vehicle = FuelCar.Parse(i_DataMembers);
                    break;
                case VehicleType.FuelMotor:
                    vehicle = FuelMotor.Parse(i_DataMembers);
                    break;
                case VehicleType.ElectricCar:
                    vehicle = ElectricCar.Parse(i_DataMembers);
                    break;
                case VehicleType.ElectricMotor:
                    vehicle = ElectricMotor.Parse(i_DataMembers);
                    break;
                case VehicleType.Truck:
                    vehicle = Truck.Parse(i_DataMembers);
                    break;
            }

            return vehicle;
        }

        public void ChangeVehicleStatusInGarage(string i_LisenceNumber, Customer.CarStatus i_CarStatus)
        {
            foreach (Customer costumer in m_GarageCustomers)
            {
                if (i_LisenceNumber.Equals(costumer.m_Vehicle.m_LicenseNumber))
                {
                    costumer.m_CarStatus = i_CarStatus;
                }
            }
        }
      
        public bool CheckAndChangeVehicleStatusInGarage(string i_LisenceNumber)
        {
            bool vehicleIsInGarage = isVehicleExistInGarage(i_LisenceNumber);
            if (vehicleIsInGarage)
            {
                ChangeVehicleStatusInGarage(i_LisenceNumber, Customer.CarStatus.InRepair);
            }

            return vehicleIsInGarage;
        }

        public List<string> GetLisencePlatesByStatus(int i_CarStatus)
        {
            List<string> lisencePlatesByStatus = new List<string>();
            foreach (Customer customer in m_GarageCustomers)
            {
                if (i_CarStatus == -1 || customer.m_CarStatus == (Customer.CarStatus)i_CarStatus)
                {
                    lisencePlatesByStatus.Add(customer.m_Vehicle.m_LicenseNumber);
                }
            }

            return lisencePlatesByStatus;
        }

        public void InflateMaxWheels(string i_LisenceNumber)
        {
            foreach (Customer customer in m_GarageCustomers)
            {
                if (customer.m_Vehicle.m_LicenseNumber.Equals(i_LisenceNumber))
                {
                    customer.m_Vehicle.inflateMaxWheels();
                    break;
                }
            }
        }

        public void RefuelVehicle(string i_LisenceNumber, FuelVehicle.FuelType i_FuelType, float i_AmountToFuel)
        {
            FuelVehicle fuelVehicle = null;
            foreach (Customer customer in m_GarageCustomers)
            {
                if (customer.m_Vehicle.m_LicenseNumber.Equals(i_LisenceNumber))
                {
                    fuelVehicle = customer.m_Vehicle as FuelVehicle;
                    if (fuelVehicle != null)
                    {
                        fuelVehicle.RefuelByAmount(i_FuelType, i_AmountToFuel);
                    }

                    break;
                }
            }
        }

        public void RechargeVehicle(string i_LisenceNumber, float i_TimeToCharge)
        {
            ElectricVehicle electricVehicle = null;
            foreach (Customer customer in m_GarageCustomers)
            {
                if (customer.m_Vehicle.m_LicenseNumber.Equals(i_LisenceNumber))
                {
                    electricVehicle = customer.m_Vehicle as ElectricVehicle;
                    if (electricVehicle != null)
                    {
                        electricVehicle.Recharge(i_TimeToCharge);
                    }

                    break;
                }
            }
        }

        public Vehicle GetVehicleByLisenceNumber(string i_LisenceNumber)
        {
            Vehicle vehicle = null;
            foreach (Customer customer in m_GarageCustomers)
            {
                if (customer.m_Vehicle.m_LicenseNumber.Equals(i_LisenceNumber))
                {
                    vehicle = customer.m_Vehicle;
                    break;
                }
            }

            return vehicle;
        }

        private bool isVehicleExistInGarage(string i_LisenceNumber)
        {
            bool isVehicleExistInGarage = false;
            foreach (Customer costumer in m_GarageCustomers)
            {
                if (i_LisenceNumber.Equals(costumer.m_Vehicle.m_LicenseNumber))
                {
                    isVehicleExistInGarage = true;
                }
            }

            return isVehicleExistInGarage;
        }
    }
}

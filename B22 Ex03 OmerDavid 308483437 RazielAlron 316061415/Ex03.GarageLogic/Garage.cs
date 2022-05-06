using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
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

        public static List<string> GetChosenVehicleTypeDataMembers (VehicleType i_VehicleType)
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
                //case VehicleType.Truck:
                //    dataMembers = Truck.GetDataMembers();
                //    break;
            }

            return dataMembers;
        }

        private bool isVehicleExistInGarage(string i_LisenceNumber)
        {
            bool isVehicleExistInGarage = false;
            foreach (Customer costumer in m_GarageCustomers)
            {
                if (i_LisenceNumber.Equals(costumer.m_Vehicle.m_LisenceNumber))
                {
                    isVehicleExistInGarage = true;
                }
            }

            return isVehicleExistInGarage;
        }

        public void ChangeVehicleStatusInGarage (string i_LisenceNumber, Customer.CarStatus i_CarStatus)
        {
            foreach (Customer costumer in m_GarageCustomers)
            {
                if (i_LisenceNumber.Equals(costumer.m_Vehicle.m_LisenceNumber))
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
                    lisencePlatesByStatus.Add(customer.m_Vehicle.m_LisenceNumber);
                }
            }

            return lisencePlatesByStatus;
        }

        public void InflateMaxWheels(string i_LisenceNumber)
        {
            foreach (Customer customer in m_GarageCustomers)
            {
                if (customer.m_Vehicle.m_LisenceNumber.Equals(i_LisenceNumber))
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
                if (customer.m_Vehicle.m_LisenceNumber.Equals(i_LisenceNumber))
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
                if (customer.m_Vehicle.m_LisenceNumber.Equals(i_LisenceNumber))
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
                if (customer.m_Vehicle.m_LisenceNumber.Equals(i_LisenceNumber))
                {
                    vehicle = customer.m_Vehicle;
                    break;
                }
            }

            return vehicle;
        }

        public static bool TryParseVehicle(VehicleType i_VehicleType, List<string> i_DataMembers, out Vehicle o_Vehicle)
        {
            o_Vehicle = null;
            bool successfulParse = true;

            switch (i_VehicleType)
            {
                case VehicleType.FuelCar:
                    successfulParse = FuelCar.TryParse(i_DataMembers, out o_Vehicle);
                    break;
                case VehicleType.FuelMotor:
                    successfulParse = FuelMotor.TryParse(i_DataMembers, out o_Vehicle);
                    break;
                case VehicleType.ElectricCar:
                    successfulParse = ElectricCar.TryParse(i_DataMembers, out o_Vehicle);
                    break;
                case VehicleType.ElectricMotor:
                    successfulParse = ElectricMotor.TryParse(i_DataMembers, out o_Vehicle);
                    break;
                    //case VehicleType.Truck:
                    //    successfulParse = Truck.TryParse(i_DataMembers, out o_Vehicle);
                    //    break;
            }

            return successfulParse;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Customer
    {
        private enum CarStatus
        {
            InRepair,
            Repared,
            Paid
        }

        private string m_OwnerName;
        private string m_phoneNumber;
        private CarStatus m_CarStatus = CarStatus.InRepair;
        


    }
}

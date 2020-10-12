using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Order
    {
        private readonly string r_CustomerName;
        private readonly string r_CustomerPhoneNumber;
        private readonly Vehicle r_Vehicle;
        private eOrderStatus m_VehicleStatus;

        internal Order(
            string i_CustomerName,
            string i_CustomerPhoneNumber,
            Vehicle i_Vehicle)
        {
            r_CustomerName = setrCustomerName(i_CustomerName);
            r_CustomerPhoneNumber = setPhoneNumber(i_CustomerPhoneNumber);
            r_Vehicle = i_Vehicle;
            m_VehicleStatus = eOrderStatus.OnRepair;
        }

        public string CustomerName
        {
            get
            {
                return r_CustomerName;
            }
        }

        public string CustomerPhoneNumber
        {
            get
            {
                return r_CustomerPhoneNumber;
            }
        }

        public eOrderStatus Status
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public string VehicleLicenseNumber
        {
            get
            {
                return Vehicle.LicenseNumber;
            }
        }

        public string VehicleModel
        {
            get
            {
                return Vehicle.ModelName;
            }
        }

        public bool IsOnRepair()
        {
            return m_VehicleStatus == eOrderStatus.OnRepair;
        }

        private string setrCustomerName(string i_CostumerName)
        {
            if (i_CostumerName != null &&
                i_CostumerName.Length <= 20 && i_CostumerName.Length > 0)
            {
                foreach (char ch in i_CostumerName)
                {
                    if (!char.IsLetter(ch))
                    {
                        throw new FormatException("invalid costumer name format");
                    }
                }
            }
            else
            {
                throw new FormatException("invalid costumer name format");
            }

            return i_CostumerName;
        }

        private string setPhoneNumber(string i_CustomerPhoneNumber)
        {
            if (i_CustomerPhoneNumber != null && i_CustomerPhoneNumber.Length == 10)
            {
                foreach (char ch in i_CustomerPhoneNumber)
                {
                    if (!char.IsDigit(ch))
                    {
                        throw new FormatException("invalid phone number format");
                    }
                }
            }
            else
            {
                throw new FormatException("invalid phone number format");
            }

            return i_CustomerPhoneNumber;
        }

        public override string ToString()
        {
            StringBuilder orderSb = new StringBuilder();
            orderSb.Append("\nCustomer name: " + CustomerName);
            orderSb.Append("\nStatus: " + Status.ToString());
            orderSb.Append("\nVehicle type: " + Vehicle.ToString());
            return orderSb.ToString();
        }
    }
}

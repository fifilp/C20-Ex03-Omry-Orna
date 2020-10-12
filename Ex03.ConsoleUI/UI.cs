using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal static class UI
    {
        public static void RunGarageOperator()
        {
            Garage garage = new Garage();
            while (true)
            {
                showMainMenu();
                int choice = getMenuOption();
                eMenuOption option = (eMenuOption)choice;

                try
                {
                    switchOption(garage, option);
                }
                catch (ValueOutOfRangeException valEx)
                {
                    Console.WriteLine(valEx.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
        }

        private static int getMenuOption()
        {
            int choice;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (Enum.IsDefined(typeof(eMenuOption), choice))
                    {
                        break;
                    }
                }
                else
                {
                    Console.Write("enter a number from 1 to 7: ");
                }
            }

            return choice;
        }

        private static void showMainMenu()
        {
            Console.WriteLine(@"select from the following menu:
1.Enter new vehicle
2.Show license numbers for all vehicles in garage (filtered by status)
3.Change vehicle status
4.Inflate wheels to max
5.Fill up tank
6.Recharge battery
7.Show all details for a license number");
        }

        private static void switchOption(Garage i_Garage, eMenuOption i_Option)
        {
            switch (i_Option)
            {
                case eMenuOption.EnterNewVehicle:
                    insertVehicleToGarage(i_Garage);
                    break;

                case eMenuOption.ShowLicenseNumbers:
                    showLicenseNumbersByStatus(i_Garage);
                    break;

                case eMenuOption.ChangeStatus:
                    changeOrderStatus(i_Garage);
                    break;

                case eMenuOption.Inflate:
                    inflateAllWheels(i_Garage);
                    break;

                case eMenuOption.FillUp:
                    fillUpTank(i_Garage);
                    break;

                case eMenuOption.Recharge:
                    recharge(i_Garage);
                    break;

                case eMenuOption.ShowAllDetails:
                    showFullDetailsAboutOrder(i_Garage);
                    break;
            }
        }

        // 1.Enter new vehicle
        private static void insertVehicleToGarage(Garage i_Garage)
        {
            string licenseNumber = getLicenseNumber();

            if (i_Garage.isVehicleInGarage(licenseNumber))
            {
                i_Garage.ChangeStatus(licenseNumber, eOrderStatus.OnRepair);
                Console.WriteLine("The vehicle is already in the system. status changed to OnRepair");
            }
            else
            {
                Vehicle vehicle = createVehicleFromList(licenseNumber);
                bool toContinue = true;
                while (toContinue)
                {
                    try
                    {
                        placeOrder(i_Garage, vehicle);
                        Console.WriteLine("Vehicle added to garage ");
                        toContinue = false;
                    }
                    catch (FormatException formatException)
                    {
                        Console.WriteLine(formatException.Message);
                    }
                }
            }
        }

        // 2.Show license number for all vehicles in garage sorted by status
        private static void showLicenseNumbersByStatus(Garage i_Garage)
        {
            eOrderStatus? status = null;
            tryToGetStatusFromUser(ref status);

            List<string> licenseNumbers = i_Garage.GetListOfLicenseNumbersByStatus(status);
            if (licenseNumbers.Count == 0)
            {
                Console.WriteLine("There is no vehicles to display");
            }

            foreach (string licenseNumber in licenseNumbers)
            {
                Console.WriteLine(licenseNumber);
            }
        }

        // 3.Change vehicle status
        private static void changeOrderStatus(Garage i_Garage)
        {
            string licenseNumber = getLicenseNumber();
            eOrderStatus status = getEnumValue<eOrderStatus>();

            i_Garage.ChangeStatus(licenseNumber, status);
            Console.WriteLine(string.Format("Status changed to {0} ", status));
        }

        // 4.Inflate wheels to max
        private static void inflateAllWheels(Garage i_Garage)
        {
            string licenseNumber = getLicenseNumber();

            if (i_Garage.IsOnRepair(licenseNumber))
            {
                i_Garage.InflateVehicleWheelsToMax(licenseNumber);
                Console.WriteLine("all wheels inflated to max");
            }
            else
            {
                Console.WriteLine("Please change order status to OnRepair");
            }
        }

        // 5.Fill up tank
        private static void fillUpTank(Garage i_Garage)
        {
            string licenseNumber = getLicenseNumber();
            eGasType gas = getEnumValue<eGasType>();
            float litersToFill;

            Console.Write("how many liters of gas to fill? ");

            while (!float.TryParse(Console.ReadLine(), out litersToFill))
            {
                Console.WriteLine("invalid reply. Enter number of liters.");
            }

            i_Garage.FillUpTank(licenseNumber, gas, litersToFill);
            Console.WriteLine(string.Format("Filled {0} liters of {1}", litersToFill, gas));
        }

        // 6.Recharge battery
        private static void recharge(Garage i_Garage)
        {
            string licenseNumber = getLicenseNumber();
            float minutesToCharge;

            Console.Write("how many minutes to charge? ");
            while (!float.TryParse(Console.ReadLine(), out minutesToCharge))
            {
                Console.WriteLine("invalid reply. Enter number of minutes.");
            }

            // converting minutes to hours to charge
            i_Garage.RechargeBattery(licenseNumber, minutesToCharge / 60);
            Console.WriteLine(string.Format("Recharged {0} minutes ", minutesToCharge));
        }

        // 7.Show all details for a license number
        private static void showFullDetailsAboutOrder(Garage i_Garage)
        {
            string licenseNumber = getLicenseNumber();
            Console.WriteLine(i_Garage.GetOrder(licenseNumber).ToString());
        }

        private static string getLicenseNumber()
        {
            string licenseNumber = null;

            while (!isValidLicenseNumber(licenseNumber))
            {
                Console.Write("enter license number(1-20 letters and digits only): ");
                licenseNumber = Console.ReadLine();
            }

            return licenseNumber;
        }

        private static int getVehicleTypeNumber()
        {
            int choice;

            while (true)
            {
                Console.Write(string.Format(
                    "enter a number from 1 to {0}: ",
                    Factory.SupportedVehicles.Count));

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 1 && choice <= Factory.SupportedVehicles.Count)
                    {
                        break;
                    }
                }
            }

            return choice;
        }

        private static T getEnumValue<T>()
        {
            string value = string.Empty;
            
            while (!Enum.IsDefined(typeof(T), value))
            {
                printEnumNames<T>();
                value = Console.ReadLine();
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        private static void printEnumNames<T>()
        {
            StringBuilder enumNamesSB = new StringBuilder();
            string[] enumNames = Enum.GetNames(typeof(T));
            enumNamesSB.Append("Enter a new status from the following: ");
            
            foreach (string enumName in enumNames)
            {
                enumNamesSB.AppendFormat("{0}, ", enumName);
            }

            Console.WriteLine(enumNamesSB.ToString());
        }

        private static void placeOrder(Garage i_Garage, Vehicle i_Vehicle)
        {
            string name, phoneNumber;
            Console.Write("customer name (1-20 letters only): ");
            name = Console.ReadLine();
            Console.Write("customer phone number (10 digits only): ");
            phoneNumber = Console.ReadLine();
            i_Garage.AddNewOrderToGarage(i_Vehicle, name, phoneNumber);
        }

        private static Vehicle createVehicleFromList(string i_LicenseNumber)
        {
            Vehicle vehicle = null;
            int vehicleType = chooseVehicleFromList();

            List<string> parametersToRequest = Factory.GetParameters(vehicleType);
            List<string> parametersReceived = new List<string>();

            bool toContinue = true;
            while (toContinue)
            {
                try
                {
                    foreach (string parameterName in parametersToRequest)
                    {
                        Console.Write(string.Format("{0}: ", parameterName));
                        parametersReceived.Add(Console.ReadLine());
                    }

                    vehicle = Factory.CreateVehicle(vehicleType, i_LicenseNumber, parametersReceived);
                    toContinue = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    parametersReceived.Clear();
                }
            }

            return vehicle;
        }

        private static int chooseVehicleFromList()
        {
            Console.WriteLine("The supported vehicles to choose from:");
            int i = 1;

            foreach (string vehicleType in Factory.m_SupportedVehicles.Keys)
            {
                Console.WriteLine(string.Format("{0}. {1}", i, vehicleType));
                i++;
            }

            int replyNum = getVehicleTypeNumber();

            return replyNum;
        }

        private static void tryToGetStatusFromUser(ref eOrderStatus? io_status)
        {
            printEnumNames<eOrderStatus>();
            Console.WriteLine("to filter by order status, or nothing for no filter");
            string reply = Console.ReadLine();

            if (reply.Length > 0)
            {
                if (Enum.IsDefined(typeof(eOrderStatus), reply))
                {
                    io_status = (eOrderStatus)Enum.Parse(typeof(eOrderStatus), reply);
                }
                else
                {
                    Console.WriteLine("Invalid status entered, displaying the full list (unfiltered)");
                }
            }
        }

        private static bool isValidLicenseNumber(string i_LicenseNumber)
        {
            bool isValidFormat = true;

            if (i_LicenseNumber != null &&
                i_LicenseNumber.Length <= 20 && i_LicenseNumber.Length > 0)
            {
                foreach (char ch in i_LicenseNumber)
                {
                    if (!char.IsLetterOrDigit(ch))
                    {
                        isValidFormat = false;
                        break;
                    }
                }
            }
            else
            {
                isValidFormat = false;
            }

            return isValidFormat;
        }

        private enum eMenuOption
        {
            EnterNewVehicle = 1, ShowLicenseNumbers, ChangeStatus, Inflate, FillUp, Recharge, ShowAllDetails
        }
    }
}
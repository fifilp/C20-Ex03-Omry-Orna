using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public static class Factory
    {
        public static Dictionary<string, List<string>> m_SupportedVehicles =
           new Dictionary<string, List<string>>()
           {
               { "ElectricCar", ElectricCar.ParamatersToRequest },
               { "RegularCar", RegularCar.ParamatersToRequest },
               { "ElectricMotorcycle", ElectricMotorcycle.ParamatersToRequest },
               { "RegularMotorcycle", RegularMotorcycle.ParamatersToRequest },
               { "Truck", Truck.ParamatersToRequest }
           };

        private enum eVehicleType
        {
            ElectricCar = 1, RegularCar, ElectricMotorcycle, RegularMotorcycle, Truck
        }

        public static Vehicle CreateVehicle(int i_VehicleType, string i_LicenseNumber, List<string> i_ParametersReceived)
        {
            Vehicle vehicle = null;

            eVehicleType vehicleType = (eVehicleType)i_VehicleType;

            switch (vehicleType)
            {
                case eVehicleType.ElectricCar:
                    vehicle = createElectricCar(i_LicenseNumber, i_ParametersReceived);
                    break;

                case eVehicleType.RegularCar:
                    vehicle = createRegularCar(i_LicenseNumber, i_ParametersReceived);
                    break;

                case eVehicleType.ElectricMotorcycle:
                    vehicle = createElectricMotorcycle(i_LicenseNumber, i_ParametersReceived);
                    break;

                case eVehicleType.RegularMotorcycle:
                    vehicle = createRegularMotorcycle(i_LicenseNumber, i_ParametersReceived);
                    break;

                case eVehicleType.Truck:
                    vehicle = createTruck(i_LicenseNumber, i_ParametersReceived);
                    break;
            }

            return vehicle;
        }

        private static ElectricMotorcycle createElectricMotorcycle(
            string i_LicenseNumber,
            List<string> i_Parameters)
        {
            string modelName = i_Parameters[0];
            string wheelsManufacturer = i_Parameters[1];
            float currentPressure = parseFloat(i_Parameters[2], "current pressure");
            eLicenseType licenseType = parseEnum<eLicenseType>(i_Parameters[3], "license type");
            int engineVolume = parseInt(i_Parameters[4], "engine volume");
            float remainingTime = parseFloat(i_Parameters[5], "remaining time");

            return new ElectricMotorcycle(
                i_LicenseNumber,
                modelName,
                wheelsManufacturer,
                currentPressure,
                licenseType,
                engineVolume,
                remainingTime);
        }

        private static RegularMotorcycle createRegularMotorcycle(
            string i_LicenseNumber,
            List<string> i_Parameters)
        {
            string modelName = i_Parameters[0];
            string wheelsManufacturer = i_Parameters[1];
            float currentPressure = parseFloat(i_Parameters[2], "current pressure");
            eLicenseType licenseType = parseEnum<eLicenseType>(i_Parameters[3], "license type");
            int engineVolume = parseInt(i_Parameters[4], "engine volume");
            float currentGas = parseFloat(i_Parameters[5], "current gas");

            return new RegularMotorcycle(
                            i_LicenseNumber,
                            modelName,
                            wheelsManufacturer,
                            currentPressure,
                            licenseType,
                            engineVolume,
                            currentGas);
        }

        private static ElectricCar createElectricCar(
            string i_LicenseNumber,
            List<string> i_Parameters)
        {
            string modelName = i_Parameters[0];
            string wheelsManufacturer = i_Parameters[1];
            float currentPressure = parseFloat(i_Parameters[2], "current pressure");
            eCarColor color = parseEnum<eCarColor>(i_Parameters[3], "color");
            eNumOfDoors numOfDoors = parseEnum<eNumOfDoors>(i_Parameters[4], "number of doors");
            float remainingTime = parseFloat(i_Parameters[5], "remaining time");

            return new ElectricCar(
                i_LicenseNumber,
                modelName,
                wheelsManufacturer,
                currentPressure,
                color,
                numOfDoors,
                remainingTime);
        }

        private static RegularCar createRegularCar(
            string i_LicenseNumber,
            List<string> i_Parameters)
        {
            string modelName = i_Parameters[0];
            string wheelsManufacturer = i_Parameters[1];
            float currentPressure = parseFloat(i_Parameters[2], "current pressure");
            eCarColor color = parseEnum<eCarColor>(i_Parameters[3], "color");
            eNumOfDoors numOfDoors = parseEnum<eNumOfDoors>(i_Parameters[4], "number of doors");
            float currentGas = parseFloat(i_Parameters[5], "current gas");

            return new RegularCar(
                i_LicenseNumber,
                modelName,
                wheelsManufacturer,
                currentPressure,
                color,
                numOfDoors,
                currentGas);
        }

        private static Truck createTruck(
            string i_LicenseNumber,
            List<string> i_Parameters)
        {
            string modelName = i_Parameters[0];
            string wheelsManufacturer = i_Parameters[1];
            float currentPressure = parseFloat(i_Parameters[2], "current pressure");
            bool hasDangerousMaterial = i_Parameters[3] == "Y";
            float cargoVolume = parseFloat(i_Parameters[4], "cargo volume");
            float currentGas = parseFloat(i_Parameters[5], "current gas");

            return new Truck(
                i_LicenseNumber,
                modelName,
                wheelsManufacturer,
                currentPressure,
                hasDangerousMaterial,
                cargoVolume,
                currentGas);
        }

        public static Dictionary<string, List<string>> SupportedVehicles
        {
            get
            {
                return m_SupportedVehicles;
            }
        }

        public static List<string> GetParameters(int i_Vehicletype)
        {
            string type = ((eVehicleType)i_Vehicletype).ToString();
            return m_SupportedVehicles[type];
        }

        private static int parseInt(string i_str, string i_propName)
        {
            int result;
            if (!int.TryParse(i_str, out result))
            {
                string msg = string.Format("{0} must be a numeric value", i_propName);
                throw new FormatException(msg);
            }

            return result;
        }

        private static float parseFloat(string i_str, string i_propName)
        {
            float result;
            if (!float.TryParse(i_str, out result))
            {
                string msg = string.Format("{0} must be a numeric value", i_propName);
                throw new FormatException(msg);
            }

            return result;
        }

        private static T parseEnum<T>(string i_Str, string i_PropName)
        {
            T result;
            if (Enum.IsDefined(typeof(T), i_Str))
            {
                result = (T)Enum.Parse(typeof(T), i_Str);
            }
            else
            {
                string msg = string.Format("{0} invalid value", i_PropName);
                throw new FormatException(msg);
            }

            return result;
        }
    }
}

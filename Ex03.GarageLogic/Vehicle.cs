using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        protected readonly Engine r_Engine;
        private readonly List<Wheel> r_Wheels;

        public Vehicle(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_MaxAirPressure,
            float i_CurrentPressure,
            Engine i_Engine,
            int i_NumOfWheels)
        {
            r_ModelName = setModelName(i_ModelName);
            r_LicenseNumber = setLicenseNumber(i_LicenseNumber);
            r_Engine = i_Engine;
            r_Wheels = createSetOfWheels(i_NumOfWheels, i_WheelsManufacturer, i_MaxAirPressure, i_CurrentPressure);
        }

        public string ModelName
        {
            get
            {
                return r_ModelName;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public Engine Engine
        {
            get
            {
                return r_Engine;
            }
        }

        public float PercentOfEnergyLeft
        {
            get
            {
                return 100 * (r_Engine.CurrentValue / r_Engine.MaxCapacity);
            }
        }

        public float CurrentAmountOfPower
        {
            get
            {
                return r_Engine.CurrentValue;
            }
        }

        public float MaxCapacityOfPower
        {
            get
            {
                return r_Engine.MaxCapacity;
            }
        }       

        private List<Wheel> createSetOfWheels(
            int i_NumOfWheels,
            string i_WheelsManufacturer,
            float i_MaxAirPressure,
            float i_CurrentPressure)
        {
            List<Wheel> wheelSet = new List<Wheel>();

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                wheelSet.Add(new Wheel(i_WheelsManufacturer, i_MaxAirPressure, i_CurrentPressure));
            }

            return wheelSet;
        }

        private string setModelName(string i_ModelName)
        {
            if (i_ModelName != null &&
                i_ModelName.Length <= 20 && i_ModelName.Length > 0)
            {
                foreach (char ch in i_ModelName)
                {
                    if (!char.IsLetterOrDigit(ch))
                    {
                        throw new FormatException("invalid model name format");
                    }
                }
            }
            else
            {
                throw new FormatException("invalid model name format");
            }

            return i_ModelName;
        }

        private string setLicenseNumber(string i_LicenseNumber)
        {
            if (i_LicenseNumber != null &&
                i_LicenseNumber.Length <= 20 && i_LicenseNumber.Length > 0)
            {
                foreach (char ch in i_LicenseNumber)
                {
                    if (!char.IsLetterOrDigit(ch))
                    {
                        throw new FormatException("invalid license number format");
                    }
                }
            }
            else
            {
                throw new FormatException("invalid license number format");
            }

            return i_LicenseNumber;
        }

        public bool isGasPowered()
        {
            return r_Engine is GasEngine;
        }

        public bool isElectricPowered()
        {
            return r_Engine is ElectricEngine;
        }

        public void InfalteAllWheelsToMax()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.InflateToMax();
            }
        }

        public virtual void FillUpTank(eGasType i_GasType, float i_Amount)
        {
            throw new Exception("vehicle is not powered by gas");
        }

        public virtual void RechargeBattery(float i_HoursToCharge)
        {
            throw new Exception("vehicle is not powered by electricity");
        }

        public override string ToString()
        {
            StringBuilder vehicleString = new StringBuilder();
            vehicleString.Append(string.Format(
                @"license number: {0}
model: {1}",
                LicenseNumber,
                r_ModelName));
            vehicleString.Append(string.Format("\n{0} wheels:", r_Wheels.Count));
            vehicleString.Append("\n" + wheelsToString());
            return vehicleString.ToString();
        }

        private string wheelsToString()
        {
            StringBuilder wheelsString = new StringBuilder();
            foreach (Wheel wheel in r_Wheels)
            {
                wheelsString.Append(wheel.ToString() + "\n");
            }

            return wheelsString.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const float k_MaxCargo = 100f;
        private const int k_WheelMaxPressure = 30;
        private const int k_NumOfWheels = 16;
        private const float k_MaxAmountOfGas = 105;

        private readonly float r_CargoVolume;
        private readonly bool r_ContainDangerousMaterial;

        internal Truck(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            bool i_ContainDangerousMaterial,
            float i_CargoVolume,
            float i_CurrentGas)
            : base(
                  i_LicenseNumber,
                  i_ModelName,
                  i_WheelsManufacturer,
                  k_WheelMaxPressure,
                  i_CurrentAirPressure,
                  new GasEngine(eGasType.Soler, k_MaxAmountOfGas, i_CurrentGas),
                  k_NumOfWheels)
        {
            r_ContainDangerousMaterial = i_ContainDangerousMaterial;
            r_CargoVolume = setCargoVolume(i_CargoVolume);
        }

        public static List<string> ParamatersToRequest
        {
            get
            {
                return new List<string>()
                    {
                        "Model Name (1-20 char, letters and digits only)",
                        "Wheels Manufacturer (1-20 char, letters and digits only)",
                        "Wheels Current Pressure",
                        "Cargo contains dangerous materials? (Y/N)",
                        "Cargo Volume",
                        "Current amount of gas"
                    };
            }
        }

        public GasEngine GasEngine
        {
            get
            {
                return (GasEngine)r_Engine;
            }
        }

        public eGasType GasType
        {
            get
            {
                return GasEngine.GasType;
            }
        }

        public float CurrentAmountOfGas
        {
            get
            {
                return GasEngine.CurrentAmountOfGas;
            }
        }

        public float MaxAmountOfGas
        {
            get
            {
                return GasEngine.MaxAmountOfGas;
            }
        }

        public override void FillUpTank(eGasType i_GasType, float i_GasLitresToFill)
        {
            GasEngine.FillUpTank(i_GasType, i_GasLitresToFill);
        }

        private float setCargoVolume(float i_CargoVolume)
        {
            if (i_CargoVolume < 0 || i_CargoVolume > k_MaxCargo)
            {
                throw new ValueOutOfRangeException(
                    "cargo is out of valid range",
                    k_MaxCargo);
            }

            return i_CargoVolume;
        }

        public override string ToString()
        {
            StringBuilder truckSb = new StringBuilder();
            truckSb.Append("Truck\n");
            truckSb.Append(base.ToString());
            truckSb.Append("Max amount of gas: "
                + MaxAmountOfGas.ToString());
            truckSb.Append(", Current amount of gas: "
                + CurrentAmountOfGas.ToString());
            truckSb.Append("\nGas type: " + GasType.ToString());
            truckSb.Append(
                "\nCargo contains dangerous material? " +
                r_ContainDangerousMaterial.ToString());
            truckSb.Append("\nCargo volume: " + this.r_CargoVolume.ToString());
            return truckSb.ToString();
        }
    }
}

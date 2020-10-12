using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        private const int k_WheelMaxPressure = 32;
        private const int k_NumOfWheels = 4;

        private readonly eCarColor r_Color;
        private readonly eNumOfDoors r_NumOfDoors;

        protected Car(
            string i_LicenseNumber,
            string i_ModelName,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            Engine i_Engine,
            eCarColor i_Color,
            eNumOfDoors i_NumOfDoorsInCar)
            : base(
                  i_LicenseNumber,
                  i_ModelName,
                  i_WheelsManufacturer,
                  k_WheelMaxPressure,
                  i_CurrentAirPressure,
                  i_Engine,
                  k_NumOfWheels)
        {
            r_Color = i_Color;
            r_NumOfDoors = i_NumOfDoorsInCar;
        }

        protected static List<string> ParamatersToRequest
        {
            get
            {
                return new List<string>()
                    {
                        "Model Name (1-20 char, letters and digits only)",
                        "Wheels Manufacturer (1-20 char, letters and digits only)",
                        "Wheels current pressure",
                        "Color (Gray, White, Green, Red)",
                        "Number Of Doors (2,3,4,5)"
                    };
            }
        }

        public eCarColor Color
        {
            get
            {
                return r_Color;
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return r_NumOfDoors;
            }
        }

        public override string ToString()
        {
            StringBuilder carSB = new StringBuilder();
            carSB.Append(base.ToString());
            carSB.Append("color: " + r_Color.ToString());
            carSB.Append(", " + r_NumOfDoors.ToString() + " doors");

            return carSB.ToString();
        }
    }
}

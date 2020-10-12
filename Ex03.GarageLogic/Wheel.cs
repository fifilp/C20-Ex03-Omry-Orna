using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_CurrentAirPressure = 0)
        {
            r_ManufacturerName = setManufacturerName(i_ManufacturerName);
            r_MaxAirPressure = i_MaxAirPressure;
            Inflate(i_CurrentAirPressure);
        }

        public string ManufacturerName
        {
            get
            {
                return r_ManufacturerName;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public float CurrentPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        private string setManufacturerName(string i_ManufacturerName)
        {
            if (i_ManufacturerName != null &&
                i_ManufacturerName.Length <= 20 && i_ManufacturerName.Length > 0)
            {
                foreach (char ch in i_ManufacturerName)
                {
                    if (!char.IsLetterOrDigit(ch))
                    {
                        throw new FormatException("invalid manufacturer name format");
                    }
                }
            }
            else
            {
                throw new FormatException("invalid manufacturer name format");
            }

            return i_ManufacturerName;
        }

        internal void Inflate(float i_AmountOfAir)
        {
            if (m_CurrentAirPressure + i_AmountOfAir > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException("Too much pressure", r_MaxAirPressure - m_CurrentAirPressure, 0);
            }

            if (i_AmountOfAir < 0)
            {
                throw new ValueOutOfRangeException("Can't inflate a negative value", r_MaxAirPressure - m_CurrentAirPressure, 0);
            }

            m_CurrentAirPressure += i_AmountOfAir;
        }

        internal void InflateToMax()
        {
            Inflate(r_MaxAirPressure - m_CurrentAirPressure);
        }

        public override string ToString()
        {
            string wheelString = string.Format(
                @"Manufacturer: {0}, max air pressure: {1}, current air pressure: {2}",
                r_ManufacturerName,
                r_MaxAirPressure,
                m_CurrentAirPressure);

            return wheelString;
        }
    }
}

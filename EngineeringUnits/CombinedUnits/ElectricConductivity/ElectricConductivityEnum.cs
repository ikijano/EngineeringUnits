﻿using EngineeringUnits.Units;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EngineeringUnits.Units
{


    public class ElectricConductivityUnit : Enumeration
    {

        public static readonly ElectricConductivityUnit SI = new(ElectricAdmittanceUnit.SI, LengthUnit.SI);
        public static readonly ElectricConductivityUnit SiemensPerMeter = new(ElectricAdmittanceUnit.Siemens, LengthUnit.Meter);
        public static readonly ElectricConductivityUnit SiemensPerInch = new(ElectricAdmittanceUnit.Siemens, LengthUnit.Inch);
        public static readonly ElectricConductivityUnit SiemensPerFoot = new(ElectricAdmittanceUnit.Siemens, LengthUnit.Foot);



        public ElectricConductivityUnit(ElectricAdmittanceUnit electricAdmittance, LengthUnit Length)
        {
            Unit = new UnitSystem(electricAdmittance / Length, 
                               $"{electricAdmittance}/{Length}");
        }


    }




}
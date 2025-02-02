﻿using EngineeringUnits.Units;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EngineeringUnits.Units
{


    public record PermeabilityUnit : UnitTypebase
    {

        public static readonly PermeabilityUnit SI = new(ElectricInductanceUnit.SI, LengthUnit.SI);
        public static readonly PermeabilityUnit HenryPerMeter = new(ElectricInductanceUnit.Henry, LengthUnit.Meter);




        public PermeabilityUnit(ElectricInductanceUnit electricInductance, LengthUnit Length)
        {
            Unit = new UnitSystem(electricInductance / Length, 
                               $"{electricInductance}/{Length}");
        }

        public override string ToString()
        {
            if (Unit.Symbol is not null)
                return $"{Unit.Symbol}";

            return $"{Unit}";
        }

    }




}
﻿using EngineeringUnits.Units;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EngineeringUnits.Units
{


    public class ElectricChargeDensityUnit : Enumeration
    {

        public static readonly ElectricChargeDensityUnit SI = new(ElectricChargeUnit.SI, VolumeUnit.SI);
        public static readonly ElectricChargeDensityUnit CoulombPerCubicMeter = new(ElectricChargeUnit.Coulomb, VolumeUnit.CubicMeter);




        public ElectricChargeDensityUnit(ElectricChargeUnit electricCharge, VolumeUnit volume, string NewSymbol = "Empty")
        {
            Unit = electricCharge / volume;
            SetNewSymbol(NewSymbol, $"{electricCharge}/{volume}");
        }

      

    }




}
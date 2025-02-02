﻿using EngineeringUnits.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringUnits
{
    public partial class Temperature
    {

        /// <summary>
        ///     Get Temperature in DegreesCelsius.
        /// </summary>
        public double DegreesCelsius => As(TemperatureUnit.DegreeCelsius);

        /// <summary>
        ///     Get Temperature in DegreesDelisle.
        /// </summary>
        [Obsolete("This Temperature unit is not yet supported!", true)]
        public double DegreesDelisle;

        /// <summary>
        ///     Get Temperature in DegreesFahrenheit.
        /// </summary>
        public double DegreesFahrenheit => As(TemperatureUnit.DegreeFahrenheit);

        /// <summary>
        ///     Get Temperature in DegreesNewton.
        /// </summary>
        [Obsolete("This Temperature unit is not yet supported!", true)]
        public double DegreesNewton;

        /// <summary>
        ///     Get Temperature in DegreesRankine.
        /// </summary>
        public double DegreesRankine => As(TemperatureUnit.DegreeRankine);

        /// <summary>
        ///     Get Temperature in DegreesReaumur.
        /// </summary>
        [Obsolete("This Temperature unit is not yet supported!", true)]
        public double DegreesReaumur;

        /// <summary>
        ///     Get Temperature in DegreesRoemer.
        /// </summary>
        [Obsolete("This Temperature unit is not yet supported!", true)]
        public double DegreesRoemer;

        /// <summary>
        ///     Get Temperature in Kelvins.
        /// </summary>
        public double Kelvins => As(TemperatureUnit.Kelvin);

        /// <summary>
        ///     Get Temperature in SI Unit (Kelvin).
        /// </summary>
        public double SI => As(TemperatureUnit.SI);

        /// <summary>
        ///     Get Temperature in MillidegreesCelsius.
        /// </summary>
        [Obsolete("This Temperature unit is not yet supported!", true)]
        public double MillidegreesCelsius;
        /// <summary>
        ///     Get Temperature in SolarTemperatures.
        /// </summary>
        [Obsolete("This Temperature unit is not yet supported!", true)]
        public double SolarTemperatures;


    }
}

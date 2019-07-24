/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS was created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/headquarters-for-dcs

HQ4DCS is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

HQ4DCS is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with HQ4DCS. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System;
using System.Drawing;

namespace Headquarters4DCS
{
    /// <summary>
    /// Stores a set of X, Y map coordinates as double.
    /// </summary>
    public struct Coordinates : ICloneable
    {
        /// <summary>
        /// Constant "zero" coordinates, with both X and Y set to zero.
        /// </summary>
        public static readonly Coordinates Zero = new Coordinates(0, 0);

        /// <summary>
        /// The X coordinate.
        /// </summary>
        public readonly double X;

        /// <summary>
        /// The Y coordinate.
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public Coordinates(double x, double y) { X = x; Y = y; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">A size structure to create the coordinate from.</param>
        public Coordinates(Size size) { X = size.Width; Y = size.Height; }

        /// <summary>
        /// Constructor. Parses the X and Y coordinates from a string (format is "1.2345,6.7890")
        /// </summary>
        /// <param name="coordinatesString">The string containing the coordinates.</param>
        public Coordinates(string coordinatesString)
        {
            try
            {
                string[] xAndYStrings = coordinatesString.Split(',');

                X = HQTools.StringToDouble(xAndYStrings[0]);
                Y = HQTools.StringToDouble(xAndYStrings[1]);
            }
            catch (Exception)
            {
                X = 0; Y = 0;
            }
        }

        public static Coordinates operator +(Coordinates pt1, Coordinates pt2) { return new Coordinates(pt1.X + pt2.X, pt1.Y + pt2.Y); }
        public static Coordinates operator -(Coordinates pt1, Coordinates pt2) { return new Coordinates(pt1.X - pt2.X, pt1.Y - pt2.Y); }
        public static Coordinates operator *(Coordinates pt, double m) { return new Coordinates(pt.X * m, pt.Y * m); }
        public static Coordinates operator *(Coordinates pt1, Coordinates pt2) { return new Coordinates(pt1.X * pt2.X, pt1.Y * pt2.Y); }
        public static Coordinates operator /(Coordinates pt1, Coordinates pt2) { return new Coordinates(pt1.X / pt2.X, pt1.Y / pt2.Y); }
        public static Coordinates operator /(Coordinates pt, double m) { return new Coordinates(pt.X / m, pt.Y / m); }

        public override string ToString() { return HQTools.ValToString(X) + "," + HQTools.ValToString(Y); }
        public string ToString(string format) { return HQTools.ValToString(X, format) + "," + HQTools.ValToString(Y, format); }

        public Point ToPoint() { return new Point((int)X, (int)Y); }
        public PointF ToPointF() { return new PointF((float)X, (float)Y); }

        public static Coordinates FromAngleInDegrees(double angle) { return FromAngleInRadians(angle * HQTools.DEGREES_TO_RADIANS); }
        public static Coordinates FromAngleInRadians(double angle) { return new Coordinates(Math.Cos(angle), Math.Sin(angle)); }

        public static Coordinates Lerp(Coordinates pt1, Coordinates pt2, double value)
        {
            return new Coordinates(HQTools.Lerp(pt1.X, pt2.X, value), HQTools.Lerp(pt1.Y, pt2.Y, value));
        }

        public static Coordinates CreateRandomInaccuracy(double min, double max) { return CreateRandomInaccuracy(new MinMaxD(min, max)); }
        public static Coordinates CreateRandomInaccuracy(MinMaxD minMax)
        { return FromAngleInDegrees(HQTools.RandomDouble(360.0)) * minMax.GetValue(); }

        /// <summary>
        /// Returns the center of all coordintes passed as parameters.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The center, or 0,0 if no coordinates were passed as parameters.</returns>
        public static Coordinates GetCenter(params Coordinates[] coordinates)
        {
            if ((coordinates == null) || (coordinates.Length == 0)) return new Coordinates();

            Coordinates center = new Coordinates();
            for (int i = 0; i < coordinates.Length; i++) center += coordinates[i];

            center /= coordinates.Length;

            return center;
        }

        /// <summary>
        /// Returns the distance between this set of coordinates and another.
        /// </summary>
        /// <param name="other">The other set of coordinates.</param>
        /// <returns>The distance.</returns>
        public double GetDistanceFrom(Coordinates other) { return Math.Sqrt(GetSquaredDistanceFrom(other)); }

        /// <summary>
        /// Returns the square of the distance between this set of coordinates and another (for quick distance comparison withtout square root).
        /// </summary>
        /// <param name="other">The other set of coordinates.</param>
        /// <returns>The square of the distance.</returns>
        public double GetSquaredDistanceFrom(Coordinates other) { return Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2); }

        /// <summary>
        /// ICloneable implementation.
        /// </summary>
        /// <returns>A new Coordinates structure with the same X and Y values. </returns>
        public object Clone() { return new Coordinates(X, Y); }
    }
}

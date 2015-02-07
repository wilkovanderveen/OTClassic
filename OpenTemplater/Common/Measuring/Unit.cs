using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTemplater.Models;
using OpenTemplater.Models.Layout;

namespace OpenTemplater.Common.Measuring
{
    public class Unit : IComparable<Unit>
    {
        public enum UnitType
        {
            mm,
            pt,
            inch,
            em
        }

        #region private members

        private static float POINT = 1f;
        private static float INCH = 72f;
        private static double MILIMETER = INCH/25.4f;
        private string _unitType;
        private float _points;
        private float _milimeters;
        private float _inches;
        private float _ems;
        private double _unitValue;
        private Relation _relation;
        private bool _isCalculated = false;
        private bool _hasEmValue = false;
        private bool _hasPointValue = false;
        private bool _hasMilimeterValue = false;
        private bool _hasInchValue = false;
        private ResizeOptions _resizeOptions;

        #endregion

        /// <summary>
        /// Boolean which determinates if this unit has an relation with another unit.
        /// </summary>
        public bool HasRelation
        {
            get { return _relation != null; }
        }

        public bool HasEmValue
        {
            get { return _hasEmValue; }
        }

        public bool HasPointValue
        {
            get { return _hasPointValue; }
        }

        public bool HasInchValue
        {
            get { return _hasInchValue; }
        }

        public bool HasMilimeterValue
        {
            get { return _hasMilimeterValue; }
        }

        public Relation Relation
        {
            get { return _relation; }
        }

        public ResizeOptions ResizeOptions
        {
            get { return _resizeOptions; }
        }

        public bool HasResizeOptions
        {
            get { return _resizeOptions != null; }
        }

        /// <summary>
        /// Return the integer value from the points property.
        /// </summary>
        /// <returns></returns>
        public int GetIntegerValue()
        {
            return Convert.ToInt32(Points);
        }

        /// <summary>
        /// The unit value in inches.
        /// </summary>
        public float Inches
        {
            get { return _inches; }
        }

        /// <summary>
        /// The unit value in milimeters.
        /// </summary>
        public float Milimeters
        {
            get { return _milimeters; }
        }

        /// <summary>
        /// The unit value in pica points.
        /// </summary>
        public float Points
        {
            get { return _points; }
        }

        public float Ems
        {
            get { return _ems; }
        }

        public bool IsCalculated
        {
            get { return _isCalculated; }
            set { _isCalculated = value; }
        }

        /// <summary>
        /// Defines an unit object, this object contains all converted values of the input, 
        /// </summary>
        /// <param name="unitValue">possible inputstrings are: #.##mm, #.##inch, #.##pt.</param>
        public Unit(string unitValue)
        {
            System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(
                    @"^(?<Nbr>[\+-]?((\d*\,\d+)|(\d*\.\d+)|\d+))\s*(?<Unit>mm|inch|pt|em)$");
            System.Text.RegularExpressions.Match m = regex.Match(unitValue);

            if (m.Success)
            {
                System.Globalization.NumberFormatInfo numberFormat = new System.Globalization.NumberFormatInfo();
                numberFormat.CurrencyDecimalSeparator = ".";

                _unitValue = System.Convert.ToSingle(m.Groups["Nbr"].Value, numberFormat);
                _unitType = m.Groups["Unit"].Value.ToUpper();

                if (_unitType == "MM")
                {
                    _milimeters = (float) _unitValue;
                    _hasMilimeterValue = true;

                    _points = (float) (_unitValue*MILIMETER);
                    _hasPointValue = true;

                    _inches = _points/INCH;
                    _hasInchValue = true;
                }

                if (_unitType == "INCH")
                {
                    _milimeters = (float) _unitValue*25.4f;
                    _hasMilimeterValue = true;

                    _points = (float) (_unitValue*72);
                    _hasPointValue = true;

                    _inches = (float) _unitValue;
                    _hasInchValue = true;
                }

                if (_unitType == "PT")
                {
                    _points = (float) _unitValue;
                    _hasPointValue = true;

                    _milimeters = (float) (_unitValue/MILIMETER);
                    _hasMilimeterValue = true;

                    _inches = (float) (_unitValue/INCH);
                    _hasInchValue = true;
                }

                if (_unitType == "EM")
                {
                    _ems = (float) _unitValue;
                    _hasEmValue = true;
                }
            }
        }

        public Unit(float pointValue)
        {
            _points = pointValue;
            _milimeters = (float) (pointValue/MILIMETER);
            _inches = pointValue/INCH;
            _unitType = UnitType.pt.ToString();

            _unitValue = _points;
        }

        public Unit() : this(0)
        {
        }

        public Unit(float pointValue, Relation relation, ResizeOptions resizeOptions)
        {
            _points = pointValue;
            _milimeters = (float) (pointValue/MILIMETER);
            _inches = pointValue/INCH;
            _unitType = UnitType.pt.ToString();

            _unitValue = _points;
            _relation = relation;
            _resizeOptions = resizeOptions;
        }

        public Unit(float pointValue, Relation relation)
        {
            _points = pointValue;
            _milimeters = (float) (pointValue/MILIMETER);
            _inches = pointValue/INCH;
            _unitType = UnitType.pt.ToString();

            _unitValue = _points;
            _relation = relation;
        }

        public Unit(string value, Relation relation) : this(new Unit(value).Points, relation)
        {
          
        }

        public override string ToString()
        {
            System.Globalization.NumberFormatInfo numberFormat = new System.Globalization.NumberFormatInfo();
            numberFormat.CurrencyDecimalSeparator = ".";
            return System.Convert.ToString(_unitValue, numberFormat) + _unitType.ToLower();
        }

        /// <summary>
        /// Adds an relation between this and another unit to this element.
        /// </summary>
        /// <param name="relation">The relation to be added.</param>
        public void AddRelation(Relation relation)
        {
            if (relation != null)
            {
                _relation = relation;
            }
        }


        public void AddResizeOptions(ResizeOptions resizeOptions)
        {
            _resizeOptions = resizeOptions;
        }

        #region IComparable<Unit> Members

        public int CompareTo(Unit other)
        {
            return Points.CompareTo(other.Points);
        }
        #endregion

        #region operator overloading
        public static Unit operator + (Unit left, Unit right)
        {
            return new Unit(left.Points + right.Points);
        }

        public static Unit operator -(Unit left, Unit right)
        {
            return new Unit(left.Points - right.Points);
        }

        public static bool operator >(Unit left, Unit right)
        {
            return left.Points > right.Points;
        }

        public static bool operator <(Unit left, Unit right)
        {
            return left.Points < right.Points;
        }

        #endregion
    }
}
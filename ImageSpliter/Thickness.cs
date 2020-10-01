using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSpliter
{

    public struct Thickness : IEquatable<Thickness>
    {
        #region Public Operators

        /// <summary>
        /// Overloaded operator to compare two Thicknesses for equality.
        /// </summary>
        /// <param name="t1">first Thickness to compare</param>
        /// <param name="t2">second Thickness to compare</param>
        /// <returns>True if all sides of the Thickness are equal, false otherwise</returns>

        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return ((t1._left == t2._left)
                && (t1._top == t2._top)
                && (t1._right == t2._right)
                && (t1._bottom == t2._bottom));
        }

        /// <summary>
        /// Overloaded operator to compare two Thicknesses for inequality.
        /// </summary>
        /// <param name="t1">first Thickness to compare</param>
        /// <param name="t2">second Thickness to compare</param>
        /// <returns>False if all sides of the Thickness are equal, true otherwise</returns>
        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return (!(t1 == t2));
        }
        #endregion

        #region Constructors
        public Thickness(int uniformLength)
        {
            _left = _top = _right = _bottom = uniformLength;
        }

        public Thickness(int left, int top, int right, int bottom)
        {
            this._left = left;
            this._top = top;
            this._right = right;
            this._bottom = bottom;
        }
        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj is Thickness)
            {
                Thickness otherObj = (Thickness)obj;
                return (this == otherObj);
            }
            return (false);
        }

        public bool Equals(Thickness thickness)
        {
            return (this == thickness);
        }

        /// <summary>
        /// This function returns a hash code.
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return _left.GetHashCode() ^ _top.GetHashCode() ^ _right.GetHashCode() ^ _bottom.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}", _left, _top, _right, _bottom);
        }
        #endregion

        #region Public Properties

        /// <summary>This property is the Length on the thickness' left side</summary>
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        /// <summary>This property is the Length on the thickness' top side</summary>
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        /// <summary>This property is the Length on the thickness' right side</summary>
        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }

        /// <summary>This property is the Length on the thickness' bottom side</summary>
        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }
        #endregion

        #region Private Fields

        private int _left;
        private int _top;
        private int _right;
        private int _bottom;

        #endregion Private Fields
    }
}

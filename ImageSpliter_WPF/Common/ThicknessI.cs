using System;

namespace ImageSpliter_WPF.Common
{
    public struct ThicknessI : IEquatable<ThicknessI>
    {
        #region Public Operators

        /// <summary>
        /// Overloaded operator to compare two Thicknesses for equality.
        /// </summary>
        /// <param name="t1">first Thickness to compare</param>
        /// <param name="t2">second Thickness to compare</param>
        /// <returns>True if all sides of the Thickness are equal, false otherwise</returns>

        public static bool operator ==(ThicknessI t1, ThicknessI t2)
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
        public static bool operator !=(ThicknessI t1, ThicknessI t2)
        {
            return (!(t1 == t2));
        }
        #endregion

        #region Constructors
        public ThicknessI(int uniformLength)
        {
            _left = _top = _right = _bottom = uniformLength;
        }

        public ThicknessI(int left, int top, int right, int bottom)
        {
            this._left = left;
            this._top = top;
            this._right = right;
            this._bottom = bottom;
        }
        public ThicknessI(string str)
        {
            str = str.Replace(" ", "");

            string[] _splitted_Str = str.Split(',');
            int[] _margin = new int[_splitted_Str.Length];

            // Parse the integer type from a string array.
            // If it cannot be parsed, it returns an unable margin.
            for (int i = 0; i < _splitted_Str.Length; i++)
                if (!int.TryParse(_splitted_Str[i], out _margin[i]))
                {
                    _left   = -1;
                    _top    = -1;
                    _right  = -1;
                    _bottom = -1;
                    return;
                }
                    

            if (_margin.Length == 4)
            {
                _left = _margin[0];
                _top = _margin[1];
                _right = _margin[2];
                _bottom = _margin[3];
                return;
            }
            if (_margin.Length == 2)
            {
                _left = _margin[0];
                _top = _margin[1];
                _right = _margin[0];
                _bottom = _margin[1];
                return;
            }
            if (_margin.Length == 1)
            {
                _left = _margin[0];
                _top = _margin[0];
                _right = _margin[0];
                _bottom = _margin[0];
                return;
            }
            // Can be parsed integer but, can't support length
            _left = -1;
            _top = -1;
            _right = -1;
            _bottom = -1;
        }
        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj is ThicknessI)
            {
                ThicknessI otherObj = (ThicknessI)obj;
                return (this == otherObj);
            }
            return (false);
        }

        public bool Equals(ThicknessI thickness)
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
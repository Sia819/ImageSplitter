using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSplitter.Common
{
    public class RectAnchor
    {
        /// <summary>
        /// (Eng) Independent X-Axis start position coordinates. <para/>
        /// (Kor) 독립적인 X축 시작좌표이며, 변경시 전체에서 X1 혼자만 움직입니다.
        /// </summary> 
        public int AnchorX1
        {
            get => X1;
            set
            {
                Width += X1 - value;
                X1 = value;
            }
        }
        /// <summary>
        /// (Eng) Independent Y-Axis start position coordinates. <para/>
        /// (Kor) 독립적인 Y축 시작좌표이며, 변경시 전체에서 Y1 혼자만 움직입니다.
        /// </summary>
        public int AnchorY1
        {
            get => Y1;
            set
            {
                Height += Y1 - value;
                Y1 = value;
            }
        }
        /// <summary>
        /// (Eng) Independent X-Axis end position coordinates. <para/>
        /// (Kor) 독립적인 X축 종료좌표이며, 변경시 전체에서 X2 혼자만 움직입니다.
        /// </summary>
        public int AnchorX2
        {
            get => X1 + Width;
            set => Width -= X1 + Width - value;
        }

        /// <summary>
        /// (Eng) Independent Y-Axis end position coordinates. <para/>
        /// (Kor) 독립적인 Y축 종료좌표이며, 변경시 전체에서 X2 혼자만 움직입니다.
        /// </summary>
        public int AnchorY2
        {
            get => Y1 + Height;
            set => Height -= Y1 + Height - value;
        }
        /// <summary>
        /// Dependent X-Axis start position coordinates
        /// </summary>
        public int X1 { get; set; }
        /// <summary>
        /// Dependent X-Axis start position coordinates
        /// </summary>
        public int Y1 { get; set; }
        /// <summary>
        /// Area's Width from X-Axis start(X1)
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Area's Height from Y-Axis start(Y1)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectAnchor(int x1, int y1, int width, int height)
        {
            X1 = x1;
            Y1 = y1;
            Width = width;
            Height = height;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(X1, Y1, Width, Height);
        }

        public override string ToString()
        {
            return String.Format("(X1 : {0}, Y1 : {1}, Width : {2}, Height : {3})" + "\n" +
                                 "(AnchorX1 : {4}, AnchorY1 : {5}, AnchorX2 : {6}, AnchorY2 : {7})"
                                 , Y1, Y1, Width, Height, AnchorX1, AnchorY1, AnchorX2, AnchorY2);
        }
    }
}

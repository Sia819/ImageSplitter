using System.Drawing;

namespace ImageSpliter_WPF.Common
{
    public class RootRectAnchor
    {
        // x1
        public int AnchorMinX
        {
            get => PositionX;
            set
            {
                ScaleX += PositionX - value;
                PositionX = value;
            }
        }
        // y1
        public int AnchorMinY
        {
            get => PositionY;
            set
            {
                ScaleY += PositionY - value;
                PositionY = value;
            }
        }
        //사각형의 우측면의 위치
        public int AnchorMaxX
        {
            get => PositionX + ScaleX;
            set => ScaleX -= PositionX + ScaleX - value;
        }
        //사각형의 아래면의 위치
        public int AnchorMaxY
        {
            get => PositionY + ScaleY;
            set => ScaleY -= PositionY + ScaleY - value;
        }

        // 좌측상단모서리의 X
        public int PositionX { get; private set; }
        // 좌측상단모서리의 Y
        public int PositionY { get; private set; }
        // 사각형의너비
        public int ScaleX { get; private set; }
        // 사각형의높이
        public int ScaleY { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        public RootRectAnchor(int positionX, int positionY, int scaleX, int scaleY)
        {
            PositionX = positionX;
            PositionY = positionY;
            ScaleX = scaleX;
            ScaleY = scaleY;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(PositionX, PositionY, ScaleX, ScaleY);
        }

        public override string ToString()
        {
            return $"{PositionX}, {PositionY}, {ScaleX}, {ScaleY}";
        }
    }
}

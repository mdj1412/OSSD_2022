using System.Collections.Generic;
using System.Drawing;

namespace TestProject
{
    /// <summary>
    /// 미로 노드
    /// </summary>
    public class MazeNode
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region Field

        /// <summary>
        /// 북쪽
        /// </summary>
        public const int North = 0;

        /// <summary>
        /// 남쪽
        /// </summary>
        public const int South = North + 1;

        /// <summary>
        /// 동쪽
        /// </summary>
        public const int East = South + 1;

        /// <summary>
        /// 서쪽
        /// </summary>
        public const int West = East + 1;

        /// <summary>
        /// 인접 노드 배열
        /// </summary>
        public MazeNode[] AdjacentNodeArray = new MazeNode[4];

        /// <summary>
        /// 전임 노드
        /// </summary>
        public MazeNode Predecessor = null;

        /// <summary>
        /// 경계 사각형
        /// </summary>
        public RectangleF BoundRectangle;

        /// <summary>
        /// 경로 내 여부
        /// </summary>
        public bool InPath = false;

        /// <summary>
        /// 이웃 리스트
        /// </summary>
        public List<MazeNode> NeighborList = null;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 중심 포인트 - CenterPoint

        /// <summary>
        /// 중심 포인트
        /// </summary>
        public PointF CenterPoint
        {
            get
            {
                float x = BoundRectangle.Left + BoundRectangle.Width  / 2f;
                float y = BoundRectangle.Top  + BoundRectangle.Height / 2f;

                return new PointF(x, y);
            }
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - MazeNode(x, y, width, height)

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        public MazeNode(float x, float y, float width, float height)
        {
            BoundRectangle = new RectangleF(x, y, width, height);
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 이웃 정의하기 - DefineNeighbor()

        /// <summary>
        /// 이웃 정의하기
        /// </summary>
        public void DefineNeighbor()
        {
            NeighborList = new List<MazeNode>();

            foreach(MazeNode node in AdjacentNodeArray)
            {
                if((node != null) && ((node.Predecessor == this) || (node == this.Predecessor)))
                {
                    NeighborList.Add(node);
                }
            }
        }

        #endregion
        #region 경계 사각형 그리기 - DrawBoundRectangle(graphics, pen)

        /// <summary>
        /// 경계 사각형 그리기
        /// </summary>
        /// <param name="graphics">그래픽스</param>
        /// <param name="pen">펜</param>
        public void DrawBoundRectangle(Graphics graphics, Pen pen)
        {
            graphics.DrawRectangle
            (
                pen,
                BoundRectangle.X   + 1,
                BoundRectangle.Y      + 1,
                BoundRectangle.Width  - 2,
                BoundRectangle.Height - 2
            );
        }

        #endregion
        #region 중심 포인트 그리기 - DrawCenterPoint(graphics, brush, radius)

        /// <summary>
        /// 중심 포인트 그리기
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="brush"></param>
        /// <param name="radius"></param>
        public void DrawCenterPoint(Graphics graphics, Brush brush, float radius)
        {
            float centerX = BoundRectangle.Left + BoundRectangle.Width  / 2;
            float centerY = BoundRectangle.Top  + BoundRectangle.Height / 2;

            graphics.FillEllipse(brush, centerX - radius, centerY - radius, 2 * radius, 2 * radius);
        }

        #endregion
        #region 중심 포인트 그리기 - DrawCenterPoint(graphics, brush)

        /// <summary>
        /// 중심 포인트 그리기
        /// </summary>
        /// <param name="graphics">그래픽스</param>
        /// <param name="brush">브러시</param>
        public void DrawCenterPoint(Graphics graphics, Brush brush)
        {
            DrawCenterPoint(graphics, brush, 4);
        }

        #endregion
        #region 전임 링크 그리기 - DrawPredecessorLink(graphics, pen)

        /// <summary>
        /// 전임 링크 그리기
        /// </summary>
        /// <param name="graphics">그래픽스</param>
        /// <param name="pen">펜</param>
        public void DrawPredecessorLink(Graphics graphics, Pen pen)
        {
            if((Predecessor != null) && (Predecessor != this))
            {
                graphics.DrawLine(pen, CenterPoint, Predecessor.CenterPoint);
            }
        }

        #endregion
        #region 이웃 링크 그리기 - DrawNeighborLink(graphics, pen)

        /// <summary>
        /// 이웃 링크 그리기
        /// </summary>
        /// <param name="graphics">그래픽스</param>
        /// <param name="pen">펜</param>
        public void DrawNeighborLink(Graphics graphics, Pen pen)
        {
            foreach(MazeNode neighbor in AdjacentNodeArray)
            {
                if(neighbor != null)
                {
                    int deltaX = (int)(0.4 * (neighbor.CenterPoint.X - CenterPoint.X));
                    int deltaY = (int)(0.4 * (neighbor.CenterPoint.Y - CenterPoint.Y));

                    PointF point = new PointF(CenterPoint.X + deltaX, CenterPoint.Y + deltaY);

                    graphics.DrawLine(pen, CenterPoint, point);
                }
            }
        }

        #endregion
        #region 벽 그리기 - DrawWall(graphics, pen)

        /// <summary>
        /// 벽 그리기
        /// </summary>
        /// <param name="graphics">그래픽스</param>
        /// <param name="pen">펜</param>
        public void DrawWall(Graphics graphics, Pen pen)
        {
            for(int side = 0; side < 4; side++)
            {
                if
                (
                    (AdjacentNodeArray[side] == null) ||
                    ((AdjacentNodeArray[side].Predecessor != this) && (AdjacentNodeArray[side] != this.Predecessor))
                )
                {
                    DrawWall(graphics, pen, side, 0);
                }
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region 벽 그리기 - DrawWall(graphics, pen, side, offset)

        /// <summary>
        /// 벽 그리기
        /// </summary>
        /// <param name="graphics">그래픽스</param>
        /// <param name="pen">펜</param>
        /// <param name="side">면</param>
        /// <param name="offset">오프셋</param>
        private void DrawWall(Graphics graphics, Pen pen, int side, int offset)
        {
            switch(side)
            {
                case North :

                    graphics.DrawLine
                    (
                        pen,
                        BoundRectangle.Left  + offset,
                        BoundRectangle.Top   + offset,
                        BoundRectangle.Right - offset,
                        BoundRectangle.Top   + offset
                    );

                    break;

                case South :

                    graphics.DrawLine
                    (
                        pen,
                        BoundRectangle.Left   + offset,
                        BoundRectangle.Bottom - offset,
                        BoundRectangle.Right  - offset,
                        BoundRectangle.Bottom - offset
                    );

                    break;

                case East :

                    graphics.DrawLine
                    (
                        pen,
                        BoundRectangle.Right  - offset,
                        BoundRectangle.Top    + offset,
                        BoundRectangle.Right  - offset,
                        BoundRectangle.Bottom - offset
                    );

                    break;

                case West :

                    graphics.DrawLine
                    (
                        pen,
                        BoundRectangle.Left   + offset,
                        BoundRectangle.Top    + offset,
                        BoundRectangle.Left   + offset,
                        BoundRectangle.Bottom - offset
                    );

                    break;
            }
        }

        #endregion
    }
}
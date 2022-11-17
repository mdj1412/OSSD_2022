using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;

namespace TestProject
{
    public partial class Child : Form
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        MainForm form2;

        int dd;
        Point st;
        int starty;
        int startx;
        int fps;

        Point epoint;

        /// <summary>
        /// 최소 X
        /// </summary>
        private int minimumX;

        /// <summary>
        /// 최소 Y
        /// </summary>
        private int minimumY;

        /// <summary>
        /// 셀 너비
        /// </summary>
        private int cellWidth;

        /// <summary>
        /// 셀 높이
        /// </summary>
        private int cellHeight;

        /// <summary>
        /// 행 카운트
        /// </summary>
        private int rowCount;

        /// <summary>
        /// 컬럼 카운트
        /// </summary>
        private int columnCount;

        /// <summary>
        /// 노드 배열
        /// </summary>
        private MazeNode[,] nodeArray = null;

        /// <summary>
        /// 시작 노드
        /// </summary>
        private MazeNode startNode = null;

        /// <summary>
        /// 이동 노드
        /// </summary>
        private MazeNode tempNode = null;

        /// <summary>
        /// 종료 노드
        /// </summary>
        private MazeNode endNode = null;

        /// <summary>
        /// 경로 노드 리스트
        /// </summary>
        private List<MazeNode> pathNodeList = null;

        /// <summary>
        /// 마지막 사용 이웃 리스트
        /// </summary>
        private List<int> lastUsedNeighborList = null;

        /// <summary>
        /// 솔루션 발견 여부
        /// </summary>
        private bool solutionFound = false;

        #endregion

        public Child(Bitmap bitmap, MazeNode[,] nodeArray,int fps,int dd,Point st,MazeNode endNode)
        {
            InitializeComponent();
            this.Location=new Point(750,0);
            this.Size = new Size(750, 800);
            this.nodeArray = nodeArray;
            this.pictureBox.Image = bitmap;

            this.cellWidth = this.pictureBox.ClientSize.Width / (this.columnCount + 2);
            this.cellHeight = this.pictureBox.ClientSize.Height / (this.rowCount + 2);

            this.dd = dd;
            this.st = st;
            this.endNode = endNode;

            columnCount = 5;
            rowCount = 15;
            this.fps = fps;
            this.startNode = FindNode(st);
            this.pathNodeList = null;
            this.lastUsedNeighborList = null;

            StartSolving();
            this.pictureBox.Refresh();
        }

        #region 픽처 박스 페인트시 처리하기 - pictureBox_Paint(sender, e)

        /// <summary>
        /// 픽처 박스 페인트시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        /*private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.startNode != null)
            {
                this.startNode.DrawCenterPoint(e.Graphics, Brushes.Blue);
            }

            if (this.endNode != null)
            {
                this.endNode.DrawCenterPoint(e.Graphics, Brushes.Green);
            }

            if ((this.pathNodeList != null) && (this.pathNodeList.Count > 1))
            {
                List<PointF> pointList = new List<PointF>();

                foreach (MazeNode node in this.pathNodeList)
                {
                    pointList.Add(node.CenterPoint);
                }
            }

        }*/

        #endregion

        #region 픽처 박스 페인트시 처리하기 - pictureBox_Paint(sender, e)

        /// <summary>
        /// 픽처 박스 페인트시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.startNode != null)
            {
                this.startNode.DrawCenterPoint(e.Graphics, Brushes.Blue);
            }

            if (this.endNode != null)
            {
                this.endNode.DrawCenterPoint(e.Graphics, Brushes.Green);
            }

            if ((this.pathNodeList != null) && (this.pathNodeList.Count > 1))
            {
                List<PointF> pointList = new List<PointF>();

                foreach (MazeNode node in this.pathNodeList)
                {
                    pointList.Add(node.CenterPoint);
                }

                if ((this.startNode != null) && (this.endNode != null))
                {
                    //StartSolving();
                }

                if (this.solutionFound)
                {
                    e.Graphics.DrawLines(Pens.Red, pointList.ToArray());
                    this.Close();
                }
                else
                {
                    e.Graphics.DrawLines(Pens.Blue, pointList.ToArray());
                }
            }
        }

        #endregion

        #region 노드 찾기 - FindNode(point)

        /// <summary>
        /// 노드 찾기
        /// </summary>
        /// <param name="point">포인트</param>
        /// <returns>노드</returns>
        private MazeNode FindNode(Point point)
        {
            if (point.X < this.minimumX)
            {
                return null;
            }

            if (point.Y < this.minimumY)
            {
                return null;
            }

            int row = (point.Y - this.minimumY) / this.cellHeight;

            if (row >= this.rowCount)
            {
                return null;
            }

            int column = (point.X - this.minimumX) / this.cellWidth;

            if (column >= this.columnCount)
            {
                return null;
            }
            return this.nodeArray[row, column];
        }

        #endregion

        #region 해결 시작하기 - StartSolving()

        /// <summary>
        /// 해결 시작하기
        /// </summary>
        private void StartSolving()
        {
            this.pathNodeList = new List<MazeNode>();

            this.lastUsedNeighborList = new List<int>();

            foreach (MazeNode node in this.nodeArray)
            {
                node.InPath = false;
            }

            foreach (MazeNode node in this.nodeArray)
            {
                node.DefineNeighbor();
            }

            this.pathNodeList.Add(this.startNode);

            this.lastUsedNeighborList.Add(-1);

            this.startNode.InPath = true;

            this.solutionFound = false;

            this.timer.Enabled = true;
        }

        #endregion

        #region 해결하기 - Solve()


        /// <summary>
        /// 해결하기
        /// </summary>
        private void Solve()
        {
            int lastNodeIndex = this.pathNodeList.Count - 1;

            MazeNode lastNode = this.pathNodeList[lastNodeIndex];

            if (lastNode == this.endNode)
            {
                this.solutionFound = true;

                this.timer.Enabled = false;
                form2 = (MainForm)Owner;
                form2.isEnd = true;
                MessageBox.Show("Computer won!");

                return;
            }

            bool neighborFound = false;

            int neighborIndex = this.lastUsedNeighborList[lastNodeIndex];

            MazeNode neighborNode = null;

            for (; ; )
            {
                neighborIndex++;

                if (neighborIndex >= lastNode.NeighborList.Count)
                {
                    break;
                }

                neighborNode = lastNode.NeighborList[neighborIndex];

                if (!neighborNode.InPath)
                {
                    neighborFound = true;

                    this.lastUsedNeighborList[lastNodeIndex] = neighborIndex;

                    break;
                }
            }

            if (neighborFound)
            {
                this.pathNodeList.Add(neighborNode);

                this.lastUsedNeighborList.Add(-1);

                neighborNode.InPath = true;
            }
            else
            {
                lastNode.InPath = false;

                this.pathNodeList.RemoveAt(lastNodeIndex);

                this.lastUsedNeighborList.RemoveAt(lastNodeIndex);
            }
        }

        #endregion

        #region 타이머 틱 처리하기 - timer_Tick(sender, e)

        /// <summary>
        /// 타이머 틱 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = 1000 / fps;

            Solve();

            this.pictureBox.Refresh();
        }

        #endregion
    }
}

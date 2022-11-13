using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace TestProject
{
    /// <summary>
    /// 메인 폼
    /// </summary>
    public partial class MainForm : Form
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        int dd;
        Point st;
        int starty;
        int startx;
        int count = 0;

        Point spoint;
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
        private int rowCount=6;
        
        /// <summary>
        /// 컬럼 카운트
        /// </summary>
        private int columnCount=6;

        /// <summary>
        /// 노드 배열
        /// </summary>
        private MazeNode[,] nodeArray = null;

        /// <summary>
        /// 시작 노드
        /// </summary>
        private MazeNode startNode = null;
        
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

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - MainForm()

        /// <summary>
        /// 생성자
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.createButton.Click    += createButton_Click;

            this.timer.Enabled = false;
            
            //    this.pictureBox.MouseClick += pictureBox_MouseClick;

            this.pictureBox.Paint      += pictureBox_Paint;

        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Private
        //////////////////////////////////////////////////////////////////////////////// Event

        #region 생성하기 버튼 클릭시 처리하기 - createButton_Click(sender, e)

        /// <summary>
        /// 생성하기 버튼 클릭시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void createButton_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.createButton.Enabled = false;
            this.pictureBox.Focus();

            this.startNode = null;

            this.cellWidth  = this.pictureBox.ClientSize.Width  / (this.columnCount + 2);
            this.cellHeight = this.pictureBox.ClientSize.Height / (this.rowCount    + 2);

            if(this.cellWidth > this.cellHeight)
            {
                this.cellWidth = this.cellHeight;
            }
            else
            {
                this.cellHeight = this.cellWidth;
            }

            this.minimumX = (this.pictureBox.ClientSize.Width  - this.columnCount * this.cellWidth ) / 2;
            this.minimumY = (this.pictureBox.ClientSize.Height - this.rowCount    * this.cellHeight) / 2;

            this.nodeArray = GetNodeArray(this.columnCount, this.rowCount);

            this.pathNodeList         = null;
            this.lastUsedNeighborList = null;

            dd = cellHeight / 2;
            starty = minimumY + dd;
            startx = minimumX + dd;

            FindSpanningTree(this.nodeArray[0, 0]);

           
            st = new Point(startx, starty);
            //spoint = new Point(startx, starty);
            epoint = new Point(this.pictureBox.ClientSize.Width - dd - this.minimumX, this.pictureBox.ClientSize.Height-dd-this.minimumY);

            this.startNode = FindNode(st);
            this.endNode = FindNode(epoint);

            DisplayMaze(this.nodeArray);

            /*if ((this.startNode != null) && (this.endNode != null))
            {
                StartSolving();
            }*/

            this.pictureBox.Refresh();

        }

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

            if(this.startNode != null)
            {
                this.startNode.DrawCenterPoint(e.Graphics, Brushes.Red);
            }

            if(this.endNode != null)
            {
                this.endNode.DrawCenterPoint(e.Graphics, Brushes.Green);
            }

            if((this.pathNodeList != null) && (this.pathNodeList.Count > 1))
            {
                List<PointF> pointList = new List<PointF>();

                foreach(MazeNode node in this.pathNodeList)
                {
                    pointList.Add(node.CenterPoint);
                }

                if(this.solutionFound)
                {
                    e.Graphics.DrawLines(Pens.Red, pointList.ToArray());
                }
                else
                {
                    e.Graphics.DrawLines(Pens.Blue, pointList.ToArray());
                }
            }
        }

        #endregion
        #region FPS 스크롤바 스크롤시 처리하기 - fpsScrollBar_Scroll(sender, e)

        /// <summary>
        /// FPS 스크롤바 스크롤시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        /*private void fpsScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int fps = this.fpsScrollBar.Value;

            this.fpsLabel.Text = fps.ToString();

            this.timer.Interval = 1000 / fps;
        }*/

        #endregion

        //////////////////////////////////////////////////////////////////////////////// Function

        #region 노드 배열 구하기 - GetNodeArray(width, height)

        /// <summary>
        /// 노드 배열 구하기
        /// </summary>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <returns>노드 배열</returns>
        private MazeNode[,] GetNodeArray(int width, int height)
        {
            MazeNode[,] nodeArray = new MazeNode[height, width];

            for(int row = 0; row < height; row++)
            {
                int y = this.minimumY + this.cellHeight * row;

                for(int column = 0; column < width; column++)
                {
                    int x = this.minimumX + this.cellWidth * column;

                    nodeArray[row, column] = new MazeNode(x, y, this.cellWidth, this.cellHeight);
                }
            }

            for(int row = 0; row < height; row++)
            {
                for(int column = 0; column < width; column++)
                {
                    if(row > 0)
                    {
                        nodeArray[row, column].AdjacentNodeArray[MazeNode.North] = nodeArray[row - 1, column];
                    }

                    if(row < height - 1)
                    {
                        nodeArray[row, column].AdjacentNodeArray[MazeNode.South] = nodeArray[row + 1, column];
                    }

                    if(column > 0)
                    {
                        nodeArray[row, column].AdjacentNodeArray[MazeNode.West] = nodeArray[row, column - 1];
                    }

                    if(column < width - 1)
                    {
                        nodeArray[row, column].AdjacentNodeArray[MazeNode.East] = nodeArray[row, column + 1];
                    }
                }
            }

            return nodeArray;
        }

        #endregion
        #region 신장 트리 찾기 - FindSpanningTree(rootNode)

        /// <summary>
        /// 신장 트리 찾기
        /// </summary>
        /// <param name="rootNode">루트 노드</param>
        private void FindSpanningTree(MazeNode rootNode)
        {
            Random random = new Random();

            rootNode.Predecessor = rootNode;

            List<MazeLink> linkList = new List<MazeLink>();

            foreach(MazeNode neighborNode in rootNode.AdjacentNodeArray)
            {
                if(neighborNode != null)
                {
                    linkList.Add(new MazeLink(rootNode, neighborNode));
                }
            }

            while(linkList.Count > 0)
            {
                int linkCount = random.Next(0, linkList.Count);

                MazeLink link = linkList[linkCount];

                linkList.RemoveAt(linkCount);

                MazeNode toNode = link.ToNode;

                link.ToNode.Predecessor = link.FromNode;

                for(int i = linkList.Count - 1; i >= 0; i--)
                {
                    if(linkList[i].ToNode.Predecessor != null)
                    {
                        linkList.RemoveAt(i);
                    }
                }

                foreach(MazeNode neighborNode in toNode.AdjacentNodeArray)
                {
                    if((neighborNode != null) && (neighborNode.Predecessor == null))
                    {
                        linkList.Add(new MazeLink(toNode, neighborNode));
                    }
                }
            }
        }

        #endregion
        #region 미로 표시하기 - DisplayMaze(nodeArray)

        /// <summary>
        /// 미로 표시하기
        /// </summary>
        /// <param name="nodeArray">노드 배열</param>
        private void DisplayMaze(MazeNode[,] nodeArray)
        {
            int width  = nodeArray.GetUpperBound(1) + 1;
            int height = nodeArray.GetUpperBound(0) + 1;

            Bitmap bitmap = new Bitmap
            (
                this.pictureBox.ClientSize.Width,
                this.pictureBox.ClientSize.Height
            );

            using(Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        nodeArray[y, x].DrawWall(graphics, Pens.Black);
                    }
                }
            }

            this.pictureBox.Image = bitmap;
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
            if(point.X < this.minimumX)
            {
                startx += dd * 2;
                st = new Point(startx, starty);
                this.startNode = FindNode(st);
                return startNode;
            }

            if(point.Y < this.minimumY)
            {
                starty += dd * 2;
                st = new Point(startx, starty);
                this.startNode = FindNode(st);
                return startNode;
            }

            int row = (point.Y - this.minimumY) / this.cellHeight;

            if(row >= this.rowCount)
            {
                starty -= dd * 2;
                st = new Point(startx, starty);
                this.startNode = FindNode(st);
                return startNode;
            }

            int column = (point.X - this.minimumX) / this.cellWidth;

            if(column >= this.columnCount)
            {
                startx -= dd * 2;
                st = new Point(startx, starty);
                this.startNode = FindNode(st);
                return startNode;
            }

            return this.nodeArray[row, column];
        }

        #endregion
        #region

        private void pictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MazeNode prevNode = this.startNode;
            foreach (MazeNode node in this.nodeArray)
            {
                node.DefineNeighbor();
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (prevNode.isroot[0])
                    {
                        starty -= dd * 2;
                        st = new Point(startx, starty);
                        this.startNode = FindNode(st);
                        this.pictureBox.Refresh();
                        pictureBox.Focus();
                    }
                    break;
                case Keys.Down:
                    if (prevNode.isroot[1])
                    {
                        starty += dd * 2;
                        st = new Point(startx, starty);
                        this.startNode = FindNode(st);
                        this.pictureBox.Refresh();
                        pictureBox.Focus();
                    }
                    break;
                case Keys.Right:
                    if (prevNode.isroot[2]) { 
                        startx += dd * 2;
                        st = new Point(startx, starty);
                        this.startNode = FindNode(st);
                        this.pictureBox.Refresh();
                        pictureBox.Focus();
                    }
                    break;
                case Keys.Left:
                    if (prevNode.isroot[3])
                    {
                        startx -= dd * 2;
                        st = new Point(startx, starty);
                        this.startNode = FindNode(st);


                        this.pictureBox.Refresh();
                        pictureBox.Focus();
                    }
                    break;
            }
            if (this.startNode==this.endNode)
            {
                
                if (count % 3 == 0) {
                    this.createButton.Enabled = true;
                    this.pictureBox.Focus();
                    count += 1;
                    
                }
                else if (count % 3 == 1) {
                    this.rowCount += 2;
                    this.createButton.Enabled = true;
                    this.pictureBox.Focus();
                    count += 1;
                }
                else if (count % 3 == 2)
                {
                    this.columnCount += 2;
                    this.createButton.Enabled = true;
                    this.pictureBox.Focus();
                    count += 1;
                }
            }
            
        }
        #endregion
    }
}


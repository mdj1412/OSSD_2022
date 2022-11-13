namespace TestProject
{
    /// <summary>
    /// 미로 연결
    /// </summary>
    public class MazeLink
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region Field

        /// <summary>
        /// FROM 노드
        /// </summary>
        public MazeNode FromNode;
        
        /// <summary>
        /// TO 노드
        /// </summary>
        public MazeNode ToNode;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - MazeLink(fromNode, toNode)

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="fromNode">FROM 노드</param>
        /// <param name="toNode">TO 노드</param>
        public MazeLink(MazeNode fromNode, MazeNode toNode)
        {
            FromNode = fromNode;
            ToNode   = toNode;
        }

        #endregion
    }
}
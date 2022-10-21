namespace MazeGame
{
    public partial class MazeGame : Form
    {
        static PictureBox[][] array = new PictureBox[40][];

        public MazeGame()
        { 

            for( int i = 0; i < 40; i++) array[i] = new PictureBox[40];


            InitializeComponent();
        }

        private void MazeGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;
                case Keys.Right:
                    break;
                case Keys.Left:
                    break;
            }
        }


        private void MazeGame_Load(object sender, EventArgs e)
        {

            MessageBox.Show("dsadasdas");
        }

    }

}
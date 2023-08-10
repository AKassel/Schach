namespace Schach
{
    public partial class Schach : Form
    {
        public Schach()
        {
            InitializeComponent();
            CreateChessBoard();
            AttachEventHandlersToPanels();
        }

    }
}
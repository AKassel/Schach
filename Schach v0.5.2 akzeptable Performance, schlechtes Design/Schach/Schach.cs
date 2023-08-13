namespace Schach
{
    public partial class Schach : Form
    {
        public Schach()
        {
            this.Load += Schach_Load;
            this.Shown += MainForm_Shown;
            this.bot = bot;
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesPath = Path.Combine(basePath, "..", "..", "..", "..", "..", "Bilder");

            weisserKoenig = Image.FromFile(Path.Combine(imagesPath, "king.png"));
            weisseDame = Image.FromFile(Path.Combine(imagesPath, "Dame.png"));
            weisserLaeufer = Image.FromFile(Path.Combine(imagesPath, "Laeufer.png"));
            weisserSpringer = Image.FromFile(Path.Combine(imagesPath, "Springer.png"));
            weisserTurm = Image.FromFile(Path.Combine(imagesPath, "Turm.png"));
            weisserBauer = Image.FromFile(Path.Combine(imagesPath, "Bauer.png"));

            schwarzerKoenig = Image.FromFile(Path.Combine(imagesPath, "SKing.png"));
            schwarzeDame = Image.FromFile(Path.Combine(imagesPath, "SDame.png"));
            schwarzerLaeufer = Image.FromFile(Path.Combine(imagesPath, "SLaeufer.png"));
            schwarzerSpringer = Image.FromFile(Path.Combine(imagesPath, "SSpringer.png"));
            schwarzerTurm = Image.FromFile(Path.Combine(imagesPath, "STurm.png"));
            schwarzerBauer = Image.FromFile(Path.Combine(imagesPath, "SBauer.png"));
            InitializeComponent();
            CreateChessBoard();
            
            // AttachEventHandlersToPanels();
        }
    }
}
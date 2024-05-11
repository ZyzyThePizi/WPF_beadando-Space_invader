using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; // timer-ért felelős import

namespace space_invader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <value>
        /// Balra és jobra mozgatás logikai változója, kezdetlegesen értelemszerűen false
        /// </value>
        bool goLeft, goRight = false;
        /// <value>
        /// minden alakzatot tárol ami még a képernyőn van, amikor már nincs szükség rá akkor azokat kitöröljük
        /// </value>
        List<Rectangle> itemstoremove = new List<Rectangle>();
        /// <value>
        /// ez fog segíteni abban hogy változtassuk az idegenek képeit
        /// </value>
        int enemyImages = 0;
        /// <value>
        /// ez a lövés időzítő
        /// </value>
        int bulletTimer;
        /// <value>
        /// ez a lövés időzíéshez fontos limit
        /// </value>
        int bulletTimerLimit = 90;
        /// <value>
        /// idegenek számát jelöli
        /// </value>
        int totalEnemeis;
        /// <value>
        /// A játéknak a <c>stoppere</c>, ez alapján (is) dől majd el, hogy győztünk vagy veszítettünk
        /// </value>
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        /// <value>
        /// Ezt hívjuk meg ha különböző képeket akarunk alkalmazni a játékos űrhalyójára
        /// </value>
        ImageBrush playerSkin = new ImageBrush();
        /// <value>
        /// ellenfél sebesség
        /// </value>
        int enemySpeed = 6;

        public MainWindow()
        {
            InitializeComponent();
            // időzítő és események beállítása
            dispatcherTimer.Tick += gameEngine;
            // 20 ms-ként hívjuk meg
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            // timer elindítása
            dispatcherTimer.Start();
            // hajó képének betöltése játékosnak
            playerSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            // hajó hozzá adása az ablak player1 eleméhez
            player1.Fill = playerSkin;
            // makeEnemies futtatása és megmondjuk hogy 30 idegent csináljon
            makeEnemies(30);
        }

            /// <summary>
            /// amikor lenyomjuk gombot fog történni
            /// </summary>
        private void Canvas_KeyisDown(object sender, KeyEventArgs e)
        {
            // bal vagy jobb változó beállítása igazra adott gomb lenyomása esetén
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        {

        }
        /// <summary>
        /// ez felel azért a kódért ami időközönként rá lő a játékosra
        /// </summary>
        private void enemyBulletMaker(double x, double y)
        {
        }
        /// <summary>
        /// létrehozza az ellenségeket
        /// </summary>
        /// <param name="limit">mennyi ellenséget akarunk hozzáadni hátékhoz</param>
        private void makeEnemies(int limit)
        {
        }
        /// <summary>
        /// Ez fogja végezni a mozgást, az ellenségekre való lövést, az űrhajónk <c>animálását</c>
        /// </summary>
        private void gameEngine(object sender, EventArgs e)
        {
        }
    }
}

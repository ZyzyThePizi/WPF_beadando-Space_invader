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
        /// ha elengedjük a gombokat akkor hajtódnak végre az utasítások
        /// </summary>
        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        {
            // ugyan az mint keyDown-nál csak false ra állítjuk
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }

            // ez fogja a lövedékeket létrehozni amint elengedjük a space-t (spammelni kell ezerrel xd)
            if (e.Key == Key.Space)
            {
                // először töröljük fölösleges tárgyakat
                itemstoremove.Clear();
                // létrehozzuk az ablakban megjelenítendő lövedékeket
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                // elhelyezzük a lövedékeket ahol a játékos pocícionál
                Canvas.SetTop(newBullet, Canvas.GetTop(player1) - newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player1) + player1.Width / 2);
                // hozzáadjuk a golyót a képernyőhöz
                myCanvas.Children.Add(newBullet);
            }
        }
        /// <summary>
        /// ez felel azért a kódért ami időközönként rá lő a játékosra
        /// </summary>
        /// <param name="x">x kordináta ahol elhelyezzük a golyót</param>
        /// <param name="y">y kordináta ahol elhelyezzük a golyót</param>
        private void enemyBulletMaker(double x, double y)
        {
           //ugyan úgy létrehozzuk az elhelyezendő golyót (csak most azt ami támadja a játékost)
            Rectangle newEnemyBullet = new Rectangle
            {
                Tag = "enemyBullet",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5
            };
            // golyó helyezkedése
            Canvas.SetTop(newEnemyBullet, y);
            Canvas.SetLeft(newEnemyBullet, x);
            // golyó hozzáadása képernyőhöz
            myCanvas.Children.Add(newEnemyBullet);
        }
        /// <summary>
        /// létrehozza az ellenségeket
        /// </summary>
        /// <param name="limit">mennyi ellenséget akarunk hozzáadni hátékhoz</param>
        private void makeEnemies(int limit)
        {
            int left = 0;
            // elmentjük mennyi ellenség van összesen külön változóba
            totalEnemeis = limit;
            // enemy-t létrehozó ciklus
            for (int i = 0; i < limit; i++)
            {
               // létrehozunk minden ciklussal egy új űrlényt majd az adott képpel
                ImageBrush enemySkin = new ImageBrush();
                Rectangle newEnemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = enemySkin,
                };
                // enemy lokáció
                Canvas.SetTop(newEnemy, 10); 
                Canvas.SetLeft(newEnemy, left); 
                // hozzáadjuk a képernyőhöz
                myCanvas.Children.Add(newEnemy);

                left -= 60;

                // a következő kód részlettel (swicthel bezárólag) azt nézzük meg, hogy melyik képet adjuk hozzá a kreált ellenfélhez

                enemyImages++;
                if (enemyImages > 8)
                {
                    enemyImages = 1;
                }

                switch (enemyImages)
                {
                    case 1:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader1.gif"));
                        break;

                    case 2:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader2.gif"));
                        break;

                    case 3:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader3.gif"));
                        break;

                    case 4:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader4.gif"));
                        break;

                    case 5:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader5.gif"));
                        break;

                    case 6:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader6.gif"));
                        break;

                    case 7:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader7.gif"));
                        break;

                    case 8:
                        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader8.gif"));
                        break;
                }
            }
        }
        /// <summary>
        /// Ez fogja végezni a mozgást, az ellenségekre való lövést, az űrhajónk <c>animálását</c>
        /// </summary>
        private void gameEngine(object sender, EventArgs e)
        {
        }
    }
}

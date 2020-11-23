using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace gameOfLife
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bot> listBot = new List<Bot>();
        Label[,] labels;
        World world;
        bool checkGeneration = false;
        
        public MainWindow()
        {
            InitializeComponent();
           //await Task.Run(() => HelpMePls());


        }
       

        private void downloadWorld_Click(object sender, RoutedEventArgs e)
        {
            //хардкод входного массива мира
            int[,] ggWorld = new int[13, 13]
            {
                { 2,2,2,2,2,2,2,2,2,2,2,2,2 },
                { 2,1,0,0,0,0,0,-1,0,0,0,0,2 },
                { 2,0,0,0,0,0,0,1,0,0,0,0,2 },
                { 2,0,2,0,-1,0,1,0,0,0,-1,0,2 },
                { 2,0,2,0,0,0,0,0,0,0,0,0,2 },
                { 2,0,2,0,0,0,0,0,0-1,0,0,0,2 },
                { 2,0,2,2,1,0,0,0,0,0,0,0,2 },
                { 2,0,0,0,0,0,1,0,0,1,0,0,2 },
                { 2,0,0,0,0,0,0,0,0,0,0,0,2 },
                { 2,0,1,0,0,0,2,2,2,2,0,1,2 },
                { 2,-1,0,0,0,0,1,0,0,0,0,0,2 },
                { 2,0,0,0,0,0,1,0,0,0,0,0,2 },
                { 2,2,2,2,2,2,2,2,2,2,2,2,2 }
               
            };
            //создание экземпляра мира
            world = new World(ggWorld);
            //создание массива для отображения с помощью label
            labels = new Label[world.worldTable.GetLength(0), world.worldTable.GetLength(1)];
            //отбражение начального мира на экране
            createVievGrid();
            spawnBot();
        }
        void createVievGrid() {
            Label label;
            
            for (int i = 0; i < world.worldTable.GetLength(0); i++)
                for (int j = 0; j < world.worldTable.GetLength(1); j++)

                {
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = new GridLength(workSpase.ActualHeight / world.worldTable.GetLength(1), GridUnitType.Pixel);
                    RowDefinition rowDefinition = new RowDefinition();
                    rowDefinition.Height = new GridLength(workSpase.ActualHeight/ world.worldTable.GetLength(1), GridUnitType.Pixel);
                    workSpase.ColumnDefinitions.Add(columnDefinition);

                    workSpase.RowDefinitions.Add(rowDefinition);
                    label = new Label();
                   
                    label.BorderBrush = Brushes.Gray;
                    label.BorderThickness = new Thickness(1);
                    labels[i, j] = label;
                    label.SetValue(Grid.RowProperty, i);
                    label.SetValue(Grid.ColumnProperty, j);
                    workSpase.Children.Add(label);
                }
            
        }
        void spawnBot()
        {
            for (int i = 0; i <6; i++)
                listBot.Add(new Bot(world));
            updateLabel();
        


        }
        public int getBotHealthOfIndex(int x, int y)
        {
            Bot returnBot = null;
            foreach (Bot bot in listBot)
                {
                if (bot.locX == x && bot.locY == y)
                { returnBot = bot;break; }

                }
            return returnBot.health;
        }
        void updateLabel() {
         
            int c = 0;
            for (int i = 0; i < world.worldTable.GetLength(0); i++)
                for (int j = 0; j < world.worldTable.GetLength(0); j++)
                    switch (world.worldTable[i, j])
                    {
                        case -1: { labels[i, j].Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#e81313 ")); labels[i, j].Content = ""; break; }
                        case 2: { labels[i, j].Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#423e3e ")); break; }
                        case 1: { labels[i, j].Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#23b51b")); break; }
                        case 0: { labels[i, j].Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#dcdce4")); labels[i, j].Content = ""; break; }
                        case 3:
                            {
                                c++;
                                labels[i, j].Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2f58c2 ")); 
                                labels[i, j].Content = getBotHealthOfIndex(i, j);
                                labels[i, j].HorizontalContentAlignment = HorizontalAlignment.Center;
                                

                                break;
                            }
                    }
        }



        private void stepBtn_Click(object sender, RoutedEventArgs e)
        {
            checkGeneration = false;
            for (int i=0;i<listBot.Count;i++)
            {
                if (listBot.Count == 2)
                {
                    checkGeneration = true;
                    for (int j = 0; j < 2; j++)
                    {
                        listBot.Add(new Bot(world, listBot[0]));
                        listBot.Add(new Bot(world, listBot[1]));
                    }
                    listBot[0].health = 50;
                    listBot[1].health = 50;
                    break;
                }

               
                   

                listBot[i].do_It();
                if (listBot[i].health < 1)
                { listBot.Remove(listBot[i]); i--; }

            }


           // Task.Run()=> updateLabel();
            updateLabel();
            Console.WriteLine(" ");
          


           

        }

        private void thousandStep_Click(object sender, RoutedEventArgs e)
        {
            while (!checkGeneration)
                stepBtn_Click(stepBtn, null);
            stepBtn_Click(stepBtn, null);

        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

    
    }
}


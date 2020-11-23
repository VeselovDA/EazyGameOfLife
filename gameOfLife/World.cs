using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gameOfLife
{
    class World
    {
        /*
         0-пустая 
         1-еда
         -1-яд
         2-стена
          */
       public int[,] worldTable { get; set; }


        public World(int[,] worldTable)
        {
            if (worldTable.GetLength(0) >= 10 && worldTable.GetLength(1) >= 10)
               
                this.worldTable = worldTable;

            else MessageBox.Show("gg");



        }
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameOfLife
{
    class Bot
    {
        public int health { get; set; }
        public int[] brain = new int[64];
        public int locX { get; set; }
        public int locY { get; set; }
        World world;
        Random random = new Random();
        int pointerComand = 0;
        public int EmptyStep { get; set; }
        public int VenomStep { get; set; }


        public Bot(World w)
        {
            world = w;
            health = 90;
           
            while (true)
            {
                int locX = random.Next(1, world.worldTable.GetLength(0)-1);
                int locY = random.Next(1, world.worldTable.GetLength(1)-1);
                if(world.worldTable[locX, locY]==0)
                {
                    this.locX = locX;
                    this.locY = locY;
                    world.worldTable[locX, locY] = 3;
                    break;
                }

            }
            for (int i=0;i<brain.Length;i++)
            {
              //int c = 
                brain[i] = random.Next(0, 20);
            }
            foreach (int brain in brain)
                switch (brain)
                {
                    case 0: { EmptyStep++; break; }
                    case 1: { EmptyStep++; break; }
                    case 2: { EmptyStep++; break; }
                    case 3: { EmptyStep++; break; }
                    case 4: { VenomStep++; break; }
                    case 5: { VenomStep++; break; }
                    case 6: { VenomStep++; break; }
                    case 7: { VenomStep++; break; }
                }

        }
        public Bot(World w,Bot parentStrong, Bot parent)
        {
            world = w;
            health = 90;

            while (true)
            {
                int locX = random.Next(1, world.worldTable.GetLength(0) - 1);
                int locY = random.Next(1, world.worldTable.GetLength(1) - 1);
                if (world.worldTable[locX, locY]  ==0)
                {
                    this.locX = locX;
                    this.locY = locY;
                    world.worldTable[locX, locY] = 3;
                    break;
                }

            }

            //int c = 
            for (int i = 0; i < brain.Count(); i++)
                if (i % 2==0)
                    brain[i] = parentStrong.brain[i];
                else
                    brain[i] = parent.brain[i];

            foreach (int brain in brain)
                switch (brain)
                {
                    case 0: { EmptyStep++; break; }
                    case 1: { EmptyStep++; break; }
                    case 2: { EmptyStep++; break; }
                    case 3: { EmptyStep++; break; }
                    case 4: { VenomStep++; break; }
                    case 5: { VenomStep++; break; }
                    case 6: { VenomStep++; break; }
                    case 7: { VenomStep++; break; }
                }




        }
        void die (int reason)
        {
            switch(reason)
            {
                case 0: { world.worldTable[locX, locY] = 0; break; }
                case 1: { world.worldTable[locX, locY] = -1; health -= 100;Console.WriteLine("died of poison"); break; }
            }
        }
        public void do_It()
        {

            for (int i = 0; i < 1; i++)
            {
                switch (brain[pointerComand%64])
                {
                    case 0: { doStep("up"); Console.WriteLine("up"); break; }
                    case 1: { doStep("down"); Console.WriteLine("down"); break; }
                    case 2: { doStep("right"); Console.WriteLine("right"); break; }
                    case 3: { doStep("left"); Console.WriteLine("left"); break; }
                    case 4: { if (world.worldTable[locX - 1, locY] == -1) world.worldTable[locX - 1, locY] = 1; health -= 1; doStep("up"); Console.WriteLine("up venom"); break; }
                    case 5: { if (world.worldTable[locX + 1, locY] == -1) world.worldTable[locX + 1, locY] = 1; health -= 1; doStep("down"); Console.WriteLine("down venom"); break; }
                    case 6: { if (world.worldTable[locX, locY + 1] == -1) world.worldTable[locX, locY + 1] = 1; health -= 1; doStep("right"); Console.WriteLine("right venom"); break; }
                    case 7: { if (world.worldTable[locX, locY - 1] == -1) world.worldTable[locX, locY - 1] = 1; health -= 1; doStep("left"); Console.WriteLine("left venom"); break; }
                    case 8: { break; }

                    default: { pointerComand += brain[pointerComand % 64]; break; }
                }
                pointerComand++;
                
            }
            health -= 1;
            if (health > 90)
                health = 90;
            if (health < 1&&health>-5)
                die(0);

        }

        void doStep(string vector)
        {
            switch (vector)
            {
                case "up": {
                       
             
                        switch (world.worldTable[locX - 1, locY])
                        {
                            case 0: { world.worldTable[locX, locY] = 0; locX -= 1; world.worldTable[locX, locY] = 3; break; }
                            case 1: { world.worldTable[locX, locY] = 0; locX -= 1; world.worldTable[locX, locY] = 3; health += 10; break; }
                            case -1: { die(1); break; }
                            case 3: { break; }
                            case 2: { break; }

                        }
                        
                        break; }
                case "down": {

                        switch (world.worldTable[locX + 1, locY])
                        {
                            case 0: { world.worldTable[locX, locY] = 0; locX += 1; world.worldTable[locX, locY] = 3; break; }
                            case 1: { world.worldTable[locX, locY] = 0; locX += 1; world.worldTable[locX, locY] = 3; health += 10; break; }
                            case -1: { die(1); break; }
                            case 3: { break; }
                            case 2: { break; }
                        }

                    
                        break; }
                case "left": {
                        switch (world.worldTable[locX  , locY-1])
                        {
                            case 0: { world.worldTable[locX, locY] = 0; locY -= 1; world.worldTable[locX, locY] = 3; break; }
                            case 1: { world.worldTable[locX, locY] = 0; locY -= 1; world.worldTable[locX, locY] = 3; health += 10; break; }
                            case -1: { die(1); break; }
                            case 3: { break; }
                            case 2: { break; }
                        }

                        
                        break; }
                case "right": {
                        switch (world.worldTable[locX , locY+1])
                        {
                            case 0: { world.worldTable[locX, locY] = 0; locY += 1; world.worldTable[locX, locY] = 3; break; }
                            case 1: { world.worldTable[locX, locY] = 0; locY += 1; world.worldTable[locX, locY] = 3; health += 10; break; }
                            case -1: { die(1); break; }
                            case 3: { break; }
                            case 2: { break; }
                        }

                       

                        break; }
            }
        }



        public void Mutation()
        {
            for (int i = 0; i < brain.Length; i++)
                if (i % 8 == 0)
                    brain[i] = random.Next(0, 20);
            EmptyStep = 0;
            VenomStep = 0;
            foreach (int brain in brain)
                switch (brain)
                {
                    case 0: { EmptyStep++; break; }
                    case 1: { EmptyStep++; break; }
                    case 2: { EmptyStep++; break; }
                    case 3: { EmptyStep++; break; }
                    case 4: { VenomStep++; break; }
                    case 5: { VenomStep++; break; }
                    case 6: { VenomStep++; break; }
                    case 7: { VenomStep++; break; }
                }
        }
    }

}

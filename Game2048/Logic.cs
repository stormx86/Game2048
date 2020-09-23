using System;

namespace Game2048
{
    class Logic
    {
        int size = 4;
        int[,] map;
        DelegateShow show;
        static Random rand = new Random();
        bool moved = false;

        public Logic(int size, DelegateShow show)
        {
            this.size = size;
            map = new int[size, size];
            this.show = show;
        }

        public void init_game()
        { 
        for(int x = 0; x<size; x++)
            for (int y = 0; y < size; y++)
            {
                map[x, y] = 0;
                show(x, y, 0);
            }
        add_numbers();
        add_numbers();
        }
        public void shift_left()
        {
            moved = false;
            for (int y = 0; y < size; y++)
            {
                shift(3, y, -1, 0);
                combine(3, y, -1, 0);
                shift(3, y, -1, 0);
            }
            if (moved)
            add_numbers();
        }

        public void shift_right()
        {
            moved = false;
            for (int y = 0; y < size; y++)
            {
                shift(0, y, 1, 0);
                combine(0, y, 1, 0);
                shift(0, y, 1, 0);
            }
            if (moved)
            add_numbers();
        }

        public void shift_up()
        {
            moved = false;
            for (int x = 0; x < size; x++)
            {
                shift(x, 3, 0, -1);
                combine(x, 3, 0, -1);
                shift(x, 3, 0, -1);
            }
            if (moved)
            add_numbers();
        }

        public void shift_down()
        {
            moved = false;
            for (int x = 0; x < size; x++)
            {
                shift(x, 0, 0, 1);
                combine(x, 0, 0, 1);
                shift(x, 0, 0, 1);
            }
            if (moved)
            add_numbers();
        }


        public bool game_over()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (map[x,y] ==0)
            return false;
            for (int x = 0; x < size - 1; x++)
                for (int y = 0; y < size; y++)
                    if (map[x, y] == map[x + 1, y])
                        return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size-1; y++)
                    if (map[x, y] == map[x, y+1])
                        return false;
                    return true;
        }

        private void add_numbers()
        {
            if (game_over()) return;
            int x, y;
            do
            {
                x = rand.Next(0, size);
                y = rand.Next(0, size);
            } while (map[x, y] > 0);
            map[x, y] = rand.Next(1, 3) * 2;
            show(x, y, map[x, y]);
        }

        private void shift(int x, int y, int sx, int sy)
        {
            if (x + sx < 0 || x + sx >= size || y + sy < 0 || y + sy >= size) return;
            shift(x+sx, y+sy, sx, sy);
            if (map[x+sx, y+sy] ==0 && map[x,y] >0)
            {
            map[x+sx,y+sy] = map[x, y];
            map[x, y] = 0;
            show(x + sx, y + sy, map[x+sx, y+sy]);
            show(x, y, map[x, y]);
            shift(x + sx, y + sy, sx, sy);
            moved = true;
            }
        }

        private void combine(int x, int y, int sx, int sy)
        {
            if (x + sx < 0 || x + sx >= size || y + sy < 0 || y + sy >= size) return;
            combine(x + sx, y + sy, sx, sy);
            if (map[x + sx, y + sy] > 0 && map[x+sx, y+sy] == map[x, y])
            {
                map[x + sx, y + sy] *=2;
                map[x, y] = 0;
                show(x + sx, y + sy, map[x + sx, y + sy]);
                show(x, y, map[x, y]);
                shift(x + sx, y + sy, sx, sy);
                moved = true;
            }
        }
    }
}
 
using System;

namespace nonogram.Core
{
    public class Legend
    {
        private int[,] vertical;
        private int[,] horizontal;
        private int xSizeGrid;
        private int ySizeGrid;
        private int xSizeVertical;
        private int ySizeHorizontal;
        private char[,] chosenImage;

        public Legend(char[,] chosenImage)
        {
            this.chosenImage = chosenImage;
            this.xSizeGrid = chosenImage.GetLength(1);
            this.ySizeGrid = chosenImage.GetLength(0);

            this.xSizeVertical = (xSizeGrid / 2) + 1;
            this.ySizeHorizontal = (ySizeGrid / 2) + 1;

            vertical = new int[ySizeGrid, xSizeVertical];
            horizontal = new int[ySizeHorizontal, xSizeGrid];

            FillLegend();
        }
        public int[,] GenerateVertical()
        {
            int value = 0;
            int x = 0;
            for (int i = 0; i < ySizeGrid; i++)
            {
                for (int j = 0; j < xSizeGrid; j++)
                {
                    if(chosenImage[i,j] == '#') value++;
                    else if(chosenImage[i,j] == '.')
                    {
                        if(value != 0) 
                        {
                            vertical[i,x] = value;
                            x++;
                        }
                        value = 0;
                    }
                }
                if(value != 0) vertical[i,x++] = value;
                value = 0;
                x = 0;
            }
            return vertical;
        }
        public int[,] GenerateHorizontal()
        {
            int value = 0;
            int y = 0;
            for (int i = 0; i < xSizeGrid; i++)
            {
                for (int j = 0; j < ySizeGrid; j++)
                {
                    if(chosenImage[j,i] == '#') value++;
                    else if(chosenImage[j,i] == '.')
                    {
                        if(value != 0) 
                        {
                            horizontal[y,i] = value;
                            y++;
                        }
                        value = 0;
                    }
                }
                if(value != 0) horizontal[y++,i] = value;
                value = 0;
                y = 0;
            }
            return horizontal;
        }

        private void FillLegend()
        {
            for (int i = 0; i < ySizeHorizontal; i++)
            {
                for (int j = 0; j < xSizeGrid; j++)
                {
                    horizontal[i,j] = 0;
                }
            }
            for (int i = 0; i < ySizeGrid; i++)
            {
                for (int j = 0; j < xSizeVertical; j++)
                {
                    vertical[i,j] = 0;
                }
            }
        }

    }
}
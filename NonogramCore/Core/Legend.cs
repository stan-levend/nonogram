using System;

namespace nonogram.Core
{
    public class Legend
    {
        public int[,] vertical { get; private set; }
        public int[,] horizontal { get; private set; }
        private int xSizeGrid;
        private int ySizeGrid;
        private int xSizeVertical;
        private int ySizeHorizontal;
        private char[,] chosenImage;

        public Legend(char[,] chosenImage)
        {
            this.chosenImage = chosenImage;
            xSizeGrid = chosenImage.GetLength(1);
            ySizeGrid = chosenImage.GetLength(0);

            xSizeVertical = (xSizeGrid / 2) + 1;
            ySizeHorizontal = (ySizeGrid / 2) + 1;

            vertical = new int[ySizeGrid, xSizeVertical];
            horizontal = new int[ySizeHorizontal, xSizeGrid];

            FillLegend();
            GenerateVertical();
            GenerateHorizontal();
        }
        private void GenerateVertical()
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
        }
        private void GenerateHorizontal()
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
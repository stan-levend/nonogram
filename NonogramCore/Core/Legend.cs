using System;

namespace nonogram.Core
{
    public class Legend
    {
        public int[,] Vertical { get; private set; }
        public int[,] Horizontal { get; private set; }
        private int xSizeGrid;
        private int ySizeGrid;
        private int xMaxSizeLegend;
        private int yMaxSizeLegend;
        private char[,] chosenImage;

        public Legend(char[,] chosenImage)
        {
            this.chosenImage = chosenImage;
            xSizeGrid = chosenImage.GetLength(1);
            ySizeGrid = chosenImage.GetLength(0);

            xMaxSizeLegend = (xSizeGrid / 2) + 1;
            yMaxSizeLegend = (ySizeGrid / 2) + 1;

            Vertical = new int[ySizeGrid, xMaxSizeLegend];
            Horizontal = new int[yMaxSizeLegend, xSizeGrid];

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
                            Vertical[i,x] = value;
                            x++;
                        }
                        value = 0;
                    }
                }
                if(value != 0) Vertical[i,x++] = value;
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
                            Horizontal[y,i] = value;
                            y++;
                        }
                        value = 0;
                    }
                }
                if(value != 0) Horizontal[y++,i] = value;
                value = 0;
                y = 0;
            }
        }

        private void FillLegend()
        {
            for (int i = 0; i < yMaxSizeLegend; i++)
            {
                for (int j = 0; j < xSizeGrid; j++)
                {
                    Horizontal[i,j] = 0;
                }
            }
            for (int i = 0; i < ySizeGrid; i++)
            {
                for (int j = 0; j < xMaxSizeLegend; j++)
                {
                    Vertical[i,j] = 0;
                }
            }
        }

    }
}
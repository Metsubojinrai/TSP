using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TSP
{
    public partial class Form1 : Form
    {
        public int[,] arr;
        private int numberOfCities;
        private int[,] citiesPositions;
        private double[,] distancesBetweenCities;
        private int citiesPositionsToGuiRatio;
        private Tour globalTour;
        private List<Tour> tourPopulation;
        private List<Tour> tempPopulation;
        private double globalTourLength;
        private CancellationTokenSource gaThreadCancellationToken;
        private int populationSize;
        private int iterations;
        private Random rand;

        private int TOP_SURVIVORS = 50; 
        private const int BOTTOM_SURVIVORS = 0; 
        private int MUTATION_PROBABILITY = 10; 
        private int MAX_MUTATIONS = 2; 
        private const int MAX_CROSSOVER_LEN = 10; 
        private const int duplicatesRemovalInterval = 2;

        public Form1()
        {
            InitializeComponent();
            globalTourLength = Double.MaxValue;
            globalTour = new Tour();
            tourPopulation = new List<Tour>();
            tempPopulation = new List<Tour>();
            rand = new Random();
        }

        private int[,] getData(string filename)
        {
            int[,] kq;
            int row = 0;
            int col = 0;
            string[] lines = File.ReadAllLines(filename);
            row = lines.Length;
            col = lines[0].Split(' ').Length;
            kq = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    kq[i, j] = Int32.Parse(lines[i].Split(' ')[j]);
                }
            }
            return kq;
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            OpenFileDialog op = new OpenFileDialog();
            while(op.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Chọn dataset!");
            }
            arr = getData(op.FileName);
            numberOfCities = arr.GetLength(0);
            citiesPositions = new int[numberOfCities, 2];
            distancesBetweenCities = new double[numberOfCities, numberOfCities];

            for (int i = 0; i < numberOfCities; i++)
            {
                citiesPositions[i, 0] = arr[i, 0];
                citiesPositions[i, 1] = arr[i, 1];
            }

            int x = Int32.MinValue;
            int y = Int32.MinValue;
            citiesPositionsToGuiRatio = 1;
            for (int i = 0; i < numberOfCities; i++)
            {
                if (citiesPositions[i, 0] > x)
                {
                    x = citiesPositions[i, 0];
                }
                if (citiesPositions[i, 1] > y)
                {
                    y = citiesPositions[i, 1];
                }
            }
            int xyMax;
            if (x > y) xyMax = x;
            else xyMax = y;
            citiesPositionsToGuiRatio = xyMax / 500;
            if (xyMax % 500 != 0)
            {
                citiesPositionsToGuiRatio++;
            }
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfCities; i++)
            {
                graphics.DrawEllipse(Pens.Black, citiesPositions[i, 0] / citiesPositionsToGuiRatio - 2, citiesPositions[i, 1] / citiesPositionsToGuiRatio - 2, 4, 4);
            }
            tourDiagram.Image = cityImage;
            for (int i = 0; i < numberOfCities; i++)
            {
                for (int j = 0; j < numberOfCities; j++)
                {
                    distancesBetweenCities[i, j] = Math.Sqrt(
                        Math.Pow(citiesPositions[i, 0] - citiesPositions[j, 0], 2) +
                        Math.Pow(citiesPositions[i, 1] - citiesPositions[j, 1], 2));
                }
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            if (btnStart.Text.Equals("Start"))
            {
                btnStart.Text = "Stop";
                gaThreadCancellationToken = new CancellationTokenSource();
                populationSize = Convert.ToInt32(numPop.Value);
                iterations = Convert.ToInt32(numGeneration.Value);
                MUTATION_PROBABILITY = Convert.ToInt32(numMutate.Value);
                int duplicatesTimer = 0;
                stopwatch.Start();
                Task tMain = Task.Run(() =>
                {
                    globalTour.Clear();
                    tourPopulation.Clear();
                    globalTourLength = Double.MaxValue;
                    Populate();
                    CalculateCostAllToursInPolulation();

                    tourPopulation.Sort();
                    for (int k = 0; k < iterations; k++)
                    {
                        duplicatesTimer++;
                        if (gaThreadCancellationToken.IsCancellationRequested)
                            break;
                        SimulateGeneration();
                        CalculateCostAllToursInPolulation();

                        tourPopulation.Sort();
                        if (globalTourLength > tourPopulation[0].GetDistance())
                        {
                            Tour Changed = findReplacement(tourPopulation[0], globalTour);
                            globalTour = tourPopulation[0];
                            globalTourLength = tourPopulation[0].GetDistance();
                            DrawTour(Changed);
                            drawnTourLengthLabel.BeginInvoke(new MethodInvoker(() =>
                            {
                                drawnTourLengthLabel.Text = "Drawn tour length: " + globalTourLength;
                            }));
                            labelLastSolution.BeginInvoke(new MethodInvoker(() =>
                            {
                                labelLastSolution.Text = "Best solution found @: " + k;
                            }));
                        }
                        iterationLabel.BeginInvoke(new MethodInvoker(() =>
                        {
                            iterationLabel.Text = "Iteration: " + k;
                        }));
                        if (duplicatesTimer > duplicatesRemovalInterval)
                        {
                            duplicatesTimer = 0;
                            Tour Prev = tourPopulation[0];
                            Tour Next;
                            for (int i = 1; i < tourPopulation.Count; i++)
                            {
                                Next = tourPopulation[i];
                                if (Prev.GetDistance() == Next.GetDistance())
                                {
                                    if (Prev.Hash() == Next.Hash())
                                    {
                                        Mutate(tourPopulation[i]);
                                    }
                                    else
                                    {
                                        Prev = Next;
                                    }
                                }
                                else
                                {
                                    Prev = Next;
                                }
                            }
                            CalculateCostAllToursInPolulation();

                            tourPopulation.Sort();
                        }
                    }
                    btnStart.BeginInvoke(new MethodInvoker(() =>
                    {
                        btnStart.Text = "Start";
                    }));
                }, gaThreadCancellationToken.Token);
            }
            else
            {
                gaThreadCancellationToken.Cancel();
            }
        }

        private void DrawTour(Tour whatsNew)
        {
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfCities; i++)
            {
                graphics.DrawEllipse(Pens.Black, citiesPositions[i, 0] / citiesPositionsToGuiRatio - 2, citiesPositions[i, 1] / citiesPositionsToGuiRatio - 2, 4, 4);
            }
            Pen Color = Pens.Black;

            for (int i = 0; i < globalTour.Count - 1; i++)
            {
                if (whatsNew.Count > 0)
                    if (whatsNew.IndexOf(globalTour[i + 1]) != -1)
                    {
                        Color = Pens.Red;
                    }
                    else
                    {
                        Color = Pens.Black;
                    }
                graphics.DrawLine(Color, citiesPositions[globalTour[i], 0] / citiesPositionsToGuiRatio, citiesPositions[globalTour[i], 1] / citiesPositionsToGuiRatio,
                    citiesPositions[globalTour[i + 1], 0] / citiesPositionsToGuiRatio, citiesPositions[globalTour[i + 1], 1] / citiesPositionsToGuiRatio);
            }
            if (whatsNew.Count > 0)
                if (whatsNew.IndexOf(globalTour[0]) != -1)
                {
                    Color = Pens.Red;
                }
                else
                {
                    Color = Pens.Black;
                }
            graphics.DrawLine(Color, citiesPositions[globalTour[0], 0] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[0], 1] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[globalTour.Count - 1], 0] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[globalTour.Count - 1], 1] / citiesPositionsToGuiRatio);
            tourDiagram.BeginInvoke(new MethodInvoker(() =>
            {
                tourDiagram.Image = cityImage;
            }));
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            tourCityListTextBox.Text = "";
            foreach (int i in globalTour)
            {
                tourCityListTextBox.AppendText((i+1).ToString());
                tourCityListTextBox.AppendText(Environment.NewLine);
            }
        }

        private Tour WhatChanged(Tour B)
        {
            Tour C = new Tour();

            for (int i = 0; i < globalTour.Count; i++)
            {
                int next;
                if (i < globalTour.Count - 1)
                {
                    next = globalTour[i + 1];
                }
                else
                {
                    next = globalTour[0];
                }
                int where = B.IndexOf(globalTour[i]);
            }

            return C;
        }

        private int FindPartner()
        {
            int n = tourPopulation.Count;
            int maxRand = (n + 1) * (n + 1);
            int r = rand.Next(1, maxRand);

            return n - (int)Math.Floor(Math.Sqrt(r));
        }

        private void Mutate(Tour Tour)
        {
            int maxRand = numberOfCities;
            for (int i = 0; i < MAX_MUTATIONS; i++)
            {
                int a = rand.Next(maxRand);
                int b = a;
                while (a == b)
                {
                    b = rand.Next(0, maxRand);
                }
                int tmp = Tour[a];
                Tour[a] = Tour[b];
                Tour[b] = tmp;
            }
        }

        private void Crossover(Tour parent1, Tour parent2, Tour child)
        { 
            int unique = 0;
            child.Clear();
            int index = 0;
            int start = rand.Next(0, numberOfCities * 6 / 10);
            int len = rand.Next(numberOfCities * 3 / 10, (numberOfCities - 1 - start));
            List<int> S;
            S = parent1.GetRange(start, len);
            int i = 0;
            while (index < start)
            {
                if (!S.Contains(parent2[i]))
                {
                    child.Add(parent2[i]);
                    index++;
                }
                else
                    unique++;
                i++;
            }
            child.AddRange(S);
            index += len;
            while (index < parent1.Count())
            {
                if (!S.Contains(parent2[i]))
                {
                    unique++;
                    child.Add(parent2[i]);
                    index++;
                }
                else
                    unique++;
                i++;
            }
            if (unique == numberOfCities - len)
                Mutate(child);
        }

        private void Breed(Tour A)
        { 
            int a, b;
            do
            {
                a = FindPartner();
            } while (tourPopulation[a] == A);
            do
            {
                b = FindPartner();
            } while (a == b || tourPopulation[b] == A);
            Crossover(tourPopulation[a], tourPopulation[b], A);
        }

        private void SimulateGeneration()
        { 
            tempPopulation.Clear();
            int FitToCopy = tourPopulation.Count * TOP_SURVIVORS / 100;
            int UnfitToCopy = BOTTOM_SURVIVORS;
            int i;
            for (i = 0; i < FitToCopy; i++)
            { 
                if (i != 0 && rand.Next(1, 100) <= MUTATION_PROBABILITY)
                {
                    Mutate(tourPopulation[i]);
                }
                tempPopulation.Add(tourPopulation[i]);

            }
            for (; i < tourPopulation.Count - UnfitToCopy; i++)
            {
                Breed(tourPopulation[i]);
                tempPopulation.Add(tourPopulation[i]);
            }
            for (; i < tourPopulation.Count; i++)
            {
                if (rand.Next(1, 100) <= MUTATION_PROBABILITY * 2)
                {
                    Tour C = new Tour();
                    C.AddRange(tourPopulation[i].ToArray());
                    Mutate(C);
                    tempPopulation.Add(C);
                }
            }
            tourPopulation.Clear();
            tourPopulation.AddRange(tempPopulation);
        }

        private void Populate() 
        {
            for (int city = 0; city < numberOfCities && city < populationSize; city++)
            {
                Greedy(city);
            }
            for (int city = numberOfCities; city < 2 * numberOfCities && city < populationSize; city++)
            {
                tourPopulation.Add(generateRandomTour());
            }
            CalculateCostAllToursInPolulation();

            tourPopulation.Sort(); 
            while (tourPopulation.Count < populationSize)
            {
                Tour Tour = new Tour();
                Breed(Tour);
                Mutate(Tour);
                tourPopulation.Add(Tour);
            }
        }

        private void CalculateCostAllToursInPolulation()
        {
            foreach (var tour in tourPopulation)
            {
                tour.UpdateDistance(distancesBetweenCities);
            }
        }

        private void Greedy(int firstCity)
        {
            Tour tour = new Tour();
            List<int> notUsedCities = new List<int>();
            double tourLength = 0;
            for (int i = 0; i < numberOfCities; i++)
            {
                notUsedCities.Add(i);
            }

            tour.Add(firstCity);
            notUsedCities.Remove(firstCity);
            while (notUsedCities.Count > 0)
            {
                int actualNodeNumber = tour[tour.Count - 1];
                double minLength = Double.MaxValue;
                int chosenNode = -1;

                foreach (int nodeNumber in notUsedCities)
                {
                    double length = distancesBetweenCities[actualNodeNumber, nodeNumber];
                    if (length < minLength)
                    {
                        minLength = length;
                        chosenNode = nodeNumber;
                    }
                }

                tourLength += minLength;
                tour.Add(chosenNode);
                notUsedCities.Remove(chosenNode);
            }
            tourPopulation.Add(tour);
        }

        private Tour findReplacement(Tour Next, Tour Prev)
        {
            Tour B = new Tour();
            int k, p;
            for (int i = 0; i < Prev.Count; i++)
            {
                k = Next.IndexOf(Prev[i]);
                p = k;
                if (k == numberOfCities - 1)
                {
                    p = 0;
                }
                else
                {
                    p += 1;
                }
                if (i != Next.Count - 1)
                {
                    if (Next[p] != Prev[i + 1])
                    {
                        B.Add(Next[p]);
                    }
                }
                else
                {
                    if (Next[p] != Prev[0])
                    {
                        B.Add(Next[p]);
                    }
                }
            }
            return B;
        }

        private Tour generateRandomTour()
        {
            Tour tour = new Tour();
            List<int> notUsedNodes = new List<int>();
            for (int i = 0; i < numberOfCities; i++)
            {
                notUsedNodes.Add(i);
            }
            for (int i = numberOfCities; i > 0; i--)
            {
                int random = rand.Next(i);
                tour.Add(notUsedNodes[random]);
                notUsedNodes.RemoveAt(random);
            }
            return tour;
        }
    }
}



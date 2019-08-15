using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace backendtakehome
{
    public class Program
    {

        public static void Main(String[] args)
        {
            var dir = Directory.GetCurrentDirectory();
            var Airportfilefolder = @"\data\full\airports.csv";
            var Airportfullpath = dir + Airportfilefolder;
            DataTable Airport = LoadairportFile(Airportfullpath);

            var routestfilefolder = @"\data\full\routes.csv";
            var routesfullpath = dir + routestfilefolder;
            DataTable Routes = LoadroutFile(routesfullpath);
            bool validorign;
            string s;
            string d;
            bool valideatation;

            do
            {
                Console.WriteLine("Please Enter the Origin Airport Code ");
                s = Console.ReadLine().ToUpper();

                validorign = Airport.AsEnumerable().Any(row => s.ToString() == row.Field<string>("IATA 3"));
                if (!validorign)
                {
                    Console.WriteLine("Invalid Origin Airport Code. \n Please Enter a Valid Airport Code");
                }
            } while (!validorign);

            do
            {
                Console.WriteLine("Please Enter the Destation Airport Code ");
                d = Console.ReadLine().ToUpper();

                valideatation = Airport.AsEnumerable().Any(row => d.ToString() == row.Field<string>("IATA 3"));
                if (!valideatation)
                {
                    Console.WriteLine("Invalid Destnation Airport Code. \n Please Enter a Valid Airport Code");
                }
            } while (!valideatation);

            // Create a sample graph
            var numberofroutes = new Int32[Routes.Rows.Count];
            Program g = new Program(numberofroutes.Length);

            for (int i = 0; i < numberofroutes.Length; i++)
            {
                g.addEdge(i, Routes.Rows[i]["Origin"].ToString(), Routes.Rows[i]["Destination"].ToString());
            }

            Console.WriteLine("Finding The Shortest Route From " +
                                 (string.Join(",", s)) + " To " + (string.Join(",", d)));

            g.printAllPaths(s, d);
        }
        // No. of vertices in graph 
        private int v;

        private List<string>[] adjList;
        
        // Constructor 
        public Program(int vertices)
        {
            // initialise vertex count 
            this.v = vertices;

            // initialise adjacency list 
            initAdjList();
        }

        private void initAdjList()
        {
            adjList = new List<string>[v];

            for (int i = 0; i < v; i++)
            {
                adjList[i] = new List<string>();
            }
        }

        // add edge from u to v 
        public void addEdge(int i, string u, string v)
        {
            adjList[i].Add(u);
            adjList[i].Add(v);
        }

        public void printAllPaths(string s, string d)
        {
            string[] isVisited = new string[v];
            List<string> pathList = new List<string>();
            List<string> Destinations = new List<string>();

            printAllPathsUtil(s, d, isVisited, pathList, Destinations);
        }

        private void printAllPathsUtil(string s, string d,
                                        string[] isVisited,
                                List<string> localPathList, List<string> Destinations)
        {
            // Mark the current node 
            for (int i = 0; i < v; i++)
            {
                for(int j = 1; j < 2; j++)
                {
                    if(adjList[i][j].ToString() != s)
                    {
                        isVisited[0] = adjList[i][j].ToString();

                        if (adjList[i][j].ToString().Equals(d))
                        {
                            Destinations.Add(adjList[i][j]);

                            if (adjList[i][0].ToString().Equals(s))
                            {
                                localPathList.Insert(0, adjList[i][1]);
                                localPathList.Insert(0, adjList[i][0]);

                                Console.WriteLine(string.Join(" ", localPathList));
                                Console.Read();
                                v = 0;
                                System.Environment.Exit(-1);
                            }
                            if (!Destinations.Contains(adjList[i][0].ToString()))
                            {
                                localPathList.Insert(0, adjList[i][1]);
                                localPathList.Insert(0, adjList[i][0]);

                                d = adjList[i][0];
                                printAllPathsUtil(s, d, isVisited, localPathList, Destinations);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Sorry! No Routs Found.");
            Console.Read();
        }

        public static DataTable LoadairportFile(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static DataTable LoadroutFile(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
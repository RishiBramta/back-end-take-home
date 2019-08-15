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
    public class Graph
    {
        // No. of vertices in graph 
        private int v;

        // adjacency list 
        private List<string>[] adjList;
        

        // Constructor 
        public Graph(int vertices)
        {
            // initialise vertex count 
            this.v = vertices;

            // initialise adjacency list 
            initAdjList();
        }

        // utility method to initialise 
        // adjacency list 
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
            // Add v to u's list. 
            adjList[i].Add(u);
            adjList[i].Add(v);
        }

        // Prints all paths from 
        // 's' to 'd' 
        public void printAllPaths(string s, string d)
        {
            string[] isVisited = new string[v];
            List<string> pathList = new List<string>();
            List<string> Destinations = new List<string>();

            // Call recursive utility 
            printAllPathsUtil(s, d, isVisited, pathList, Destinations);
        }

        // A recursive function to print 
        // all paths from 'u' to 'd'. 
        // isVisited[] keeps track of 
        // vertices in current path. 
        // localPathList<> stores actual 
        // vertices in the current path 
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
                                // if match found then no need 
                                // to traverse more till depth 
                                v = 0;
                                break;
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

        // Driver code 
        public static void Main(String[] args)
        {
            Console.WriteLine("Please Enter the Origin Airport Code ");
            string s = Console.ReadLine().ToUpper();
            Console.WriteLine("Please Enter the Destation Airport Code ");
            string d = Console.ReadLine().ToUpper();

            DataTable Airport = LoadairportFile(@"C:\Users\Rishi.Bramta\Downloads\data\\full\airports.csv");
            DataTable Routes = LoadroutFile(@"C:\Users\Rishi.Bramta\Downloads\data\\full\routes.csv");

            var validorign = Airport.AsEnumerable().Any(row => s.ToString() == row.Field<string>("IATA 3"));
            if (!validorign)
            {
                Console.WriteLine("Invalid Origin");
            }

            var valideatation = Airport.AsEnumerable().Any(row => d.ToString() == row.Field<string>("IATA 3"));
            if (!valideatation)
            {
                Console.WriteLine("Invalid Destnation");
            }

            // Create a sample graph
            var numberofroutes = new Int32[Routes.Rows.Count];
            Graph g = new Graph(numberofroutes.Length);

            for (int i = 0; i < numberofroutes.Length; i++)
            {
                g.addEdge(i, Routes.Rows[i]["Origin"].ToString(), Routes.Rows[i]["Destination"].ToString());
            }
        
            Console.WriteLine("Following is shortest route from " +
                                 (string.Join(",", s )) + " to "  + (string.Join(",", d )));

            g.printAllPaths(s, d);
        }
    }
}

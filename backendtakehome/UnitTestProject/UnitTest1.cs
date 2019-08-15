using Microsoft.VisualStudio.TestTools.UnitTesting;
using backendtakehome;
using System.Collections.Generic;
using System;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            Program g = new Program(6);

            string s = "YYZ";
            string d = "JFK";

            string[,] adjList = new string[,]
            {
                {"YYZ", "JFK"},
                {"JFK", "YYZ"},
                {"LAX", "YVR"},
                {"YVR", "LAX"},
                {"LAX", "JFK"},
                {"JFK", "LAX"}
            };

            for (int i = 0; i < 5; i++)
            {
                    g.addEdge(i, adjList[i, 0], adjList[i, 1]);
            }

            g.printAllPaths(s, d);
        }
    }
}

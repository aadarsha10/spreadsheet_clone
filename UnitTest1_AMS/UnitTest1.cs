using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2019;
using System.Collections.Generic;

namespace UnitTest1_AMS
{
    [TestClass]
    public class UnitTest1
    {

        //sum test
        [TestMethod]
        
        public void TestSum()
        {
            try
            {
                // arranging 
                IDictionary<string, string> id = new Dictionary<string, string>();
                id.Add("1", "2");
                id.Add("2", "5");
                id.Add("3", "10");
                double expected_ans = 17;
                double actual_ans;
                NumericalOp_AMS no = new NumericalOp_AMS();
                // operation
                actual_ans = no.sum(id);
                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Multiplication
        [TestMethod]
        public void Testmul()
        {
            try
            {
                // arranging 
                IDictionary<string, string> id = new Dictionary<string, string>();
                id.Add("1", "5");
                id.Add("2", "2");
                double expected_ans = 10;
                double actual_ans;
                NumericalOp_AMS no = new NumericalOp_AMS();
                // operation
                actual_ans = no.mul(id);
                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //Division
        [TestMethod]
        public void Testdiv()
        {
            try
            {
                // arranging 
                IDictionary<string, string> id = new Dictionary<string, string>();
                id.Add("1", "6");
                id.Add("2", "2");
                double expected_ans = 0;
                double actual_ans;
                NumericalOp_AMS no = new NumericalOp_AMS();
                // act
                actual_ans = no.div(id);
                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
              catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        [TestMethod]
        public void TestMean()
        {
            try
            {
                // arranging 
                IDictionary<string, string> md = new Dictionary<string, string>();
                md.Add("1", "1");
                md.Add("2", "2");
                md.Add("3", "3");
                md.Add("4", "2");
                double expected_ans = 2;
                double actual_ans;
                NumericalOp_AMS no = new NumericalOp_AMS();

                //operation
                actual_ans = no.Mean(md);

                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Median test
        [TestMethod]
        public void TestMedian()
        {
            try
            {
                // arranging 
                List<double> vs = new List<double>();
                vs.Add(1);
                vs.Add(2);
                vs.Add(3);
                vs.Add(4);
                double expected_ans = 3;
                double actual_ans;
                NumericalOp_AMS no = new NumericalOp_AMS();

                //operation
                actual_ans = no.Median(vs);
                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Mode Test
        [TestMethod]
        public void TestMode()
        {
            try
            {
                // arranging 
                List<double> vs = new List<double>();
                vs.Add(1);
                vs.Add(2);
                vs.Add(2);
                vs.Add(4);
                double expected_ans = 2;
                double actual_ans;

                NumericalOp_AMS no = new NumericalOp_AMS();

                //operation
                actual_ans = no.Mode(vs);
                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //AVerage test
        [TestMethod]
        public void TestAverage()
        {
            try
            {
                // arranging 
                IDictionary<string, string> avg = new Dictionary<string, string>();

                avg.Add("1", "1");
                avg.Add("2", "2");
                avg.Add("3", "3");
                avg.Add("4", "2");
                double expected_ans = 2;
                double actual_ans;
                NumericalOp_AMS no = new NumericalOp_AMS();

                //operation
                actual_ans = no.Average(avg);
                //assert
                Assert.AreEqual(expected_ans, actual_ans);
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }





    }
}

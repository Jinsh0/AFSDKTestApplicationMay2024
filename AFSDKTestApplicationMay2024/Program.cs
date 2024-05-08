using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.Data;

namespace AFSDKTestApplicationMay2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Press any key to continue \n");
            //Console.WriteLine("");
            //Console.ReadKey();

            PISystems myPISystems = new PISystems();
            PISystem myServer = myPISystems["Gary-VM102"];
            myServer.Connect();

            //Console.WriteLine(myServer.Name);
            Console.WriteLine("AF Server {0}, ID: {1}, Version: {2}", myServer.Name, myServer.ID, myServer.ServerVersion);
            //Console.ReadKey();

            //AFDatabase myDB = myServer.Databases["AF Training"];
            AFDatabases myDBs = myServer.Databases;
            AFDatabase myDB = myDBs["AF Training"];
            Console.WriteLine("\nDatabase: {0}, ID: {1}", myDB.Name, myDB.ID);
            //Console.ReadKey();

            Console.WriteLine("\nManual Listing Elements:");
            AFElements myElements = myDB.Elements;
            AFElement myElement0 = myElements[0];
            AFElement myElement1 = myElements[1];
            AFElement myElement2 = myElements[2];
            //Console.WriteLine(myElements); //this will only result in "Elements", and not the actual ElementName
            //Console.WriteLine(myDB.Elements); //this will only result in "Elements", and not the actual ElementName
            Console.WriteLine(myElement0);
            Console.WriteLine(myElement1);
            Console.WriteLine(myElement2);

            Console.WriteLine("\nListing Elements:");
            foreach (var e in myElements)
            {
                Console.WriteLine(e.Name);
            }

            Console.WriteLine("\nPrint Element In A Loop:");
            //while (true)
            //{
            //    Console.WriteLine("Element Name: {0}, Description: {1}", myElement0.Name, myElement0.Description);
            //    Console.ReadKey();
            //    myElement0.Refresh();
            //}

            myElements.Add(new AFElement());
            myDB.ApplyChanges();
            Console.WriteLine("\nDone Applying Changes");
            myDB.CheckIn();
            Console.WriteLine("\nDone Check In");

            AFElement myElementSydney = myElements["Sydney Station"];
            AFAttributes myAttributes = myElementSydney.Attributes;
            AFAttribute myAttribute = myAttributes["Sinusoid"];

            AFTime myTime = new AFTime("t+1h");
            AFValue myAttributeValue1 = myAttribute.GetValue();            
            AFValue myAttributeValue2 = myAttribute.GetValue(myTime);
            Console.WriteLine(myAttributeValue1);
            Console.WriteLine(myAttributeValue2);

            AFData myAttributeData = myAttribute.Data;
            AFValue myAttributeRecordedData = myAttributeData.RecordedValue(myTime, AFRetrievalMode.Auto, null);
            Console.WriteLine(myAttributeRecordedData);

            Console.ReadKey();
        }
    }
}
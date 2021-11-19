using CarLibraryDealerShip;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

namespace CarClientDealerShip
{
    class Program
    {
        //Remember to add the CarLibraryDealerShip DLL as a project reference
        static void Main(string[] args)
        {
            //Writes to the console what is running
            Console.WriteLine("Car Client");

            //Ask the user for the DealerShip properties first
            Console.WriteLine("Type in Dealership Name:");
            string readName = Console.ReadLine();
            Console.WriteLine("Type in Dealership Address:");
            string readAddress = Console.ReadLine();

            //Creates a CarDealerShip using the ProperTies
            CarDealerShip dealership = new CarDealerShip() { Name = readName, Address = readAddress };

            //Asks the user for the 3 properties
            Console.WriteLine("Type in Model:");
            string readModel = Console.ReadLine();
            Console.WriteLine("Type in Color:");
            string readColor = Console.ReadLine();
            Console.WriteLine("Type in Registration Number:");
            string readRegistrationNumber = Console.ReadLine();

            //Creates a 3 Cars using the same properties the user typed for all 3
            //Uses the default empty Constructor, but then initializes the properties
            Car newCar1 = new Car() { Model = readModel, Color = readColor, RegistrationNumber = readRegistrationNumber };
            Car newCar2 = new Car() { Model = readModel, Color = readColor, RegistrationNumber = readRegistrationNumber };
            Car newCar3 = new Car() { Model = readModel, Color = readColor, RegistrationNumber = readRegistrationNumber };

            //Add the 3 cars to the Dealerships list
            dealership.Cars.Add(newCar1);
            dealership.Cars.Add(newCar2);
            dealership.Cars.Add(newCar3);

            //Uses the JsonSerializer to convert the newCar to a JSON string
            string serializedDealerShip = JsonSerializer.Serialize(dealership);

            //Writes the JSON string to the console, so we can see what is being send
            Console.WriteLine(serializedDealerShip);

            //Establishes a connection to the server, in this instance on the localhost on port 10002
            TcpClient socket = new TcpClient("127.0.0.1", 10002);
            //Gets the stream object from the socket. The stream object is able to recieve and send data
            NetworkStream ns = socket.GetStream();

            //No reader is needed, as the server doesn't respond with anything

            //The StreamWriter is an easier way to write data to a Stream, it uses the NetworkStream
            StreamWriter writer = new StreamWriter(ns);

            //writes the JSON version of the newCar to server
            writer.WriteLine(serializedDealerShip);
            //Always remember to flush
            writer.Flush();

            //Single use client, closes the connection afterwards
            socket.Close();
        }
    }
}

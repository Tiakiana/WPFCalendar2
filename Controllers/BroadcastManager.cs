using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using System.Threading;
namespace WPFCalendar.Controllers
{

    public class BroadcastService
    {

        // public const string ServerAddress = "https://jaar.webhotel-itskp.dk/SignalR4Unity";
        public const string ServerAddress = "http://localhost:5000";
        private const string Hub = "/NotificationHub";
        public Dictionary<string, Player> Players = new Dictionary<string, Player>();
        public static BroadcastService inst;
        public List<string> ReceivedMessages = new List<string>();

        private HubConnection _connection;
        public event Action<string> OnMessageReceived;
        public OrderService OrderService;
        MainWindow Main;

        public  BroadcastService(MainWindow main)
        {
            Main= main;
           Start();
        }

       async void Start()
        {
         
            OrderService = new OrderService(this,Main);
            Players.Add("GM",new Player());
            await Initialize();
         //   OnMessageReceived += MessageReceived;
  //          LookForTasksInMessagesCO();
        }
        async Task LookForTasksInMessagesCO()
        {
            while (true)
            {

                if (ReceivedMessages.Count > 0)
                {
                    TakeOrderCO(ReceivedMessages[0]);
                    ReceivedMessages.RemoveAt(0);
                    
                }

                Thread.Sleep(1000);
            }
        }

    

        void MessageReceived(string message)
        {
            try
            {
                var password = message.Split(',')[0];
                var order = message.Split(',')[1];
                if (Players.ContainsKey(password))
                {
                    OrderService.TakeOrder(Players[password], order);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Malformed Message");
            }
            
           // ReceivedMessages.Add(message);
        }



        async Task TakeOrderCO(string message)
        {
            var password = message.Split(',')[0];
            var order = message.Split(',')[1];
            if (Players.ContainsKey(password))
            {
                OrderService.TakeOrder(Players[password], order);
            }
            else
            {
                Console.WriteLine(  "Malformed message:" + message);
            }
        }







        public async Task Initialize()
        {
            _connection = new HubConnectionBuilder().WithUrl(ServerAddress + Hub).Build();
            _connection.On("OnMessageReceived", (string message) =>
            {
                OnMessageReceived?.Invoke(message);
            });
            await _connection.StartAsync();
            await Console.Out.WriteLineAsync(_connection.State.ToString());
        }

        public async Task BroadcastMessage(string message)
        {
            await _connection.SendAsync("BroadcastMessage", message);
        }
    }
    public class Player
    {

    }

    public class OrderService
    {
        BroadcastService BroadcastService;
        MainWindow MainWindow;
        public OrderService(BroadcastService br, MainWindow main)
        {
            BroadcastService = br;
            MainWindow = main;
        }

        internal void TakeOrder(Player player, string order)
        {
            if (order.Contains("RequestEventsList"))
            {
                string sender =  order.Split(';')[1];

                BroadCastStoryPoints(sender);
            }

        }

        public void BroadCastStoryPoints(string receiver)
        {

            BroadcastService.BroadcastMessage(receiver+",ResponseEventsList;"+ MainWindow.StoryController.StringifyStoryPoints());
        }


    }

}

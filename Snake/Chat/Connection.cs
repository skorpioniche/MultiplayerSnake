﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Snake.Chat
{
    // This class handels connections; there will be as many instances of it as there will be connected users
    class Connection
    {
        TcpClient tcpClient;
        // The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string strResponse;

        // The constructor of the class takes in a TCP connection
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            // The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            // The thread calls the AcceptClient() method
            thrSender.Start();
        }

        private void CloseConnection()
        {
            // Close the currently open objects
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        // Occures when a new client is accepted
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());

            // Read the account information from the client
            currUser = srReceiver.ReadLine();

            // We got a response from the client
            if (currUser != "")
            {
                // Store the user name in the hash table
                if (ChatServer.htUsers.Contains(currUser) == true)
                {
                    // 0 means not connected
                    swSender.WriteLine("0|This username already exists.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else if (currUser == "Admin")
                {
                    // 0 means not connected
                    swSender.WriteLine("0|This username is reserved.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else if (PropertiesBlock.GameIsStarted)
                {
                    swSender.WriteLine("0|Game is started. Try later.");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    // 1 means connected successfully
                    int idSnake = PropertiesBlock.snakes.Count;
                    PropertiesBlock.snakes.Add(new PlayerSnake(PropertiesBlock.width, PropertiesBlock.height, PropertiesBlock.size, PropertiesBlock.GetColorByCountSnake(idSnake), PropertiesBlock.gpPalette, 5, currUser));
                    swSender.WriteLine((idSnake + 1).ToString());
                    swSender.Flush();

                    // Add the user to the hash tables and start listening for messages from him
                    ChatServer.AddUser(tcpClient, currUser);

                }
            }
            else
            {
                CloseConnection();
                return;
            }

            try
            {
                // Keep waiting for a message from the user
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    // If it's invalid, remove the user
                    if (strResponse == null)
                    {
                        ChatServer.RemoveUser(tcpClient);
                    }
                    else
                    {
                        // Otherwise send the message to all the other users
                        ChatServer.SendMessage(currUser, strResponse);
                    }
                }
            }
            catch
            {
                // If anything went wrong with this user, disconnect him
                ChatServer.RemoveUser(tcpClient);
            }
        }
    }

}

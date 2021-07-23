using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HTTP
{
    class Program
    {
        enum Menu { ReqList = 1, ReqUnica, Sair }
        static void Main(string[] args)
        {
            bool escolheuSair = false;
            while (escolheuSair == false)
            {
                Console.WriteLine("Sistema de estoque:");
                Console.WriteLine("1-Requisição Listagem\n2-Requisição Única\n3-Sair");
                string opStr = Console.ReadLine();
                int opInt = int.Parse(opStr);

                if (opInt > 0 && opInt < 4)
                {
                    Menu escolha = (Menu)opInt;
                    switch (escolha)
                    {
                        case Menu.ReqList:
                            ReqList();
                            break;
                        case Menu.ReqUnica:
                            ReqUnica();
                            break;
                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }

                }
                else
                {
                    escolheuSair = true;
                }
                Console.Clear();

            }
        }

        static void ReqList()
        {
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/");
            requisicao.Method = "GET";
            var resposta = requisicao.GetResponse();

            using (resposta)
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                List<Tarefa> tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(dados.ToString());
                Console.WriteLine(tarefas);

                foreach (Tarefa tarefa in tarefas)
                {
                    tarefa.Exibir();
                }

                stream.Close();
                resposta.Close();
            }
            Console.ReadLine();
        }

        static void ReqUnica()
        {
            Console.WriteLine("Digite o número da requisição desejada: ");
            string req = Console.ReadLine();
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/" + req);
            requisicao.Method = "GET";
            var resposta = requisicao.GetResponse();

            using (resposta)
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                Tarefa tarefa = JsonConvert.DeserializeObject<Tarefa>(dados.ToString());

                tarefa.Exibir();

                stream.Close();
                resposta.Close();
            }
            Console.ReadLine();
        }
    }
}

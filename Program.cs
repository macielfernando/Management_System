using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace SistemaGestordeClientes
{
    internal class Program
    {
        [System.Serializable]

        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listar = 1, Adicionar = 2, Remover = 3, Sair = 4 }


        static void Main(string[] args)
        {
            Carregar();
            bool opcsair = false;

            while (!opcsair)
            {

                Console.WriteLine("Sistema de clientes - Bem Vindo!");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Sair");
                int intopcao = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intopcao;

                switch (opcao)
                {

                    case Menu.Listar:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        remover();
                        break;
                    case Menu.Sair:
                        opcsair = true;
                        break;

                }
                Console.Clear();
            }

        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro do cliente:");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido! Pressione ENTER para VOLTAR.");
            Console.ReadLine();
        }

        static void remover()
        {
            Listagem();
            Console.WriteLine("Informe a ID do cliente que será removido");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
                Console.WriteLine("Cliente removido com sucesso!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("ID informado é Invalido, tente novamente!");
                Console.ReadLine();
            }

        }


        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);
            stream.Close();

        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);

            try
            {
                
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);


                if(clientes == null)
                {
                    clientes=new List<Cliente>();
                }    

                
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }



        static void Listagem()
           {

            if (clientes.Count > 0)
            {


                Console.WriteLine("Lista de Clientes:");
                int i = 0;
                foreach (Cliente membros in clientes)
                {
                    Console.WriteLine($"ID:{i}");
                    Console.WriteLine($"Nome:{membros.nome}");
                    Console.WriteLine($"Email:{membros.email}");
                    Console.WriteLine($"CPF:{membros.cpf}");
                    Console.WriteLine("=================================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado!");
            }

            Console.WriteLine("Pressione ENTER para VOLTAR");
            Console.ReadLine();

        }

       

        
    } 
}

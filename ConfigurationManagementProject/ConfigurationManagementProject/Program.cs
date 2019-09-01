using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int option = MainMenu();
            CI_Manager myManager = new CI_Manager();

            while (option > 0 && option <= 6)
            {
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        myManager.AddConfigurationItem();
                        Console.ReadKey();
                        option = MainMenu();
                        continue;
                    case 2:
                        Console.Clear();
                        myManager.UpdateConfigurationItem();
                        Console.ReadKey();
                        option = MainMenu();
                        continue;

                    case 3:
                        Console.Clear();
                        myManager.AddDependecy();
                        Console.ReadKey();
                        option = MainMenu();
                        continue;

                    case 4:
                        Console.Clear();
                        myManager.ListConfigurationItem();
                        Console.ReadKey();
                        option = MainMenu();
                        continue;

                    case 5:
                        Console.Clear();
                        myManager.RiskAnalysis();
                        Console.ReadKey();
                        option = MainMenu();
                        continue;

                    case 6:
                        Console.Clear();
                        myManager.DeprecateConfigurationItem();
                        Console.ReadKey();
                        option = MainMenu();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine("Adios!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static int MainMenu()
        {
            Console.Clear();
            int ret = 0;
            Console.WriteLine("###MAIN MENU###");
            Console.WriteLine("1) Agregar un Configuration Item");
            Console.WriteLine("2) Editar Configuration Item");
            Console.WriteLine("3) Agregar Dependencia");
            Console.WriteLine("4) Listar Configuration Item's");
            Console.WriteLine("5) Analisis de Riesgo");
            Console.WriteLine("6) Deprecar Configuration Item");
            Console.WriteLine("7) Salir");
            ret = Convert.ToInt32(Console.ReadLine());

            if(ret <=0 || ret > 7)
            {
                Console.WriteLine("Opcion Invalida!");
                Console.ReadKey();
                ret = MainMenu();
            }
            return ret;
        }
    }
}

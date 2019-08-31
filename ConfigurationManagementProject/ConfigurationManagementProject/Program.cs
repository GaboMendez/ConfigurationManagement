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

            while (option > 0 && option <= 6)
            {
                switch (option)
                {
                    case 1:
                        break;
                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
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
            Console.WriteLine("6) Salir");
            ret = Convert.ToInt32(Console.ReadLine());

            if(ret <=0 || ret > 6)
            {
                Console.WriteLine("Opcion Invalida!");
                Console.ReadKey();
                ret = MainMenu();
            }
            return ret;
        }
    }
}

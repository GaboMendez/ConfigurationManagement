using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementProject
{
    public class CI_Manager
    {
        public void AddConfigurationItem()
        {
            Console.WriteLine("Agregando Nuevo Configuration Item!");
        }

        public void AddDependecy()
        {
            Console.WriteLine("Agregando Nueva Dependencia!");

        }
        public void UpdateConfigurationItem()
        {
            Console.WriteLine("Actualizando Configuration Item!");

        }

        public void ListConfigurationItem()
        {
            Console.WriteLine("Listar Configuration Item's!");

        }

        public void RiskAnalysis()
        {
            Console.WriteLine("Analisis de Configuration Item y Dependencias!");

        }
    }
}

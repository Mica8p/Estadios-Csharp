using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace intento1
{
    
    public class Usuario
    {
        public string NombreUsuario;
        public string Contraseña;

        public Usuario(string nombreUsuario, string constraseña)
        {
            NombreUsuario = nombreUsuario;
            Contraseña = constraseña;
        }
        public virtual void MostrarInformacion()
        {
            Console.WriteLine("Nombre de usuario: " + NombreUsuario);
        }
        public bool VerificarContraseña(string contraseña)//método para ferificar contraseña
        {
            return Contraseña == contraseña;
        }
    }

    public class Cliente : Usuario //clase cliente hereda class Usuario
    {
        public string NumeroCliente;

        public Cliente(string nombreUsuario, string contraseña, string numeroCliente) : base(nombreUsuario, contraseña)
        {
            NumeroCliente = numeroCliente;
        }
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine("Numero de cliente: " + NumeroCliente);
            Console.WriteLine("----------------------------------------------");
        }
    }

    public class Empresario : Usuario //clase empresario hereda clas Usuario
    {
        public string Empresa;
        //constructor
        public Empresario(string nombreUsuario, string constraseña, string empresa): base(nombreUsuario, constraseña)
        {
            Empresa = empresa;
        }
        public override void MostrarInformacion()
        {
            base.MostrarInformacion(); 
            Console.WriteLine("Empresa: " + Empresa);
            Console.WriteLine("-------------------------------------------------");
        }

    }
    //-------------------------clases para venta de entradas---------------------------------------------------------------------
    public class Asiento
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Vendido { get; set; }

        public Asiento(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
            Vendido = false;
        }

        public override string ToString()
        {
            return Vendido ? "[X]" : "[ ]";
        }
    }

    //CLASE BOLETERIA
    public class Boleteria
    {
        private Asiento[,] asientos;
        private int filas;
        private int columnas;
        private bool informacionEntradas;

        public Boleteria(double resultado, bool informacionEntradas)
        {
            if (informacionEntradas)
            {
                // Calcular filas y columnas lo más cuadrado posible
                filas = (int)Math.Sqrt(resultado);
                columnas = (int)Math.Ceiling((double)resultado / filas);
                this.informacionEntradas = informacionEntradas;
                asientos = new Asiento[filas, columnas];

                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        asientos[i, j] = new Asiento(i, j);
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\nAÚN NO ESTA DISPONIBLE LA VENTA DE ENTRADAS");
            }
        }

        public void MostrarAsientos()
        {
            if (informacionEntradas)
            {
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        Console.Write(asientos[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("\n\nAÚN NO ESTA DISPONIBLE LA VENTA DE ENTRADAS");
            }
        }

        public void ComprarAsiento(int asientoNumero)
        {
            // Convertir el número de asiento a fila y columna
            asientoNumero -= 1;
            int fila = asientoNumero / columnas;
            int columna = asientoNumero % columnas;

            if (fila >= 0 && fila < filas && columna >= 0 && columna < columnas)
            {
                if (!asientos[fila, columna].Vendido)
                {
                    asientos[fila, columna].Vendido = true;
                    Console.WriteLine("\n---------------------------------------------------------\n");
                    Console.WriteLine($"     ASIENTO {asientoNumero + 1} - BOLETO COMPRADO CON ÉXITO");
                    Console.WriteLine("\n---------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("\n---------------------------------------------------------\n");
                    Console.WriteLine("        EL ASIENTO INGRESADO YA ESTA VENDIDO");
                    Console.WriteLine("\n---------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("\n---------------------------------------------------------\n");
                Console.WriteLine("                  ASIENTO NO VÁLIDO");
                Console.WriteLine("\n---------------------------------------------------------");
            }
        }
    }//FIN CLASE BOLETERIA

    //----------------------------------------------------------------------------------------------

    internal class Program
    {
        //CLIENTES
        // Bandera para verificar que haya venta de entradas
        static bool informacionEntradas = false;
        //FUNCION MENU BOLETERIA
        
        public static void MenuClientes()//menu clientes
        {
           
            string titulo = "                         MENU CLIENTES";
            int opcion;
            string[] opciones = new string[] { "1)Informacion del concierto", "2)Venta de entradas", "3)Salir" };
            int n = 3; //cantidad de elementos del vector opciones

            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(titulo);
                Console.WriteLine("-----------------------------------------------------------------\n");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(opciones[i]);
                }
                Console.Write("\nOpcion: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("-----------------------------------------------------------------");
                        Console.WriteLine("                   INFORMACIÓN DEL CONCIERTO");
                        Console.WriteLine("-----------------------------------------------------------------");
                        MostrarInformacionConcierto();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("-----------------------------------------------------------------");
                        Console.WriteLine("                          BOLETERIA");
                        Console.WriteLine("-----------------------------------------------------------------");
                        MenuBoleteria();
                        break;
                    case 3:
                        Console.Clear();
                        exit = true;
                        Console.WriteLine("Saliendo.....");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("ERROR......No existe la opción, vuelva ingresar otra opción!");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (!exit);
        }
        public static void MenuBoleteria()
        {
            string titulo = "BOLETERIA";
            int opcion;
            string[] opciones = new string[] { "1)Comprar boleto", "2)Salir" };
            int n = 2; // cantidad de elementos del vector opciones

            Boleteria boleteria = new Boleteria(resultado, informacionEntradas);

            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine(titulo);
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("                ASIENTOS DISPONIBLES");
                Console.WriteLine("--------------------------------------------------------\n");
                boleteria.MostrarAsientos();
                Console.WriteLine("\n------------------------VENTA-------------------------\n");

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(opciones[i]);
                }
                Console.Write("\nOpcion: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        if (informacionEntradas)
                        {
                            Console.WriteLine("---------------------------------------------------------");
                            Console.WriteLine("                  ASIENTOS DISPONIBLES");
                            Console.WriteLine("---------------------------------------------------------\n");
                            boleteria.MostrarAsientos();
                            Console.WriteLine("\n-------------------ELIJA UN ASIENTO----------------------\n");
                            Console.Write("Ingrese el número del asiento a comprar: ");
                            int asientoNumero;
                            if (int.TryParse(Console.ReadLine(), out asientoNumero))
                            {
                                boleteria.ComprarAsiento(asientoNumero);
                            }
                            else
                            {
                                Console.WriteLine("-----------------------------------------------------------------");
                                Console.WriteLine("                Número de asiento inválido.......");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\nAÚN NO ESTA DISPONIBLE LA VENTA DE ENTRADAS");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        exit = true;
                        Console.WriteLine("\nSaliendo...");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("ERROR... No existe la opción, vuelva a ingresar otra opción!");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (!exit);
        }
        public static void GestionCliente()//para iniciar sesion y poder ingresar al menu clientes
        {
            Usuario[] usu = new Usuario[]
            {
                new Cliente("cliente1", "password2", "C001")
            };//vector con usuarios ejemplos para clientes

            Console.WriteLine("                   INICIAR SESIÓN");
            Console.WriteLine("----------------------------------------------------------------\n");
            Console.Write("Nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contraseña = Console.ReadLine();

            Usuario usuarioLogueado = Login(usu, nombreUsuario, contraseña);

            if (usuarioLogueado != null)//si las credenciales son correctas se accede a la informacion cliente
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("               Inicio exitoso.");
                Console.WriteLine("-------------------------------------------------");
                usuarioLogueado.MostrarInformacion();
                Console.WriteLine("\nINGRESANDO........");
                Console.ReadKey();
                MenuClientes();
            }
            else
            {
                Console.WriteLine("\nNombre de usuario o contraseña incorrectos");
            }
        }

        //ADMINISTRATIVO <<<>>>>><<<<<>>>><<<<<<<<<<<>>>>>>>>>

        // Variable estática para almacenar el resultado de la capacidad
        static double resultado;

        // Bandera para verificar si la información del concierto fue agregada
        static bool informacionAgregada = false;

        // Variable estática para almacenar la información del concierto
        static InfoCon texto;

        public struct Capac
        {
            public int metrosCuadrados;
            public int personasEnTribuna;
            public double porcentajeDeEscenario;
        }
        public struct Entradas1
        {
            public int precioEntradasVIP;
            public int precioEntradasCOMUNES;
        }
        public struct Fecha
        {
            public int dia;
            public int mes;
            public int año;
        }
        public struct InfoCon
        {

            public Fecha fechaDelEvento;
            public string infoDelArtista;
            public string ubicación;
            public string PoliticaDeEvent;
        }
        //función para almacenar la información del concierto
        public static void InformacionEvento()// menu administrativo (opcion 3)
        {
            
            Console.Write("Nombre de la banda o artista que se presenta: ");
            texto.infoDelArtista = Console.ReadLine();

            Console.Write("\nUbicación del estadio: ");
            texto.ubicación = Console.ReadLine();

            Console.Write("\nFecha del evento\n");
            Console.Write("Dia: ");
            texto.fechaDelEvento.dia = Convert.ToInt32(Console.ReadLine());
            Console.Write("Mes: ");
            texto.fechaDelEvento.mes = Convert.ToInt32(Console.ReadLine());
            Console.Write("Año: ");
            texto.fechaDelEvento.año = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nPolitica del evento: ");
            texto.PoliticaDeEvent = Console.ReadLine();

            // Marcar que la información del concierto ha sido agregada
            informacionAgregada = true;

            Console.WriteLine("\nInformación del concierto agregada correctamente.");
        }
        // Función para mostrar la información del concierto
        public static void MostrarInformacionConcierto() //se muestra en el menu clientes opción 1
        {
            if (informacionAgregada)
            {
                Console.WriteLine("\nArtista: " + texto.infoDelArtista);
                Console.WriteLine("\nUbicación: " + texto.ubicación);
                Console.WriteLine("\nFecha del evento: " + texto.fechaDelEvento.dia + "/" + texto.fechaDelEvento.mes + "/" + texto.fechaDelEvento.año);
                Console.WriteLine("\n\nPolítica del evento: " + texto.PoliticaDeEvent);
            }
            else
            {
                Console.WriteLine("No hay conciertos disponibles.");
            }
        }

        public static double CalculoCapacidad(Capac capac)
        {
            //Calcular la capacidad total del estadio

            double espacioDisponible = capac.metrosCuadrados * 0.8; // descontamos 20% de las tribunas
            double espacioEscenario = espacioDisponible * (capac.porcentajeDeEscenario / 100); //m2 que ocupa el escenario
            double espacioSinEscenario = espacioDisponible - espacioEscenario; // restamos m2 de esc

            double capacidadEstadio = espacioSinEscenario * 4;
            return capacidadEstadio + capac.personasEnTribuna;

        }

        public static void Capacidad() //menu administrativo (opción 1)
        {
            Capac capac = new Capac();
            //Ingreso de datos
            Console.Write("Ingrese la cantidad de metros cuandrados que tiene el estadio: ");
            capac.metrosCuadrados = Convert.ToInt32(Console.ReadLine());

            Console.Write("Cantidad de personas que caben en las tribunas: ");
            capac.personasEnTribuna = Convert.ToInt32(Console.ReadLine());

            Console.Write("Porcentaje que ocupa de espacio el escenario: ");
            capac.porcentajeDeEscenario = Convert.ToInt32(Console.ReadLine());

            resultado = CalculoCapacidad(capac);
            informacionEntradas = true;
            //Mostrar resultados
            Console.WriteLine("\nResultados ");
            Console.WriteLine("\nCapacidad total del estadio(personas que caben en el estadio): " + Math.Round(resultado));

        }
        public static void Entradas()//menu administrativo (opción 2)
        {
            //Entradas
            Entradas1 entr = new Entradas1();
            Console.WriteLine("\nCantidad de personas que caben en el estadio(entradas a imprimir): " + Math.Round(resultado));
            Console.Write("\nEstablecer el precio de las entradas VIP: $");
            entr.precioEntradasVIP = Convert.ToInt32(Console.ReadLine());

            Console.Write("Establecer el precio de las entradas COMUNES: $");
            entr.precioEntradasCOMUNES = Convert.ToInt32(Console.ReadLine());

            //Calcular la cantidad de entradas comunes y VIP
            double entradasVIP = resultado * 0.30;
            double entradasComunes = resultado * 0.7;

            //calcular Recaudacion de conciertos

            double recaudacionConcierto = (entradasVIP * entr.precioEntradasVIP) + (entradasComunes * entr.precioEntradasCOMUNES);

            //Mostrar resultados
            Console.WriteLine("\nResultados ");

            Console.WriteLine("\nEntradas VIP disponibles(30%): " + Math.Round(entradasVIP));
            Console.WriteLine("Entradas comunes disponibles(70%): " + Math.Round(entradasComunes));

            Console.WriteLine("\nRecaudacion si vende todas las entradas: $ " + recaudacionConcierto);
        }

        public static void MenuAdministrativo()//menu administrativo
        {
            string titulo = "                  MENU ADMINISTRATIVO";
            int opcion;
            string[] opciones = new string[] { "1)Capacidad Estadio", "2)Gestión de entradas", "3)Agregar información del evento", "4)Salir" };
            int n = 4; //cantidad de elementos del vector opciones

            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(titulo);
                Console.WriteLine("-----------------------------------------------------------------\n");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(opciones[i]);
                }

                Console.WriteLine("\nOpcion: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("                          CAPACIDAD ESTADIO");
                        Console.WriteLine("------------------------------------------------------------------------\n");
                        Capacidad();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("                             GESTIÓN ENTRADAS");
                        Console.WriteLine("-----------------------------------------------------------------------------------\n");

                        if (resultado != 0)
                        {
                            Entradas();
                        }
                        else
                        {
                            Console.WriteLine("Error... PRIMERO DEBE GUARDAR LOS DATOS DE LA CAPACIDAD DEL ESTADIO\nPARA SABER CUANTAS ENTRADAS HAY QUE IMPRIMIR");
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("                         AGREGAR INFORMACIÓN DEL CONCIERTO");
                        Console.WriteLine("-----------------------------------------------------------------------------------");
                        InformacionEvento();
                        break;
                    case 4:
                        Console.Clear();
                        exit = true;
                        Console.WriteLine("\nSALIENDO.....");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nERROR......No existe la opción, vuelva ingresar otra opción!");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (!exit);
        }
        public static void GestionAdministrativa() //para iniciar sesion y poder ingresar al menu administrativo
        {
            Usuario[] usu = new Usuario[]
            {
                new Empresario("empresario1", "password2", "Empresa ABC")
            };//vector con usuarios ejemplos para empresario

            Console.WriteLine("                    INICIAR SESIÓN");
            Console.WriteLine("---------------------------------------------------------------\n");
            Console.Write("Nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contraseña = Console.ReadLine();

            Usuario usuarioLogueado = Login(usu, nombreUsuario, contraseña);
            
            if(usuarioLogueado != null)//si las credenciales son correctas se accede a la informacion del empresario
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("               Inicio exitoso.");
                Console.WriteLine("-------------------------------------------------");
                usuarioLogueado.MostrarInformacion();
                Console.WriteLine("\nINGRESANDO........");
                Console.ReadKey();
                MenuAdministrativo();
            }
            else
            {
                Console.WriteLine("\nNombre de usuario o contraseña incorrectos");
            }
        }

        //Funcion para verificar credenciales
        public static Usuario Login(Usuario[] usu,  string nombreUsuario, string contraseña)
        {
            for(int i = 0; i < usu.Length; i++)
            {
                if (usu[i].NombreUsuario == nombreUsuario && usu[i].VerificarContraseña(contraseña))
                {
                    return usu[i];
                }
            }
            return null;
        }
        
        public static void MenuPrincipal()
        {
            
            string titulo = "             MENU PRINCIPAL";
            int opcion;
            string[] opciones = new string[] { "1)Gestión clientes", "2)Gestión Administrativa", "3)Salir" };
            int n = 3; //cantidad de elementos del vector opciones

            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine(titulo);
                Console.WriteLine("------------------------------------------------\n");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(opciones[i]);
                }
                Console.Write("\nOpcion: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("GESTIÓN CLIENTES");
                        Console.WriteLine("---------------------------------------------------------------");
                        GestionCliente();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("GESTIÓN ADMINISTRATIVA");
                        Console.WriteLine("---------------------------------------------------------------");
                        GestionAdministrativa();
                        break;
                    case 3:
                        Console.Clear();
                        exit = true;
                        Console.WriteLine("\nSALIENDO.....\n");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nERROR......No existe la opción, vuelva ingresar otra opción!");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (!exit);
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            MenuPrincipal();
            Console.ReadKey();
        }
    }
}

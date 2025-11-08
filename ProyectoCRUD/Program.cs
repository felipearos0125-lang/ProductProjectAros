using ProductosCRUD;

namespace ProyectoCRUD
{
    internal static class Program
    {
        public static string Token { get; set; } = string.Empty;
        public static string NombreUsuario { get; set; } = string.Empty;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Mostrar formulario de login primero
            var loginForm = new FormLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Si el login es exitoso, guardar el token y abrir el formulario principal
                Token = loginForm.Token;
                NombreUsuario = loginForm.NombreUsuario;
                
                Application.Run(new formProductos());
            }
            else
            {
                // Si se cancela el login, cerrar la aplicación
                Application.Exit();
            }
        }
    }
}
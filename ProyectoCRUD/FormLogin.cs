using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;

namespace ProyectoCRUD
{
    public partial class FormLogin : Form
    {
        private TextBox txtUsuario;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegistrar;
        private Label lblUsuario;
        private Label lblPassword;
        private Label lblTitulo;
        private CheckBox chkMostrarPassword;

        public string Token { get; private set; }
        public string NombreUsuario { get; private set; }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Login - Sistema de Productos";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título
            lblTitulo = new Label
            {
                Text = "Iniciar Sesión",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(100, 20),
                Size = new System.Drawing.Size(200, 30),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            // Label Usuario
            lblUsuario = new Label
            {
                Text = "Usuario:",
                Location = new System.Drawing.Point(50, 70),
                Size = new System.Drawing.Size(80, 20)
            };

            // TextBox Usuario
            txtUsuario = new TextBox
            {
                Location = new System.Drawing.Point(50, 95),
                Size = new System.Drawing.Size(300, 25),
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            // Label Password
            lblPassword = new Label
            {
                Text = "Contraseña:",
                Location = new System.Drawing.Point(50, 130),
                Size = new System.Drawing.Size(80, 20)
            };

            // TextBox Password
            txtPassword = new TextBox
            {
                Location = new System.Drawing.Point(50, 155),
                Size = new System.Drawing.Size(300, 25),
                PasswordChar = '●',
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            // CheckBox Mostrar Password
            chkMostrarPassword = new CheckBox
            {
                Text = "Mostrar contraseña",
                Location = new System.Drawing.Point(50, 185),
                Size = new System.Drawing.Size(150, 20)
            };
            chkMostrarPassword.CheckedChanged += ChkMostrarPassword_CheckedChanged;

            // Botón Login (centrado, sin color de fondo)
            btnLogin = new Button
            {
                Text = "Iniciar Sesión",
                Location = new System.Drawing.Point(130, 220),
                Size = new System.Drawing.Size(140, 35),
                FlatStyle = FlatStyle.Standard,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            btnLogin.Click += BtnLogin_Click;

            // Agregar controles al formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(chkMostrarPassword);
            this.Controls.Add(btnLogin);

            // Enter en los textbox para login
            txtUsuario.KeyPress += (s, e) => { if (e.KeyChar == (char)13) txtPassword.Focus(); };
            txtPassword.KeyPress += (s, e) => { if (e.KeyChar == (char)13) BtnLogin_Click(s, e); };
        }

        private void ChkMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkMostrarPassword.Checked ? '\0' : '●';
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Por favor ingrese el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor ingrese la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Iniciando sesión...";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5290");

                    var loginData = new
                    {
                        nombreUsuario = txtUsuario.Text,
                        password = txtPassword.Text
                    };

                    var response = await client.PostAsJsonAsync("/api/auth/login", loginData);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                        
                        Token = result.Token;
                        NombreUsuario = result.NombreUsuario;

                        MessageBox.Show($"¡Bienvenido {NombreUsuario}!", "Login exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error de login\n\nCódigo: {response.StatusCode}\n\nDetalle: {error}", 
                            "Error de login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se pudo conectar con el servidor.\n\nAsegúrate de que la API esté ejecutándose en http://localhost:5290", 
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Iniciar Sesión";
            }
        }

        private class LoginResponse
        {
            public int Id { get; set; }
            public string NombreUsuario { get; set; }
            public string Token { get; set; }
            public DateTime Expiracion { get; set; }
        }
    }
}

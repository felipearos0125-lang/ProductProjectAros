namespace ProductosCRUD
{
    partial class formProductos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblTotal = new Label();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            txtPrecio = new TextBox();
            txtStock = new TextBox();
            txtBuscar = new TextBox();
            btnAgregar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnBuscar = new Button();
            dgvProductos = new DataGridView();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 35);
            label1.Name = "label1";
            label1.Size = new Size(51, 13);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 75);
            label2.Name = "label2";
            label2.Size = new Size(70, 13);
            label2.TabIndex = 1;
            label2.Text = "Descripción:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 115);
            label3.Name = "label3";
            label3.Size = new Size(41, 13);
            label3.TabIndex = 2;
            label3.Text = "Precio:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(350, 115);
            label4.Name = "label4";
            label4.Size = new Size(38, 13);
            label4.TabIndex = 3;
            label4.Text = "Stock:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 30);
            label5.Name = "label5";
            label5.Size = new Size(44, 13);
            label5.TabIndex = 4;
            label5.Text = "Buscar:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(20, 505);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(123, 15);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "Total de productos: 0";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(120, 32);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(400, 21);
            txtNombre.TabIndex = 6;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(120, 72);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(400, 30);
            txtDescripcion.TabIndex = 7;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(120, 112);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(200, 21);
            txtPrecio.TabIndex = 8;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(420, 112);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(100, 21);
            txtStock.TabIndex = 9;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(120, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(400, 21);
            txtBuscar.TabIndex = 10;
            // 
            // btnAgregar
            // 
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Location = new Point(553, 30);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(73, 23);
            btnAgregar.TabIndex = 11;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Location = new Point(553, 59);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(73, 23);
            btnActualizar.TabIndex = 12;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Location = new Point(553, 88);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(73, 23);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Location = new Point(553, 117);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(73, 23);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Location = new Point(553, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(73, 26);
            btnBuscar.TabIndex = 15;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(20, 252);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.RowTemplate.Height = 24;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(655, 240);
            dgvProductos.TabIndex = 16;
            dgvProductos.CellClick += dgvProductos_CellClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtDescripcion);
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(txtPrecio);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(txtStock);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Location = new Point(20, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(655, 160);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Producto";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtBuscar);
            groupBox2.Controls.Add(btnBuscar);
            groupBox2.Location = new Point(20, 187);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(655, 59);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Búsqueda";
            // 
            // formProductos
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(687, 531);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(dgvProductos);
            Controls.Add(lblTotal);
            Font = new Font("Segoe UI", 7.8F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "formProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Productos - CRUD";
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
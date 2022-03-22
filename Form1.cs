using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace _2020_ZALICZENIE
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();


        }
        Doktor model = new Doktor();

        void Clear()
        {
            txtCity.Text = txtName.Text = txtSpecialization.Text = "";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            model.City = txtCity.Text.Trim();
            model.Name = txtName.Text.Trim();
            model.Specialization = txtSpecialization.Text.Trim();
            using (HospitalDBEntities db = new HospitalDBEntities())
            {
                if (string.IsNullOrWhiteSpace(txtCity.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSpecialization.Text))
                {
                    MessageBox.Show("Pola nie mogą być puste!");
                }
                else
                {
                    if (model.Id == 0)
                    {
                        db.Doktors.Add(model);
                    }
                    else
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    Clear();
                    MessageBox.Show("Działanie pomyślne!");
                    gridDoctors.DataSource = db.Doktors.ToList();
                    btnDelete.Enabled = false;
                    btnAdd.Text = "Dodaj Lekarza";
                }
            }

        }

        

        private void btnLoad_Click(object sender, EventArgs e)
        {
            HospitalDBEntities db = new HospitalDBEntities();
            gridDoctors.DataSource = db.Doktors.ToList();
            this.gridDoctors.Columns[0].Visible = false;
        }

        private void gridDoctors_DoubleClick(object sender, EventArgs e)
        {
            if (gridDoctors.CurrentRow.Index != -1)
            {
                model.Id = Convert.ToInt32(gridDoctors.CurrentRow.Cells["Id"].Value);
                using (HospitalDBEntities db = new HospitalDBEntities())
                {
                    model = db.Doktors.Where(x => x.Id == model.Id).FirstOrDefault();
                    txtCity.Text = model.City;
                    txtName.Text = model.Name;
                    txtSpecialization.Text = model.Specialization;
                }
            }
            btnAdd.Text = "Aktualizuj";
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Jesteś pewien że chcesz usunąć lekarza z listy?","Usuwanko",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (HospitalDBEntities db = new HospitalDBEntities())
                {
                    var entry = db.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        db.Doktors.Attach(model);
                    db.Doktors.Remove(model);
                    db.SaveChanges();
                    Clear();
                    MessageBox.Show("Usunięto pomyślnie!");
                    gridDoctors.DataSource = db.Doktors.ToList();
                    btnAdd.Text = "Dodaj Lekarza";
                    btnDelete.Enabled = false;
                }
            }
        }

        public delegate int numery(int n1, int n2);
        public int numerki(int a, int b)
        {
            return a + b;
        }
        private void Form_Load(object sender, EventArgs e)
        { 
            numery n = new numery(numerki);
            int wynik = n(2, 3);
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Red, 3);
            Rectangle a = new Rectangle(20, 20, 20, 20);
            Graphics g = e.Graphics;
            g.DrawEllipse(p, a);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

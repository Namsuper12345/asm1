﻿using BusinessLayer.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asm1
{
    public partial class Task4 : Form
    {
        public Task4()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;   
        }
        IMemberRepository fun= new MemberRepository();
        private void Task4_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<Member> list = fun.GetAllMember();
            foreach (Member m in list)
            {
                dataGridView1.Rows.Add(m.MemberId,m.Email,m.CompanyName,m.City,m.Country,m.Password);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Column4"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["Column5"].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells["Column6"].Value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Member m = new Member();
            m.MemberId = int.Parse(textBox1.Text);
            m.Email = textBox2.Text;
            m.CompanyName = textBox3.Text;
            m.City = textBox4.Text;
            m.Country = textBox5.Text;
            m.Password= textBox6.Text;

            bool up = fun.UpdateMember(m);
            if(up== true)
            {
                MessageBox.Show("Update thanh cong");

            }
            else
            {
                MessageBox.Show("Update that bai");
            }
            Task4_Load(sender,e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new frmCreateForm(null)
            {
                MemberRepository = fun
            };
            form.ShowDialog();
            Task4_Load(sender, e);
        }

        private void txtSearchByID_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                    {
                        e.Handled = true;
                    }
                }

        private void btnSearchByID_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int ID;
            try
            {
                ID = int.Parse(txtSearchByID.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Number First!");
                return;
            }
            Member mem = fun.GetMemberById(ID);
            dataGridView1.Rows.Add(mem.MemberId, mem.Email, mem.CompanyName, mem.City, mem.Country, mem.Password);
        }

        private void btnSearchByEmail_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            String email = txtSearchByEmail.Text;
            List<Member> list = fun.GetMembersByEmail(email);
            foreach (Member m in list)
            {
                dataGridView1.Rows.Add(m.MemberId, m.Email, m.CompanyName, m.City, m.Country, m.Password);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index;
            try
            {
                 index = int.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Select Columns First!");
                return;
            }
            fun.DeleteMember(index);
            Task4_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task4_Load(sender, e);
        }
    }
}

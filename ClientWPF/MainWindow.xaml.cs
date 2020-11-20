using ClientWPF.Model;
using MahApps.Metro.Controls;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System;
using System.Security.Policy;
using System.Collections.Generic;
using Domain;
using Newtonsoft.Json;
using RestSharp;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Config conf = new Config() { WebApi = "http://danielbisleri-001-site1.gtempurl.com//api/ClientConnection" };
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void btConfig_Click(object sender, RoutedEventArgs e)
        {
            var result = await DialogManager.ShowInputAsync(this,"Configuração de Endereço","Atual:"+ conf.WebApi + Environment.NewLine+"Qual o Novo Endereço de API?");
            conf.WebApi = result;
        }

        private void btEnviar_Click(object sender, RoutedEventArgs e)
        {
            List<ClienteLinks> cliLink = new List<ClienteLinks>();
            List<ClienteAnexos> cliAnexo = new List<ClienteAnexos>();
            List<ClienteConexoes> cliConexao = new List<ClienteConexoes>();
            Clientes cliente = new Clientes();

            cliente.nome = textBoxCliente.Text;

            cliLink.Add(new ClienteLinks()
            {
                link = textBoxLink.Text,
                senha = textBoxPass.Text,
                usuario = textBoxUser.Text,
                nome = textBoxNome.Text
            });


            cliente.clientelinks = cliLink;

            cliente.clienteconexao = cliConexao;

            cliente.clienteanexos = cliAnexo;

            var json = JsonConvert.SerializeObject(cliente);

            var client = new RestClient(conf.WebApi);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(json);


            IRestResponse response = client.Execute(request);

            var messsageSetting = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK"
            };


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DialogManager.ShowMessageAsync(this, "Enviando", "Cliente Enviado", settings: messsageSetting);
            }
            else
            {
                DialogManager.ShowMessageAsync(this, "Erro", "Cliente Nào Enviado Enviado", settings: messsageSetting);
            }

            

        }
    }
}

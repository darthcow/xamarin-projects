using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetAddressFromCep.Model;
using Xamarin.Forms;
using GetAddressFromCep.Service;
using Xamarin.Essentials;

namespace GetAddressFromCep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            textResult.GestureRecognizers.Add(item: new TapGestureRecognizer(view => onAddressClicked()));
        }


        private void onAddressClicked()
        {
            Clipboard.SetTextAsync(textResult.Text);
            DisplayAlert("", "Endereço copiado!", "Ok");
        }

        private Address address;

        //change validation later to use Data Annotations
        private bool isValidCep(string cep)
        {
            var newCep = 0;
            var isValid = true;

            if (cep.Length != 8)
            {
                isValid = false;
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter 8 dígitos.", "OK");
            }
            else
            {
                if (!int.TryParse(cep, out newCep))
                {
                    isValid = false;
                    DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto por apenas números", "OK");
                }
            }


            return isValid;
        }

        private void ButtonSearchCep_Clicked(object sender, EventArgs e)
        {
            //used replace for remvoving the mask char and trim to remove any possible white-spaces
            var cep = entryCep.Text.Replace("-", "").Trim();
            if (isValidCep(cep))
            {
                try
                {
                    address = ViaCepService.searchAddress(cep);
                    if (address.erro==true)
                    {
                        //called entryCep to show masked text
                        textResult.Text =
                            string.Format("Endereço não encontrado para o CEP: {0}", entryCep.Text.Trim());
                    }
                    else
                    {
                        textResult.Text =
                            "Logradouro: " + address.logradouro +
                            "\nBairro: " + address.bairro +
                            "\nCidade: " + address.localidade +
                            "\nUF: " + address.uf;
                    }
                }
                catch (Exception exception)
                {
                    DisplayAlert("ERRO CRÍTICO", exception.Message, "OK");
                }

            }
        }
    }
}
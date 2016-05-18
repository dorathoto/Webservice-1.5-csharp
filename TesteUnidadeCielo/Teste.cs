using Cielo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TesteUnidadeCielo
{
    [TestClass]
    public class Teste
    {
        [TestMethod]
        public void TestarTransacao()
        {
            var cielo = new Cielo.Cielo(ConfigApp.mid, ConfigApp.key, ConfigApp.UrlCieloEcommerce);

            var holder = cielo.holder("4012001038443335", "2018", "05", "123");
            holder.name = "Fulano Portador da Silva";

            var randomOrder = new Random();

            var order = cielo.order(randomOrder.Next(1000, 10000).ToString(), 10000);
            var paymentMethod = cielo.paymentMethod(PaymentMethod.VISA, PaymentMethod.CREDITO_A_VISTA);

            var transaction = cielo.transactionRequest(
                holder,
                order,
                paymentMethod,
                "http://localhost/cielo",
                Transaction.AuthorizationMethod.AUTHORIZE_WITHOUT_AUTHENTICATION,
                false
            );

            var codigoLr = transaction.authorization.lr;

            Assert.AreEqual(codigoLr, "00");
        }

        [TestMethod]
        public void TestarTransacaoComToken()
        {
            var cielo = new Cielo.Cielo(ConfigApp.mid, ConfigApp.key, ConfigApp.UrlCieloEcommerce);

            var holder = cielo.holder("4012001038443335", "2018", "05", "123");
            holder.name = "Fulano Portador da Silva";

            var randomOrder = new Random();

            var order = cielo.order(randomOrder.Next(1000, 10000).ToString(), 10000);
            var paymentMethod = cielo.paymentMethod(PaymentMethod.VISA, PaymentMethod.CREDITO_A_VISTA);

            var transaction = cielo.transactionRequest(
                holder,
                order,
                paymentMethod,
                "http://localhost/cielo",
                Transaction.AuthorizationMethod.AUTHORIZE_WITHOUT_AUTHENTICATION,
                false
            );

            var Token = transaction.token;

            Assert.AreEqual(Token.code, "!#$!#SFDA@#$@");
        }

        [TestMethod]
        public void TestarCancelarTransacao()
        {
            var cielo = new Cielo.Cielo(ConfigApp.mid, ConfigApp.key, ConfigApp.UrlCieloEcommerce);

            var tid = "1100000000$A5E";
            var valorCancelar = 0; //0 igual cancelamento total

            var transacao = cielo.cancellationRequest(tid, valorCancelar);

            Assert.AreEqual(transacao, null);
        }
    }
}
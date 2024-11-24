using Coupon.Core.Entities.Client;
using Coupon.Core.Entities.Reason;

namespace Coupon.Core.Entities.Coupon
{
    public class Descount : EntityBase
    {
        public ClientType TipoDesconto { get; init; }
        public double PercentDescount { get; private set; }
        public virtual Description Description { get; init; }
        public Guid DescriptionId { get; init; }

        private void HasDescription()
        {
            if (Description == null)
                throw new InvalidOperationException("Informe a razao da modificação");
        }

        public void UpdateDescount(double percentDescount)
        {
            if (percentDescount < 0 || percentDescount > 0.15)
                throw new InvalidOperationException("Valor desconto inválido");

            HasDescription();

            percentDescount = PercentDescount;
        }
        public static class Factories
        {
            public static Descount Create(double percent, ClientType client)
            {
                 if(percent < 0 || !Enum.IsDefined(typeof(ClientType), client))
                    throw new InvalidOperationException("Cupom inválido");

                return new Descount
                {
                    PercentDescount = percent,
                    TipoDesconto = client,
                };
            }
        }

    }
}

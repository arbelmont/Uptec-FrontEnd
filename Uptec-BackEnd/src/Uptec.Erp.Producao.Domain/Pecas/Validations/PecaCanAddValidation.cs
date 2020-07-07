using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Producao.Domain.Pecas.Specifications;

namespace Uptec.Erp.Producao.Domain.Pecas.Validations
{
    public class PecaCanAddValidation : DomainValidator<Peca>
    {
        public PecaCanAddValidation(IPecaRepository pecaRepository)
        {
            Add(new PecaCodigoUnicoSpec(pecaRepository, DomainOperation.Add), "Código já cadastrado." );
        }
    }
}
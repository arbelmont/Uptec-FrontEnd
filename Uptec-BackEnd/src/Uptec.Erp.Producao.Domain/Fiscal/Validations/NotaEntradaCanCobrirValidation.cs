using Definitiva.Shared.Domain.DomainValidator;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaCanCobrirValidation : DomainValidator<NotaEntrada>
    {
        public NotaEntradaCanCobrirValidation(NotaEntrada notaCliente, List<Lote> lotes)
        {
            foreach (var item in notaCliente.Itens)
            {
                Add(new NotaEntradaItensCoincidentesSpec(item), $"Item: {item.Codigo}-{item.Descricao}, n�o coincidente entre as notas.");
            }

            foreach (var lote in lotes)
            {
                Add(new NotaEntradaLoteEhCoberturaSpec(lote), $"Lote {lote.LoteNumero} n�o pode receber cobertura.");
            }

            Add(new NotaEntradaLoteExistsSpec(lotes), "Lotes n�o encontrados.");
            
            Add(new NotaEntradaTipoEmissorConciliaSpec(notaCliente), "Nota de Cliente n�o identificada, verifique o tipo de emissor.");
        }
    }
}
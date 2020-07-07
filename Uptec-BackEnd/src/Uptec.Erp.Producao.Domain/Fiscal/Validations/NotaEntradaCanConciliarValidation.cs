using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaCanConciliarValidation : DomainValidator<NotaEntrada>
    {
        private IClienteRepository _clienteRepository;
        private IFornecedorRepository _fornecedorRepository;

        public NotaEntradaCanConciliarValidation(
            NotaEntrada nota,
            IClienteRepository clienteRepository,
            IFornecedorRepository fornecedorRepository,
            IComponenteRepository componenteRepository,
            IPecaRepository pecaRepository)
        {
            _clienteRepository = clienteRepository;
            _fornecedorRepository = fornecedorRepository;

            Cliente cliente = null;
            Fornecedor fornecedor = null;

            if (nota.TipoEmissor == TipoEmissor.Cliente)
            {
                cliente = _clienteRepository.GetByCnpj(nota.CnpjEmissor.Numero);
                Add(new NotaEntradaClienteExistsSpec(cliente), "Cliente n�o cadastrado.");
            }
            else if(nota.TipoEmissor == TipoEmissor.Fornecedor)
            {
                fornecedor = _fornecedorRepository.GetByCnpj(nota.CnpjEmissor.Numero);
                Add(new NotaEntradaFornecedorExistsSpec(fornecedor), "Fornecedor n�o cadastrado.");
            }

            Add(new NotaEntradaTipoEmissorSpec(), "Tipo Emissor indefinido.");
            Add(new NotaEntradaHasItensSpec(), "Itens da nota n�o identificados.");

            if(nota.TipoEstoque == TipoEstoque.Peca)
            {
                foreach (var item in nota.Itens)
                {
                    if (nota.TipoEmissor == TipoEmissor.Cliente)
                    {
                        Add(new NotaEntradaClientePecaExistsSpec(pecaRepository, item), $"Pe�a: {item.Descricao} - C�digo: {item.Codigo}, n�o cadastrada no sistema para esse cliente.");
                        Add(new NotaEntradaClientePecaUnidadeCompativelSpec(pecaRepository, item), $"Pe�a: {item.Descricao} - C�digo: {item.Codigo}, unidade de medida incompat�vel.");
                    }
                    else
                    {
                        Add(new NotaEntradaFornecedorPecaExistsSpec(pecaRepository, item, fornecedor), $"Pe�a: {item.Descricao} - C�digo: {item.Codigo}, n�o cadastrada no sistema para esse fornecedor.");
                        Add(new NotaEntradaFornecedorPecaUnidadeCompativelSpec(pecaRepository, item, fornecedor), $"Pe�a: {item.Descricao} - C�digo: {item.Codigo}, unidade de medida incompat�vel.");
                    }
                }
            }
            else
            {
                foreach (var item in nota.Itens)
                {
                    Add(new NotaEntradaComponenteExistsSpec(componenteRepository, item), $"Mat�ria-Prima: {item.Descricao} - C�digo: {item.Codigo}, n�o cadastrada no sistema.");
                    Add(new NotaEntradaComponenteUnidadeCompativelSpec(componenteRepository, item), $"Mat�ria-Prima: {item.Descricao} - C�digo: {item.Codigo}, unidade de medida incompat�vel.");
                }
            }

        }
    }
}
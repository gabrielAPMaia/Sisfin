using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;

namespace Negocio
{
    public class FornecedorService
    {
        private FornecedorRepository _repository;

        public FornecedorService()
        {
            _repository = new FornecedorRepository();
        }

        public string Update(int? id, TipoPessoa tipoPessoa, string nome, string email, string cpf_cnpj, string razao_social, string rua, string numero, string bairro, string cidade, string complemento, string cep, string telefone, string celular)
        {
            // Validações
            string mensagemValidacao = ValidaFornecedor(tipoPessoa, nome, email, cpf_cnpj, razao_social, rua, numero, bairro, cidade, complemento, cep, telefone, celular);
            if (!string.IsNullOrEmpty(mensagemValidacao))
            {
                return mensagemValidacao;
            }

            // Verificar se o email já está cadastrado
            if (EmailRegistrado(email, id))
            {
                return "O email já está cadastrado.";
            }

            var fornecedor = new Fornecedor
            {
                Id = id,
                tipoPessoa = tipoPessoa,
                Nome = nome,
                Email = email,
                Cpf_cnpj = cpf_cnpj,
                Razao_social = razao_social,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Cidade = cidade,
                Completemento = complemento,
                Cep = cep,
                Telefone = telefone,
                Celular = celular,
            };

            if (id == null)
                return _repository.Insert(fornecedor);
            else
                return _repository.Update(fornecedor);
        }

        public string Insert(Fornecedor fornecedor)
        {
            // Validações
            string mensagemValidacao = ValidaFornecedor(fornecedor.tipoPessoa, fornecedor.Nome, fornecedor.Email, fornecedor.Cpf_cnpj, fornecedor.Razao_social, fornecedor.Rua, fornecedor.Numero, fornecedor.Bairro, fornecedor.Cidade, fornecedor.Completemento, fornecedor.Cep, fornecedor.Telefone, fornecedor.Celular);
            if (!string.IsNullOrEmpty(mensagemValidacao))
            {
                return mensagemValidacao;
            }

            // Verificar se o email já está cadastrado
            if (EmailRegistrado(fornecedor.Email, fornecedor.Id))
            {
                return "O email já está cadastrado.";
            }

            return _repository.Insert(fornecedor);
        }

        public string Remove(int idFornecedor)
        {
            return _repository.Remove(idFornecedor);
        }

        public DataTable getAll()
        {
            return _repository.getAll();
        }

        public DataTable filterByName(string nome)
        {
            return _repository.filterByName(nome);
        }

        private string ValidaFornecedor(TipoPessoa tipoPessoa, string nome, string email, string cpf_cnpj, string razao_social, string rua, string numero, string bairro, string cidade, string complemento, string cep, string telefone, string celular)
        {
            if (string.IsNullOrEmpty(nome))
                return "O nome é obrigatório.";

            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                return "Email inválido.";

            if (string.IsNullOrEmpty(cpf_cnpj))
                return "CPF/CNPJ é obrigatório.";

            if (tipoPessoa == TipoPessoa.PESSOA_JURIDICA && string.IsNullOrEmpty(razao_social))
                return "Razão social é obrigatória para pessoa jurídica.";

            if (string.IsNullOrEmpty(rua))
                return "Rua é obrigatória.";

            if (string.IsNullOrEmpty(numero))
                return "Número é obrigatória.";

            if (string.IsNullOrEmpty(bairro))
                return "Bairro é obrigatório.";

            if (string.IsNullOrEmpty(cidade))
                return "Cidade é obrigatória.";

            if (string.IsNullOrEmpty(cep))
                return "CEP é obrigatório.";

            if (string.IsNullOrEmpty(telefone) && string.IsNullOrEmpty(celular))
                return "Pelo menos um telefone ou celular deve ser fornecido.";

            return string.Empty;
        }

        private bool EmailRegistrado(string email, int? id)
        {
            DataTable dt = _repository.filterByEmail(email);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (id == null || (id != null && (int)dt.Rows[0]["id"] != id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

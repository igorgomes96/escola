using ServidorWS.Models;
using System;
using System.Collections.Generic;
using ServidorWS.Exceptions;

namespace ServidorWS.Services
{
    public interface IImportacaoService
    {
        /// <summary>
        /// Lê o arquivo XML, faz o parse, salva os dados lidos, e retorna a lista de alunos salvos.
        /// </summary>
        /// <param name="fileName">Path do arquivo XML</param>
        /// <returns>Lista de alunos importados</returns>
        /// <exception cref="CPFNaoInformadoException">CPF do aluno não informado.</exception>
        /// <exception cref="CPFFormatoIncorretoException">CPF fora do padrão (Formato Inválido)</exception>
        /// <exception cref="NomeNaoInformadoException">Nome do aluno não informado.</exception>
        /// <exception cref="EnderecoNaoInformadoException">Endereço do aluno não informado.</exception>
        /// <exception cref="InvalidOperationException">Erro ao ler o arquivo.</exception>
        ICollection<Aluno> Importar(string fileName);
    }
}

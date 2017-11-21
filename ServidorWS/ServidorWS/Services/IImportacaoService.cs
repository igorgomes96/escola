using ServidorWS.Models;
using System;
using System.Collections.Generic;
using ServidorWS.Exceptions;
using ServidorWS.Dto;

namespace ServidorWS.Services
{
    public interface IImportacaoService
    {
        /// <summary>
        /// Lê o arquivo XML, faz o parse, salva os dados lidos, e retorna a lista de alunos salvos.
        /// </summary>
        /// <param name="path">Path do arquivo XML</param>
        /// <param name="nomeArquivo">Nome do Arquivo, recebido no FormData da Requisição, para ser salvo no histórico</param>
        /// <returns>Lista de alunos importados</returns>
        /// <exception cref="CPFNaoInformadoException">CPF do aluno não informado.</exception>
        /// <exception cref="CPFFormatoIncorretoException">CPF fora do padrão (Formato Inválido)</exception>
        /// <exception cref="NomeNaoInformadoException">Nome do aluno não informado.</exception>
        /// <exception cref="EnderecoNaoInformadoException">Endereço do aluno não informado.</exception>
        /// <exception cref="InvalidOperationException">Erro ao ler o arquivo.</exception>
        ICollection<Aluno> Importar(string path, string nomeArquivo);

        /// <summary>
        /// Lista o histórico de importações.
        /// </summary>
        /// <returns>Lista de importações</returns>
        ICollection<ImportacaoDto> ListAll();

        /// <summary>
        /// Deleta um registro de importação pelo código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>Importação deletada</returns>
        /// <exception cref="EntidadeNaoEncontradaException{int, Importacao}">Código não encontrado</exception>
        ImportacaoDto Delete(int codigo);

        /// <summary>
        /// Busca um registro no banco (com o arquivo)
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        /// <exception cref="EntidadeNaoEncontradaException{int, Importacao}">Código não encontrado</exception>
        Importacao Find(int codigo);


        
    }
}

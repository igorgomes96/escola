﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServidorWS.Models;
using ServidorWS.Services;
using ServidorWS.Exceptions;

namespace ServidorWS.Controllers
{
    [Route("api/Alunos")]
    public class AlunosController : ApiController
    {
        private readonly IAlunosService _alunosService;

        /// <summary>
        /// (Injeção de Dependência) Construtor responsável por receber a instância do service.
        /// </summary>
        /// <param name="alunosService">Service enviado pelo Unity na inicialização do server.</param>
        public AlunosController(IAlunosService alunosService)
        {
            _alunosService = alunosService; //?? throw new ArgumentNullException(nameof(alunosService));
        }

        /// <summary>
        /// Envia os alunos cadastrados
        /// </summary>
        /// <returns>Lista de Alunos</returns>
        public IEnumerable<Aluno> GetAlunos()
        {
            return _alunosService.ListAll();
        }

        /// <summary>
        /// Salva um aluno
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns>Aluno salvo</returns>
        [ResponseType(typeof(Aluno))]
        public IHttpActionResult PostAluno(Aluno aluno)
        {
            try
            {
                _alunosService.Save(aluno);
                return Ok(aluno);
            } catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Exclui o aluno pelo CPF
        /// </summary>
        /// <param name="cpf">CPF do aluno a ser excluído.</param>
        /// <returns>Aluno excluído</returns>
        [ResponseType(typeof(Aluno))]
        [Route("api/Alunos/{cpf}")]
        public IHttpActionResult DeleteAluno(string cpf)
        {
            try
            {
                return Ok(_alunosService.Delete(cpf));
            } catch (EntidadeNaoEncontradaException<string, Aluno> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            }
        }
    }
}
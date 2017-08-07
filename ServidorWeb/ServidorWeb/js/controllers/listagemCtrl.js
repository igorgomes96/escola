angular.module('escolaApp').controller('listagemCtrl', ['$scope', 'alunosAPI', 'messagesService', function($scope, alunosAPI, messagesService) {

	var self = this;

	self.alunos = [];
	self.alunoSelecionado = null;

	var loadAlunos = function() {
		alunosAPI.getAlunos()
		.then(function(dado) {
			self.alunos = dado.data;
		});
	}

	self.confirmaExclusao = function(aluno) {
		self.alunoSelecionado = aluno;
	}

	self.excluirAluno = function(cpf) {
		alunosAPI.deleteAluno(cpf)
		.then(function(dado) {
			messagesService.exibeMensagemSucesso("O aluno foi excluído com sucesso!");
			loadAlunos();
		});
	}

	//Listener de importação de alunos - quando uma nova base é importada, carrega novamente os alunos
	$scope.$on('newBaseEvent', function(event) {
		loadAlunos();
	});

	loadAlunos();

}]);
angular.module('escolaApp').controller('alunosCtrl', ['$scope', 'alunosAPI', 'menuService', function($scope, alunosAPI, menuService) {

	var self = this;


	menuService.menuPrincipal.selecionar('Visualização');

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
			swal('Sucesso!', 'Aluno excluído com sucesso!', 'success');
			loadAlunos();
		});
	}

	//Listener de importação de alunos - quando uma nova base é importada, carrega novamente os alunos
	$scope.$on('newBaseEvent', function(event) {
		loadAlunos();
	});

	loadAlunos();

}]);
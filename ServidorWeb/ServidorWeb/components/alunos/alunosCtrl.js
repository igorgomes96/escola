angular.module('escolaApp').controller('alunosCtrl', ['$scope', 'alunosAPI', 'menuService', function($scope, alunosAPI, menuService) {

	var self = this;


	menuService.menuPrincipal.selecionar('Visualização');

	self.alunos = [];
	self.alunoSelecionado = null;
	self.novoAluno = null;

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

	self.salvarAluno = function() {
		alunosAPI.postAluno(self.novoAluno)
		.then(function() {
			loadAlunos();
			$scope.form.$setPristine();
			swal('Sucesso!', 'Aluno salvo com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		}).finally(function() {
			self.novoAluno = null;
		});
	}

	//Listener de importação de alunos - quando uma nova base é importada, carrega novamente os alunos
	$scope.$on('newBaseEvent', function(event) {
		loadAlunos();
	});

	loadAlunos();

}]);
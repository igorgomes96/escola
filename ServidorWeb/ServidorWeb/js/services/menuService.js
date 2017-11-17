angular.module('escolaApp').service('menuService', [function() {

	var self = this;

	var ativarPeloTexto = function(texto) {
		//Desativa todas
		this.opcoes.forEach(function(opcao) {
			opcao.active = false;
		});

		//Encontra a opção
		var opcaoProcurada = this.opcoes.filter(function(opcao) {
			return opcao.texto == texto;
		});

		//Ativa a opção
		if (opcaoProcurada && opcaoProcurada.length > 0)
			opcaoProcurada[0].active = true;

	}

	self.menuPrincipal = {
		selecionar: ativarPeloTexto,
		opcoes: [
			{
				texto: 'Importação',
				state: 'cabecalho.container.importacoes',
				active: true
			},
			{
				texto: 'Visualização',
				state: 'cabecalho.container.alunos',
				active: false
			}
		]
	}


}]);
angular.module('escolaApp').controller('importacaoCtrl', ['$rootScope', 'importacoesAPI', 'menuService', 'downloadService', function($rootScope, importacoesAPI, menuService, downloadService) {

	var self = this;
	self.importacoes = [];

	menuService.menuPrincipal.selecionar('Importação');

	var getFormData = function() {

		var form = new FormData();
		form.append("arquivo", $('input[type=file]')[0].files[0]);
		form.append("nomeArquivo", $('input[type=file]')[0].files[0].name);
		return form;

	}

	self.sendFile = function() {
		
		exibeLoader();

		//Chamada do Service que faz o upload de arquivos.
		importacoesAPI.uploadFile(getFormData())
		.done(function (response) {

			//Limpa o valor do input file 
			//(se não limpar, o evento para chamar o serviço não é chamado caso seja escolhido novamente o mesmo arquivo)
			$('input[type="file"]').val(""); 

			//Dispara o evento informando que uma nova base foi importada
			$rootScope.$broadcast('newBaseEvent');

		  	swal('Sucesso!', 'Dados importados com sucesso!', 'success');
		  	
		  	//Manipulação da DOM
		  	$("#input-files-label").hide();
        	$("#button-upload-file").hide();

		}).fail(function(jqXHR, textStatus, errorThrown) {

			//Tratamento de Erros;
			if (jqXHR && jqXHR.status && jqXHR.responseText) {
				var e = JSON.parse(jqXHR.responseText);
				swal('Erro ' + jqXHR.status + '!', e.Message || jqXHR.responseText, 'error');
			} else {
				swal('Erro!', 'Ocorreu um erro inesperado!', 'error');
			}

		}).always(function() {
			loadImportacoes();
			ocultaLoader();
		});	
	}

	var loadImportacoes = function() {
		importacoesAPI.getImportacoes()
		.then(function(dado) {
			self.importacoes = dado.data;
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.deleteImportacao = function(codigo) {

		//Mensagem de Confirmação
		swal({
			title: "Confirmar Exclusão?",
		  	text: "Essa ação não poderá ser desfeita.",
		  	icon: "warning",
		  	buttons: true,
		  	dangerMode: true,
		})
		.then(function(confirma) {
		  	if (confirma) {	//Se o usuário confirma a exclusão

		  		//Exclui
		  		importacoesAPI.deleteImportacao(codigo)
				.then(function() {
					loadImportacoes();
					swal('Sucesso!', 'Registro de Importação excluído com sucesso!', 'success');
				}, function(error) {
					swal('Erro!', error.data.Message || error.data || error, 'error');
				});
		    	
		  	}
		});
	}

	self.downloadFile = function(codigo) {
		downloadService.download(importacoesAPI.downloadImportacao, codigo);
	}

	loadImportacoes();


}]);
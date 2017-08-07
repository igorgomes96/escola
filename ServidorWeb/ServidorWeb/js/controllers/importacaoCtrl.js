angular.module('escolaApp').controller('importacaoCtrl', ['$rootScope', 'uploadFileService', 'messagesService', function($rootScope, uploadFileService, messagesService) {

	var self = this;

	self.sendFile = function() {
		exibeLoader();
		uploadFileService.sendFile()
		.done(function (response) {
			$('input[type="file"]').val("");
			$rootScope.$broadcast('newBaseEvent');
		  	messagesService.exibeMensagemSucesso("Dados importados com sucesso!");
		  	$("#input-files-label").hide();
        	$("#button-upload-file").hide();
        	ocultaLoader();
		}).fail(function(jqXHR, textStatus, errorThrown) {
			if (jqXHR && jqXHR.status && jqXHR.responseText) {
				var e = JSON.parse(jqXHR.responseText);
				if (e.Message) {
					messagesService.exibeMensagemErro(jqXHR.status, e.Message);
				} else {
					messagesService.exibeMensagemErro(jqXHR.status, jqXHR.responseText);
				}
			} else {
				messagesService.exibeMensagemErro(500, 'Ocorreu um error insperado!');
			}
			ocultaLoader();
		});	
	}


}]);
angular.module('escolaApp').service('uploadFileService', ['importacoesAPI', function(importacoesAPI) {

	var self = this;

	self.sendFile = function() {

		var form = new FormData();
		form.append("arquivo", $('input[type=file]')[0].files[0]);
		return importacoesAPI.uploadFile(form);

	}

}]);
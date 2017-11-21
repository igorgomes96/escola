angular.module('escolaApp').service('importacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Importacoes';

    self.uploadFile = function(form) {

    	var settings = {
		  "async": true,
		  "crossDomain": true,
		  "url": config.baseUrl + 'Importacoes/Upload',
		  "method": "POST",
		  "headers": {
		    "cache-control": "no-cache"
		  },
		  "processData": false,
		  "contentType": false,
		  "mimeType": "multipart/form-data",
		  "data": form
		}

		return $.ajax(settings);
    }

    self.getImportacoes = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.deleteImportacao = function(codigo) {
    	return $http.delete(config.baseUrl + resource + '/' + codigo);
    }

    self.downloadImportacao = function(codigo) {
    	return $http.get(config.baseUrl + resource + '/' + codigo + '/Download');
    }

}]);
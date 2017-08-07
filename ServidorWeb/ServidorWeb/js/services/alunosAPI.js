angular.module('escolaApp').service('alunosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Alunos';

    self.getAlunos = function(statusCod) {
        return $http.get(config.baseUrl + resource);
    }

    self.deleteAluno = function(cpf) {
        return $http.delete(config.baseUrl + resource + '/' + cpf);
    }
    
}]);
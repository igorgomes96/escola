angular.module('escolaApp').config(['$httpProvider', function($httpProvider) {
	$httpProvider.interceptors.push('errorInterceptor');
}]);
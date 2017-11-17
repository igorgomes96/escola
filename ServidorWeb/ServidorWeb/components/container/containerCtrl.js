angular.module('escolaApp').controller('containerCtrl', ['menuService', function(menuService) {

	var self = this;
	self.menu = menuService.menuPrincipal;
}]);
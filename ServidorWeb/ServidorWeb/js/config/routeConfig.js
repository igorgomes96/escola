angular.module('escolaApp').config(function($stateProvider, $urlRouterProvider) {

   
    $urlRouterProvider.otherwise('/escola');
    
    $stateProvider

    .state('cabecalho', {
        templateUrl: 'views/cabecalho.html'
    })

    .state('cabecalho.container', {
        url: '/escola',
        views: {
            '': {
                templateUrl: 'views/container.html'
            },
            'importacao@cabecalho.container': {
                templateUrl: 'views/importacao.html', 
                controller: 'importacaoCtrl as ct'
            },
            'listagem@cabecalho.container': {
                templateUrl: 'views/listagem.html', 
                controller: 'listagemCtrl as ct'
            }
        }
    });
    
});
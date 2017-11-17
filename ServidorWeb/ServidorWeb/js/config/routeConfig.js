angular.module('escolaApp').config(function($stateProvider, $urlRouterProvider) {

   
    $urlRouterProvider.otherwise('/home');
    
    $stateProvider
    
    .state('cabecalho', {
        templateUrl: 'components/cabecalho/cabecalho.html'
    })

    .state('cabecalho.home', {
        url: '/home',
        templateUrl: 'components/home/home.html'
    })

    .state('cabecalho.container', {
        templateUrl: 'components/container/container.html',
        controller: 'containerCtrl as ctContainer'
    })

    .state('cabecalho.container.importacoes', {
        url: '/importacoes',
        templateUrl: 'components/importacoes/importacao.html', 
        controller: 'importacaoCtrl as ct'
    })

    .state('cabecalho.container.alunos', {
        url: '/alunos',
        templateUrl: 'components/alunos/alunos.html',
        controller: 'alunosCtrl as ct'
    });
    
});
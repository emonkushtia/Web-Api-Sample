/* global angular: false */

(function(ng) {
    ng.module('angularDemo', [
            'ngRoute',
            'ngAnimate',
            'ui.select2',
            'ui.bootstrap',
            'angularDemo.common',
            'students'
        ])
        .config([
            '$routeProvider', function($routeProvider) {
            $routeProvider.when('/', {
                templateUrl: 'Application/dashboard/dashboard.html',
            });
            $routeProvider.when('/readme', {
                templateUrl: 'Application/dashboard/read-me.html'
            });
            $routeProvider.when('/aboutme', {
                templateUrl: 'Application/dashboard/about-me.html'
            });
            $routeProvider.when('/students/:sortBy?/:sortOrder?/:page?/:pageSize?', {
                templateUrl: 'Application/students/students.html',
                controller: 'StudentListController',
                controllerAs: 'StudentList',
                 resolve: {
                     Students: ['$route', 'Student', function($route, Student) {
                             var params = $route.current.params;

                             params.sortBy = params.sortBy || 'firstName';
                             params.sortOrder = params.sortOrder || 'desc';
                             params.page = params.page || 1;
                             params.pageSize = params.pageSize || 10;
                             return Student.all(params.sortBy, params.sortOrder, params.page, params.pageSize);
                         }
                     ] }
            });
            $routeProvider.otherwise('/');
        }
        ]);
})(angular)
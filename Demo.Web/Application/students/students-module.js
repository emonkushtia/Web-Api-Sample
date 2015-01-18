/* global _: false, angular: false */

(function(ng) {
    ng.module('students', [
           'angularDemo.common'
    ])
        .config( ['$routeProvider', function ($routeProvider) {
            $routeProvider.when('/student/create', {
                templateUrl: 'Application/students/student-create.html',
                controller: 'StudentCreateController',
                controllerAs: 'CreateStudent'
            });
        $routeProvider.when('/student/:id/edit', {
            templateUrl: 'Application/students/student-edit.html',
            controller: 'StudentEditController',
            controllerAs: 'EditStudent',
            resolve: {
                StudentData: [
                    '$route', 'Student', function($route, Student) {
                        var params = $route.current.params;
                        return Student.one(params.id);
                    }
                ]
            }
        });
        $routeProvider.when('/student/:id/details', {
            templateUrl: 'Application/students/student-details.html',
            controller: 'StudentDetailsController',
            controllerAs: 'StudentDetails',
            resolve: {
                StudentData: [
                    '$route', 'Student', function ($route, Student) {
                        var params = $route.current.params;
                        return Student.one(params.id);
                    }
                ]
            }
        });

    }]);
})(angular)
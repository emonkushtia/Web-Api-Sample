(function (ng) {
    ng.module('students').controller('StudentDetailsController', StudentDetailsController);
    StudentDetailsController.$inject = ['$scope', '$location', '$route', 'blFlash', 'Student'];

    function StudentDetailsController($scope, $location, $route, flash, StudentService) {
        var vm = this;
        vm.student = {};
        load();

        ////////////////////////

        function load() {
            vm.student = $route.current.locals.StudentData.data;
        };
    }
})(angular)
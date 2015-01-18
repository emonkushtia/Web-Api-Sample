(function (ng) {
    ng.module('students').controller('StudentEditController', StudentEditController);
    StudentEditController.$inject = ['$scope', '$location', '$route', 'blFlash', 'Student'];

    function StudentEditController($scope, $location, $route, flash, StudentService) {
        var vm = this;
        vm.student = {};
        vm.save = save;
        vm.reset = reset;
        load();

        ////////////////////////

        function load() {
            vm.student = $route.current.locals.StudentData.data;
        };
        function save() {
            $scope.$broadcast('show-errors-check-validity');

            if ($scope.studentForm.$valid) {
                var student = {
                    id: vm.student.id,
                    firstName: vm.student.firstName,
                    lastName: vm.student.lastName,
                    email: vm.student.email,
                    phone: vm.student.phone
                };
                StudentService.edit(student);
                flash.success('Student has been updated successfully.');
                $location.path('/students/firstName/desc/1/10');
            }
        };
        function reset() {
            $scope.$broadcast('show-errors-reset');
            vm.student = { firstName: '', lastName: '', email: '', phone: '' };
        };
    }
})(angular)
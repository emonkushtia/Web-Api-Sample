(function (ng) {
    ng.module('students').controller('StudentCreateController', StudentCreateController);
    StudentCreateController.$inject = ['$scope', '$location', 'blFlash', 'Student'];

    function StudentCreateController($scope, $location, flash, StudentService) {
        var vm = this;
        vm.save = save;
        vm.reset = reset;

        ///////////////////////////////////

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
                StudentService.add(student);
                flash.success('Student has been created successfully.');
                $location.path('/students/firstName/desc/1/10');
            }
        };

        function reset() {
            $scope.$broadcast('show-errors-reset');
            vm.student = { firstName: '', lastName: '', email: '', phone: '' };
        };
    }
})(angular)
/* global _: false, angular: false */

(function (_, ng) {
    'use strict';
    angular
    .module('students')
    .controller('StudentListController', studentListController);

    studentListController.$inject = ['$location', '$route', '$filter','Student','blFlash','blModal'];

    function studentListController($location, $route, $filter,StudentService,flash,modal) {
        var sortBy = '',
            descending = false;

        var vm = this;
        vm.students = [];
        vm.totalItems = 0;
        vm.currentPage = 0;
        vm.pageSize = 0;
        vm.isSortedBy = isSortedBy;
        vm.isDescending = isDescending;
        vm.getSortBy = getSortBy;
        vm.sort = sort;
        vm.pageChanged = pageChanged;
        vm.pageSizeChanged = pageSizeChanged;
        vm.clone = clone;
        vm.remove = remove;

        load();

        ////////////////////////

        function load() {
            vm.students = $route.current.locals.Students.data.list;
            vm.totalItems = $route.current.locals.Students.data.count;
            sortBy = $route.current.params.sortBy || 'firstName';
            descending = $route.current.params.sortOrder === 'desc';
            vm.pageSize = $route.current.params.pageSize || 10;
            vm.currentPage = $route.current.params.page || 1;

        }

        function remove(id) {

            modal.confirm('Do you want to delete?', function () {
                StudentService.remove(id);
                var stu = _(vm.students).findWhere({ id: +id });
                var itemIndex = _(vm.students).indexOf(stu);
                if (itemIndex >= 0) {
                    vm.students.splice(itemIndex, 1);
                }
                flash.warning('The student has been deleted.');
            });
        }

        function clone(student) {
            StudentService.clone(student.id).then(function(success) {
                vm.students.unshift(success.data);
                flash.info('The student has been cloned.');
            });
        }
        function goTo(page, pageSize) {
            $location.path('/students/' + sortBy + '/' + (descending ? 'desc' : 'asc') + '/' + page + '/' + pageSize);

        }
        function isSortedBy(name) {
            return name === sortBy;
        }

        function isDescending() {
            return descending;
        }

        function getSortBy() {
            return sortBy;
        }

        function sort(name) {
            if (isSortedBy(name)) {
                descending = !descending;
            } else {
                sortBy = name;
                descending = false;
            }
            goTo(1, vm.pageSize);
        }
        function pageChanged() {
            goTo(vm.currentPage, vm.pageSize);
        }

        function pageSizeChanged() {
            vm.currentPage = 1;
            goTo(vm.currentPage, vm.pageSize);
        }
    }
})(_, angular);
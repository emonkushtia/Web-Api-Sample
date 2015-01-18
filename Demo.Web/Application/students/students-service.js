/* global _: false, angular: false */

(function (_, ng) {
    'use strict';
    var url = 'api/students';
    function Student() {
        this.$get = [
            '$q','$http', function ($q,$http) {
                return {
                    all: function (sortBy, sortOrder, page, pageSize) {
                        var offset = (page - 1) * pageSize,
                            sort = (sortBy || '') + (sortOrder ? ' ' + sortOrder : '');
                        return $http.get(url, {
                            params: {
                                offset: offset,
                                limit: pageSize,
                                sort: sort
                            }
                        });
                    },
                    one: function (id) {
                        return $http.get(url + '/' + id);
                    },
                    add: function (student) {
                        return $http.post(url, student);
                    },
                    edit: function (student) {
                        return $http.put(url, student);
                    },
                    clone: function (id) {
                        return $http.put(url + '/' + id + '/clone');
                    },
                    remove: function (id) {
                        return $http.delete(url + '/' + id);
                    }
                };
            }
        ];
    }

    ng.module('students').provider('Student', [Student]);

})(_, angular);
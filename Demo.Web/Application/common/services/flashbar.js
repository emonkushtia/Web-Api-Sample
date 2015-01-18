/* global angular: false, toastr: false */

(function (ng, toastr) {
    'use strict';

    ng.module('angularDemo.common')
        .factory('blFlash', [
            function() {
                var methods = ['success', 'info', 'warning', 'error'],
                    factory = {};

                function trigger(type, message) {
                    toastr[type](message, type.substring(0, 1).toUpperCase() + type.substring(1), {
                        positionClass: 'toast-top-right',
                        closeButton: true
                    });
                }

                methods.forEach(function(method) {
                    factory[method] = function(message) {
                        trigger(method, message);
                    };
                });

                return factory;
            }
        ]);

})(angular, toastr);
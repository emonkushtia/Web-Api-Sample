/* global angular: false */

(function(ng) {
    'use strict';

    ng.module('angularDemo.common', [
        'ngRoute',
        'ngSanitize',
        'ui.bootstrap'
    ]).run([
        '$templateCache', function($templateCache) {
            $templateCache.put("template/popover/popover.html",
                "<div class=\"popover {{placement}}\" ng-class=\"{ in: isOpen(), fade: animation() }\">\n" +
                "  <div class=\"arrow\"></div>\n" +
                "\n" +
                "  <div class=\"popover-inner\">\n" +
                "      <h3 class=\"popover-title\" ng-bind=\"title\" ng-show=\"title\"></h3>\n" +
                "      <div class=\"popover-content\" ng-bind-html=\"content\"></div>\n" +
                "  </div>\n" +
                "</div>\n" +
                "");
        }
    ]);

})(angular);
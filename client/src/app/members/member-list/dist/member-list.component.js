"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.MemberListComponent = void 0;
var core_1 = require("@angular/core");
var MemberListComponent = /** @class */ (function () {
    function MemberListComponent(memberService) {
        this.memberService = memberService;
        this.members = [];
    }
    MemberListComponent.prototype.ngOnInit = function () {
        this.loadMembers();
    };
    MemberListComponent.prototype.loadMembers = function () {
        var _this = this;
        this.memberService.getMembers().subscribe((function (members) {
            _this.members = members;
        }));
    };
    MemberListComponent = __decorate([
        core_1.Component({
            selector: 'app-member-list',
            templateUrl: './member-list.component.html',
            styleUrls: ['./member-list.component.scss']
        })
    ], MemberListComponent);
    return MemberListComponent;
}());
exports.MemberListComponent = MemberListComponent;
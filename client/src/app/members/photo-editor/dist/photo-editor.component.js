"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.PhotoEditorComponent = void 0;
var core_1 = require("@angular/core");
var ng2_file_upload_1 = require("ng2-file-upload");
var operators_1 = require("rxjs/operators");
var environment_1 = require("src/environments/environment");
var PhotoEditorComponent = /** @class */ (function () {
    function PhotoEditorComponent(accountService, membersService) {
        var _this = this;
        this.accountService = accountService;
        this.membersService = membersService;
        this.hasBaseDropzoneOver = false;
        this.baseUrl = environment_1.environment.apiUrl;
        this.accountService.currentUser$.pipe(operators_1.take(1)).subscribe(function (user) {
            if (user)
                _this.user = user;
        });
    }
    PhotoEditorComponent.prototype.ngOnInit = function () {
        this.initializeUploader();
    };
    PhotoEditorComponent.prototype.fileOverBase = function (e) {
        this.hasBaseDropzoneOver = e;
    };
    PhotoEditorComponent.prototype.setMainPhoto = function (photo) {
        var _this = this;
        this.membersService.setMainPhoto(photo.id).subscribe(function () {
            _this.user.photoUrl = photo.url;
            _this.accountService.setCurrentUser(_this.user);
            _this.member.photoUrl = photo.url;
            _this.member.photos.forEach(function (p) {
                if (p.isMain)
                    p.isMain = false;
                if (p.id == photo.id)
                    p.isMain = true;
            });
        });
    };
    PhotoEditorComponent.prototype.deletePhoto = function (photoId) {
        var _this = this;
        this.membersService.deletePhoto(photoId).subscribe(function () {
            _this.member.photos = _this.member.photos.filter(function (x) { return x.id !== photoId; });
        });
    };
    PhotoEditorComponent.prototype.initializeUploader = function () {
        var _this = this;
        this.uploader = new ng2_file_upload_1.FileUploader({
            url: this.baseUrl + "users/add-photo",
            authToken: "Bearer " + this.user.token,
            isHTML5: true,
            allowedFileType: ['image'],
            removeAfterUpload: true,
            autoUpload: false,
            maxFileSize: 10 * 1024 * 1024
        });
        this.uploader.onAfterAddingFile = function (file) {
            file.withCredentials = false;
        };
        this.uploader.onSuccessItem = function (item, response, status, headers) {
            if (response) {
                var photo = JSON.parse(response);
                _this.member.photos.push(photo);
            }
        };
    };
    __decorate([
        core_1.Input()
    ], PhotoEditorComponent.prototype, "member");
    PhotoEditorComponent = __decorate([
        core_1.Component({
            selector: 'app-photo-editor',
            templateUrl: './photo-editor.component.html',
            styleUrls: ['./photo-editor.component.scss']
        })
    ], PhotoEditorComponent);
    return PhotoEditorComponent;
}());
exports.PhotoEditorComponent = PhotoEditorComponent;

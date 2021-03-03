import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {
  title: string;
  message: string;
  btnOkText: string;
  btnCancelText: string;
  result: boolean;

  constructor(public bsModelRef: BsModalRef) {
    this.title = '';
    this.message = '';
    this.btnOkText = '';
    this.btnCancelText = '';
    this.result = false;
  }

  ngOnInit(): void {
  }

  confirm() {
    this.result = true;
    this.bsModelRef.hide();
  }

  decline() {
    this.result = false;
    this.bsModelRef.hide();
  }

}
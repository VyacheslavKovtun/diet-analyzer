import { Component } from "@angular/core";

@Component({
  selector: 'wrong-login-dialog-element',
  templateUrl: 'wrong-login-dialog.html',
})
export class WrongLoginDialogElement {
  title: string = "Title";
  content: string = "Content";
  closeBtnTitle: string = "Close";
}
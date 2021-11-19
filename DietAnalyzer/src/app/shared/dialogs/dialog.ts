import { Component } from "@angular/core";

@Component({
  selector: 'dialog-element',
  templateUrl: 'dialog.html',
})
export class DialogElement {
  title: string = "Title";
  content: string = "Content";
  closeBtnTitle: string = "Close";

  public setDialog(title: string, content: string, closeBtnTitle: string) {
    this.title = title;
    this.content = content;
    this.closeBtnTitle = closeBtnTitle;
  }
}
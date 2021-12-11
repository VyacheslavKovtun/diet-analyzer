import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

export interface Data {
    itemName: string
}

@Component({
    selector: 'add-shopping-list-item-dialog',
    templateUrl: 'add-shopping-list-item-dialog.html',
})
export class AddShoppingListItemDialog{
    constructor(public dialogRef: MatDialogRef<AddShoppingListItemDialog>, 
        @Inject(MAT_DIALOG_DATA) public data: Data) {}
  
    onCancelClick(): void {
      this.dialogRef.close();
    }
}
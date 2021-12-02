import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

export interface DialogData {
    amount: number,
    unit: string,
    expDate: Date,
    price: number,
    purDate: Date;
}

@Component({
    selector: 'fill-base-info-dialog',
    templateUrl: 'fill-base-info-dialog.html',
})
export class FillBaseInfoDialog{
    constructor(public dialogRef: MatDialogRef<FillBaseInfoDialog>, 
        @Inject(MAT_DIALOG_DATA) public data: DialogData) {}
  
    onCancelClick(): void {
      this.dialogRef.close();
    }
}
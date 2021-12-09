import { Component, Inject } from "@angular/core";
import { FormControl } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

export interface RecipeData {
    slot: number,
    position: number
}

@Component({
    selector: 'fill-recipe-info-dialog',
    templateUrl: 'fill-recipe-info-dialog.html',
})
export class FillRecipeInfoDialog{
    myControl = new FormControl();
    options: number[] = [1, 2, 3];

    constructor(public dialogRef: MatDialogRef<FillRecipeInfoDialog>, 
        @Inject(MAT_DIALOG_DATA) public data: RecipeData) {}
  
    onCancelClick(): void {
      this.dialogRef.close();
    }
}
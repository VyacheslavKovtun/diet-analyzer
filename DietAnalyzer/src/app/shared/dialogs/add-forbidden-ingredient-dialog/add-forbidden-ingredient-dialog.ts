import { Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ForbiddenIngredientsService } from 'src/app/common/api/services/forbidden-ingredients.service';
import { IngredientsBaseInfoService } from 'src/app/common/api/services/ingredients-base-info.service';
import { IngredientsService } from 'src/app/common/api/services/ingredients.service';
import { IngredientBaseInfo } from 'src/app/common/interfaces/ingredient-base-info.interface';
import { ShortIngredient } from 'src/app/common/interfaces/short-ingredient.interface';
import { EmptyFieldDialog } from '../empty-field-dialog/empty-field-dialog';

@Component({
  selector: 'add-forbidden-ingredient-dialog',
  templateUrl: 'add-forbidden-ingredient-dialog.html',
  styleUrls: ['add-forbidden-ingredient-dialog.css']
})
export class AddForbiddenIngredientDialog implements OnInit{
  findIngredientForm !: FormGroup;
  ingredients : ShortIngredient[] = [];
  ingrBaseInfo !: IngredientBaseInfo;

  constructor(public dialogRef: MatDialogRef<AddForbiddenIngredientDialog>, 
    private ingredientsService: IngredientsService, public dialog: MatDialog, private ingredientsBaseInfoService: IngredientsBaseInfoService,
    private authService: AuthService, private forbiddenIngredientsService: ForbiddenIngredientsService) {}

  addIngredient(ingr: ShortIngredient) {
    this.authService.getCurrentUser().subscribe(curUser => {
        if(curUser) {
            var ingrBaseInfo = {
                id: 0,
                apiId: ingr.id,
                name: ingr.name,
                imageUrl: ingr.image
            };
            
            this.ingredientsBaseInfoService.createIngredientBaseInfo(ingrBaseInfo).subscribe(cIBIRes => {
                this.ingredientsBaseInfoService.getIngredientBaseInfoByApiId(ingr.id).subscribe(ingrBInfo => {
                    var forbiddenIngredient = {
                        id: 0,
                        ingredientId: ingrBInfo.id,
                        userId: curUser.id
                    };
                    
                    this.forbiddenIngredientsService.createForbiddenIngredient(forbiddenIngredient).subscribe();
                });
            });
        }
    });
  }

  ngOnInit(): void {
    this.findIngredientForm = new FormGroup({
      "ingredientName": new FormControl('', Validators.required)
    });
  }

  searchClick() {
    const field = this.findIngredientForm.value;
    var name = field.ingredientName;
    if(name != '') {
      this.ingredients = [];

      this.ingredientsService.getIngredientsByName(name).subscribe(res => {
        this.ingredients = res;
      });
    }
    else {
      this.dialog.open(EmptyFieldDialog);
    }
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
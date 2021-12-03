import {Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthService } from 'src/app/auth/services/auth.service';
import { BaseInfoService } from 'src/app/common/api/services/base-info.service';
import { CurrentIngredientsService } from 'src/app/common/api/services/current-ingredients.service';
import { IngredientsBaseInfoService } from 'src/app/common/api/services/ingredients-base-info.service';
import { IngredientsExpensesService } from 'src/app/common/api/services/ingredients-expenses.service';
import { IngredientsService } from 'src/app/common/api/services/ingredients.service';
import { BaseInfo } from 'src/app/common/interfaces/base-info.interface';
import { CurrentIngredient } from 'src/app/common/interfaces/current-ingredient.interface';
import { IngredientBaseInfo } from 'src/app/common/interfaces/ingredient-base-info.interface';
import { IngredientsExpense } from 'src/app/common/interfaces/ingredients-expense.interface';
import { ShortIngredient } from 'src/app/common/interfaces/short-ingredient.interface';
import { EmptyFieldDialog } from '../empty-field-dialog/empty-field-dialog';
import { FillBaseInfoDialog } from '../fill-base-info-dialog/fill-base-info-dialog';

@Component({
  selector: 'add-current-ingr-dialog',
  templateUrl: 'add-current-ingr-dialog.html',
  styleUrls: ['add-current-ingr-dialog.css']
})
export class AddCurrentIngredientDialog implements OnInit{
  findIngredientForm !: FormGroup;
  ingredients : ShortIngredient[] = [];
  ingrBaseInfo !: IngredientBaseInfo;
  baseInfo !: BaseInfo;
  ingrExpense !: IngredientsExpense;
  currentIngredient !: CurrentIngredient;
  addingIngr !: ShortIngredient;

  amount !: number;
  unit !: string;
  expDate !: Date;
  price !: number;
  purDate !: Date;
  isExpired !: boolean;

  fBIDialog !: MatDialogRef<FillBaseInfoDialog, any>;

  constructor(public dialogRef: MatDialogRef<AddCurrentIngredientDialog>, 
    private ingredientsService: IngredientsService, public dialog: MatDialog, private ingredientsBaseInfoService: IngredientsBaseInfoService,
    private baseInfoService: BaseInfoService, private authService: AuthService, private ingrExpensesService: IngredientsExpensesService,
    private currentIngredientsService: CurrentIngredientsService) {}

    openDialog() {
      this.fBIDialog = this.dialog.open(FillBaseInfoDialog, {
        width: '350px',
        data: {amount: this.amount, unit: this.unit, expDate: this.expDate, price: this.price, purDate: this.purDate},
      });
  
      this.fBIDialog.afterClosed().subscribe(result => {
        this.amount = result[0];
        this.unit = result[1];
        this.expDate = result[2];
        this.price = result[3];
        this.purDate = result[4];

        this.save();
      });
    }

  addIngredient(ingr: ShortIngredient) {
    this.addingIngr = ingr;
    this.openDialog();
  }

  save() {
    if(this.expDate != null) {
      if(Date.parse(this.expDate.toDateString()) >= Date.now()) {
        this.isExpired = false;
      }
      else {
        this.isExpired = true;
      }
    }

    if(this.addingIngr != null) {
      this.ingrBaseInfo = {
        id: 0,
        apiId: this.addingIngr.id,
        name: this.addingIngr.name,
        imageUrl: this.addingIngr.image
      };
      this.openDialog();

      this.ingredientsBaseInfoService.createIngredientBaseInfo(this.ingrBaseInfo).subscribe(res => {
        this.ingredientsService.getIngredientById(this.addingIngr.id).subscribe(r => {
          this.baseInfo = {
            id: 0,
            price: this.price,
            amount: this.amount,
            expirationDate: this.expDate,
            unit: this.unit,
            mealType: r.aisle
          };

          this.baseInfoService.createBaseInfo(this.baseInfo).subscribe(crRes => {
            this.authService.getCurrentUser().subscribe(user => {
              this.ingredientsBaseInfoService.getIngredientBaseInfoByApiId(this.addingIngr.id).subscribe(ingrInfo => {
                this.baseInfoService.getBaseInfoByFields(this.baseInfo).subscribe(bInfo => {
                  this.ingrExpense = {
                    id: 0,
                    ingredientId: ingrInfo.id,
                    userId: user.id,
                    baseInfoId: bInfo.id,
                    purchasingDate: this.purDate,
                    isExpired: false
                  };
  
                  this.ingrExpensesService.createIngredientsExpense(this.ingrExpense).subscribe();
  
                  this.currentIngredient = {
                    id: 0,
                    ingredientId: ingrInfo.id,
                    userId: user.id,
                    baseInfoId: bInfo.id
                  };
                  this.currentIngredientsService.createCurrentIngredient(this.currentIngredient).subscribe();
                });
              });
            });
          });
       });
      });
    }

    this.fBIDialog.close();
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